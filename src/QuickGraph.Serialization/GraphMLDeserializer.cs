using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Reflection.Emit;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace QuickGraph.Serialization
{
    /// <summary>
    /// A GraphML ( http://graphml.graphdrawing.org/ ) format deserializer.
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <typeparam name="TGraph">type of the graph</typeparam>
    /// <remarks>
    /// <para>
    /// Custom vertex, edge and graph attributes can be specified by 
    /// using the <see cref="System.Xml.Serialization.XmlAttributeAttribute"/>
    /// attribute on properties (field not suppored).
    /// </para>
    /// <para>
    /// The serializer uses LCG (lightweight code generation) to generate the 
    /// methods that writes the attributes to avoid paying the price of 
    /// Reflection on each vertex/edge. Since nothing is for free, the first
    /// time you will use the serializer *on a particular pair of types*, it
    /// will have to bake that method.
    /// </para>
    /// <para>
    /// Hyperedge, nodes, nested graphs not supported.
    /// </para>
    /// </remarks>
    public sealed class GraphMLDeserializer<TVertex, TEdge, TGraph>
        : SerializerBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IMutableVertexAndEdgeSet<TVertex, TEdge>
    {
        #region Compiler
        delegate void ReadVertexAttributesDelegate(
            XmlReader reader,
            string namespaceUri,
            TVertex v);
        delegate void ReadEdgeAttributesDelegate(
            XmlReader reader,
            string namespaceUri,
            TEdge e);
        delegate void ReadGraphAttributesDelegate(
            XmlReader reader,
            string namespaceUri,
            TGraph g);

        static class ReadDelegateCompiler
        {
            public static readonly ReadVertexAttributesDelegate VertexAttributesReader;
            public static readonly ReadEdgeAttributesDelegate EdgeAttributesReader;
            public static readonly ReadGraphAttributesDelegate GraphAttributesReader;
            public static readonly Action<TVertex> SetVertexDefault;
            public static readonly Action<TEdge> SetEdgeDefault;
            public static readonly Action<TGraph> SetGraphDefault; 

            static ReadDelegateCompiler()
            {
                VertexAttributesReader =
                    (ReadVertexAttributesDelegate)CreateReadDelegate(
                    typeof(ReadVertexAttributesDelegate),
                    typeof(TVertex)
                    //,"id"
                    );
                EdgeAttributesReader =
                    (ReadEdgeAttributesDelegate)CreateReadDelegate(
                    typeof(ReadEdgeAttributesDelegate),
                    typeof(TEdge)
                    //,"id", "source", "target"
                    );
                GraphAttributesReader =
                    (ReadGraphAttributesDelegate)CreateReadDelegate(
                    typeof(ReadGraphAttributesDelegate),
                    typeof(TGraph)
                    );
                SetVertexDefault =
                    (Action<TVertex>)CreateSetDefaultDelegate(
                        typeof(Action<TVertex>),
                        typeof(TVertex)
                    );
                SetEdgeDefault =
                    (Action<TEdge>)CreateSetDefaultDelegate(
                        typeof(Action<TEdge>),
                        typeof(TEdge)
                    );
                SetGraphDefault =
                    (Action<TGraph>)CreateSetDefaultDelegate(
                        typeof(Action<TGraph>),
                        typeof(TGraph)
                    );
            }

            static class Metadata
            {
                public static readonly MethodInfo ReadToFollowingMethod =
                    typeof(XmlReader).GetMethod(
                        "ReadToFollowing",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                public static readonly MethodInfo GetAttributeMethod =
                    typeof(XmlReader).GetMethod(
                        "GetAttribute",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                public static readonly PropertyInfo NameProperty =
                    typeof(XmlReader).GetProperty("Name");
                public static readonly PropertyInfo NamespaceUriProperty =
                    typeof(XmlReader).GetProperty("NamespaceUri");
                public static readonly MethodInfo StringEqualsMethod =
                    typeof(string).GetMethod(
                        "op_Equality",
                        BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                public static readonly ConstructorInfo ArgumentExceptionCtor =
                    typeof(ArgumentException).GetConstructor(new Type[] { typeof(string) });

                private static readonly Dictionary<Type, MethodInfo> ReadContentMethods;

                static Metadata()
                {
                    ReadContentMethods = new Dictionary<Type, MethodInfo>();
                    ReadContentMethods.Add(typeof(bool), typeof(XmlReader).GetMethod("ReadElementContentAsBoolean", new Type[] { typeof(string), typeof(string) }));
                    ReadContentMethods.Add(typeof(int), typeof(XmlReader).GetMethod("ReadElementContentAsInt", new Type[] { typeof(string), typeof(string) }));
                    ReadContentMethods.Add(typeof(long), typeof(XmlReader).GetMethod("ReadElementContentAsLong", new Type[] { typeof(string), typeof(string) }));
                    ReadContentMethods.Add(typeof(float), typeof(XmlReader).GetMethod("ReadElementContentAsFloat", new Type[] { typeof(string), typeof(string) }));
                    ReadContentMethods.Add(typeof(double), typeof(XmlReader).GetMethod("ReadElementContentAsDouble", new Type[] { typeof(string), typeof(string) }));
                    ReadContentMethods.Add(typeof(string), typeof(XmlReader).GetMethod("ReadElementContentAsString", new Type[] { typeof(string), typeof(string) }));
                }

                public static bool TryGetReadContentMethod(Type type, out MethodInfo method)
                {
                    Contract.Requires(type != null);

                    bool result = ReadContentMethods.TryGetValue(type, out method);
                    Contract.Assert(!result || method != null, type.FullName);
                    return result;
                }
            }

            public static Delegate CreateSetDefaultDelegate(
                Type delegateType,
                Type elementType
                )
            {
                Contract.Requires(delegateType != null);
                Contract.Requires(elementType != null);

                var method = new DynamicMethod(
                    "Set" + elementType.Name + "Default",
                    typeof(void),
                    new Type[] { elementType },
                    elementType.Module
                    );
                var gen = method.GetILGenerator();

                // we need to create the swicth for each property
                foreach (var kv in SerializationHelper.GetAttributeProperties(elementType))
                {
                    var property = kv.Property;
                    var defaultValueAttribute = Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute))
                        as DefaultValueAttribute;
                    if (defaultValueAttribute == null)
                        continue;
                    var setMethod = property.GetSetMethod();
                    if (setMethod == null)
                        throw new InvalidOperationException("property " + property.Name + " is not settable");
                    var value = defaultValueAttribute.Value;
                    if (value != null &&
                        value.GetType() != property.PropertyType)
                        throw new InvalidOperationException("invalid default value type of property " + property.Name);
                    gen.Emit(OpCodes.Ldarg_0);
                    switch (Type.GetTypeCode(property.PropertyType))
                    {
                        case TypeCode.Int32:
                            gen.Emit(OpCodes.Ldc_I4, (int)value);
                            break;
                        case TypeCode.Int64:
                            gen.Emit(OpCodes.Ldc_I8, (long)value);
                            break;
                        case TypeCode.Single:
                            gen.Emit(OpCodes.Ldc_R4, (float)value);
                            break;
                        case TypeCode.Double:
                            gen.Emit(OpCodes.Ldc_R8, (double)value);
                            break;
                        case TypeCode.String:
                            gen.Emit(OpCodes.Ldstr, (string)value);
                            break;
                        case TypeCode.Boolean:
                            gen.Emit((bool)value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                            break;
                        default:
                            throw new InvalidOperationException("unsupported type " + property.PropertyType);
                    }
                    gen.EmitCall(setMethod.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, setMethod, null);
                }
                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }

            public static Delegate CreateReadDelegate(
                Type delegateType,
                Type elementType
                //,params string[] ignoredAttributes
                )
            {
                Contract.Requires(delegateType != null);
                Contract.Requires(elementType != null);

                var method = new DynamicMethod(
                    "Read" + elementType.Name,
                    typeof(void),
                    //          reader, namespaceUri
                    new Type[] { typeof(XmlReader), typeof(string), elementType },
                    elementType.Module
                    );
                var gen = method.GetILGenerator();

                var key = gen.DeclareLocal(typeof(string));

                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldstr, "key");
                gen.EmitCall(OpCodes.Callvirt, Metadata.GetAttributeMethod, null);
                gen.Emit(OpCodes.Stloc_0);

                //// if (String.Equals(key, "id")) continue;
                //foreach (string ignoredAttribute in ignoredAttributes)
                //{
                //    gen.Emit(OpCodes.Ldloc_0);
                //    gen.Emit(OpCodes.Ldstr, ignoredAttribute);
                //    gen.EmitCall(OpCodes.Call, Metadata.StringEqualsMethod, null);
                //    gen.Emit(OpCodes.Brtrue, doWhile);
                //}

                // we need to create the swicth for each property
                var next = gen.DefineLabel();
                var @return = gen.DefineLabel();
                bool first = true;
                foreach (var kv in SerializationHelper.GetAttributeProperties(elementType))
                {
                    var property = kv.Property;

                    if (!first)
                    {
                        gen.MarkLabel(next);
                        next = gen.DefineLabel();
                    }
                    first = false;

                    // if (!key.Equals("foo"))
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Ldstr, kv.Name);
                    gen.EmitCall(OpCodes.Call, Metadata.StringEqualsMethod, null);
                    // if false jump to next
                    gen.Emit(OpCodes.Brfalse, next);

                    // do our stuff
                    MethodInfo readMethod = null;
                    if (!Metadata.TryGetReadContentMethod(property.PropertyType, out readMethod))
                        throw new ArgumentException(String.Format("Property {0} has a non-supported type", property.Name));

                    // do we have a set method ?
                    var setMethod = property.GetSetMethod();
                    if (setMethod == null)
                        throw new ArgumentException(String.Format("Property {0}.{1} has not set method", property.DeclaringType, property.Name));
                    // reader.ReadXXX
                    gen.Emit(OpCodes.Ldarg_2); // element
                    gen.Emit(OpCodes.Ldarg_0); // reader
                    gen.Emit(OpCodes.Ldstr, "data");
                    gen.Emit(OpCodes.Ldarg_1); // namespace uri
                    gen.EmitCall(OpCodes.Callvirt, readMethod, null);
                    gen.EmitCall(OpCodes.Callvirt, setMethod, null);

                    // jump to do while
                    gen.Emit(OpCodes.Br, @return);
                }

                // we don't know this parameter.. we throw
                gen.MarkLabel(next);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Newobj, Metadata.ArgumentExceptionCtor);
                gen.Emit(OpCodes.Throw);

                gen.MarkLabel(@return);
                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }
        }
        #endregion

        public void Deserialize(
            XmlReader reader,
            TGraph visitedGraph,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory)
        {
            Contract.Requires(reader != null);
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            var worker = new ReaderWorker(
                this,
                reader,
                visitedGraph,
                vertexFactory,
                edgeFactory);
            worker.Deserialize();
        }

        class ReaderWorker
        {
            private readonly GraphMLDeserializer<TVertex, TEdge, TGraph> serializer;
            private readonly XmlReader reader;
            private readonly TGraph visitedGraph;
            private readonly IdentifiableVertexFactory<TVertex> vertexFactory;
            private readonly IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory;
            private string graphMLNamespace = "";

            public ReaderWorker(
                GraphMLDeserializer<TVertex, TEdge, TGraph> serializer,
                XmlReader reader,
                TGraph visitedGraph,
                IdentifiableVertexFactory<TVertex> vertexFactory,
                IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
                )
            {
                Contract.Requires(serializer != null);
                Contract.Requires(reader != null);
                Contract.Requires(visitedGraph != null);
                Contract.Requires(vertexFactory != null);
                Contract.Requires(edgeFactory != null);

                this.serializer = serializer;
                this.reader = reader;
                this.visitedGraph = visitedGraph;
                this.vertexFactory = vertexFactory;
                this.edgeFactory = edgeFactory;
            }

            public GraphMLDeserializer<TVertex, TEdge, TGraph> Serializer
            {
                get { return this.serializer; }
            }

            public XmlReader Reader
            {
                get { return this.reader; }
            }

            public TGraph VisitedGraph
            {
                get { return this.visitedGraph; }
            }

            public void Deserialize()
            {
                this.ReadHeader();
                this.ReadGraphHeader();
                this.ReadElements();
            }

            private void ReadHeader()
            {
                // read flow until we hit the graphml node
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                        reader.Name == "graphml")
                    {
                        this.graphMLNamespace = reader.NamespaceURI;
                        return;
                    }
                }

                throw new ArgumentException("graphml node not found");
            }

            private void ReadGraphHeader()
            {
                if (!this.Reader.ReadToDescendant("graph", this.graphMLNamespace))
                    throw new ArgumentException("graph node not found");
            }

            private void ReadElements()
            {
                Contract.Requires(
                    this.Reader.Name == "graph" &&
                    this.Reader.NamespaceURI == this.graphMLNamespace,
                    "incorrect reader position");

                ReadDelegateCompiler.SetGraphDefault(this.VisitedGraph);

                var vertices = new Dictionary<string, TVertex>(StringComparer.Ordinal);

                // read vertices or edges
                var reader = this.Reader;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                        reader.NamespaceURI == this.graphMLNamespace)
                    {
                        switch (reader.Name)
                        {
                            case "node":
                                this.ReadVertex(vertices);
                                break;
                            case "edge":
                                this.ReadEdge(vertices);
                                break;
                            case "data":
                                GraphMLDeserializer<TVertex, TEdge, TGraph>.ReadDelegateCompiler.GraphAttributesReader(this.Reader, this.graphMLNamespace, this.VisitedGraph);
                                break;
                            default:
                                throw new InvalidOperationException(String.Format("invalid reader position {0}:{1}", this.Reader.NamespaceURI, this.Reader.Name));
                        }
                    }
                }
            }

            private void ReadEdge(Dictionary<string, TVertex> vertices)
            {
                Contract.Requires(vertices != null);
                Contract.Assert(
                    this.Reader.NodeType == XmlNodeType.Element &&
                    this.Reader.Name == "edge" &&
                    this.Reader.NamespaceURI == this.graphMLNamespace);

                // get subtree
                using (var subReader = this.Reader.ReadSubtree())
                {
                    // read id
                    string id = ReadAttributeValue(this.Reader, "id");
                    string sourceid = ReadAttributeValue(this.Reader, "source");
                    TVertex source;
                    if (!vertices.TryGetValue(sourceid, out source))
                        throw new ArgumentException("Could not find vertex " + sourceid);
                    string targetid = ReadAttributeValue(this.Reader, "target");
                    TVertex target;
                    if (!vertices.TryGetValue(targetid, out target))
                        throw new ArgumentException("Could not find vertex " + targetid);

                    var edge = this.edgeFactory(source, target, id);
                    ReadDelegateCompiler.SetEdgeDefault(edge);

                    // read data
                    while (subReader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element &&
                            reader.Name == "data" &&
                            reader.NamespaceURI == this.graphMLNamespace)
                            ReadDelegateCompiler.EdgeAttributesReader(subReader, this.graphMLNamespace, edge);
                    }

                    this.VisitedGraph.AddEdge(edge);
                }
            }

            private void ReadVertex(Dictionary<string, TVertex> vertices)
            {
                Contract.Requires(vertices != null);
                Contract.Assert(
                    this.Reader.NodeType == XmlNodeType.Element &&
                    this.Reader.Name == "node" &&
                    this.Reader.NamespaceURI == this.graphMLNamespace);

                // get subtree
                using (var subReader = this.Reader.ReadSubtree())
                {
                    // read id
                    string id = ReadAttributeValue(this.Reader, "id");
                    // create new vertex
                    TVertex vertex = vertexFactory(id);
                    // apply defaults
                    ReadDelegateCompiler.SetVertexDefault(vertex);
                    // read data
                    while (subReader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element &&
                            reader.Name == "data" &&
                            reader.NamespaceURI == this.graphMLNamespace)
                            ReadDelegateCompiler.VertexAttributesReader(subReader, this.graphMLNamespace, vertex);
                    }
                    // add to graph
                    this.VisitedGraph.AddVertex(vertex);
                    vertices.Add(id, vertex);
                }
            }

            private static string ReadAttributeValue(XmlReader reader, string attributeName)
            {
                Contract.Requires(reader != null);
                Contract.Requires(attributeName != null);
                reader.MoveToAttribute(attributeName);
                if (!reader.ReadAttributeValue())
                    throw new ArgumentException("missing " + attributeName + " attribute");
                return reader.Value;
            }
        }
    }
}
