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
    /// A GraphML ( http://graphml.graphdrawing.org/ ) format serializer.
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
    public sealed class GraphMLSerializer<TVertex,TEdge,TGraph> 
        : SerializerBase<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IEdgeListGraph<TVertex, TEdge>
    {
        #region Compiler
        delegate void WriteVertexAttributesDelegate(
            XmlWriter writer,
            TVertex v);
        delegate void WriteEdgeAttributesDelegate(
            XmlWriter writer,
            TEdge e);
        delegate void WriteGraphAttributesDelegate(
            XmlWriter writer,
            TGraph e);

        public static bool MoveNextData(XmlReader reader)
        {
            Contract.Requires(reader != null);
            return
                reader.NodeType == XmlNodeType.Element &&
                reader.Name == "data" &&
                reader.NamespaceURI == GraphMLXmlResolver.GraphMLNamespace;
        }

        static class WriteDelegateCompiler
        {
            public static readonly WriteVertexAttributesDelegate VertexAttributesWriter;
            public static readonly WriteEdgeAttributesDelegate EdgeAttributesWriter;
            public static readonly WriteGraphAttributesDelegate GraphAttributesWriter;

            static WriteDelegateCompiler()
            {
                VertexAttributesWriter =
                    (WriteVertexAttributesDelegate)CreateWriteDelegate(
                        typeof(TVertex),
                        typeof(WriteVertexAttributesDelegate));
                EdgeAttributesWriter =
                    (WriteEdgeAttributesDelegate)CreateWriteDelegate(
                        typeof(TEdge),
                        typeof(WriteEdgeAttributesDelegate)
                        );
                GraphAttributesWriter =
                    (WriteGraphAttributesDelegate)CreateWriteDelegate(
                        typeof(TGraph),
                        typeof(WriteGraphAttributesDelegate)
                    );
            }

            static class Metadata
            {
                public static readonly MethodInfo WriteStartElementMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteStartElement",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                public static readonly MethodInfo WriteEndElementMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteEndElement",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { },
                        null);
                public static readonly MethodInfo WriteStringMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteString",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                public static readonly MethodInfo WriteAttributeStringMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteAttributeString",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                public static readonly MethodInfo WriteStartAttributeMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteStartAttribute",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                public static readonly MethodInfo WriteEndAttributeMethod =
                    typeof(XmlWriter).GetMethod(
                        "WriteEndAttribute",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { },
                        null);
                private static readonly Dictionary<Type, MethodInfo> WriteValueMethods = new Dictionary<Type, MethodInfo>();

                static Metadata()
                {
                    var writer = typeof(XmlWriter);
                    WriteValueMethods.Add(typeof(bool), writer.GetMethod("WriteValue", new Type[] { typeof(bool) }));
                    WriteValueMethods.Add(typeof(int), writer.GetMethod("WriteValue", new Type[] { typeof(int) }));
                    WriteValueMethods.Add(typeof(long), writer.GetMethod("WriteValue", new Type[] { typeof(long) }));
                    WriteValueMethods.Add(typeof(float), writer.GetMethod("WriteValue", new Type[] { typeof(float) }));
                    WriteValueMethods.Add(typeof(double), writer.GetMethod("WriteValue", new Type[] { typeof(double) }));
                    WriteValueMethods.Add(typeof(string), writer.GetMethod("WriteString", new Type[] { typeof(string) }));
                }

                public static bool TryGetWriteValueMethod(Type valueType, out MethodInfo method)
                {
                    Contract.Requires(valueType != null);
                    return WriteValueMethods.TryGetValue(valueType, out method);
                }
            }

            public static Delegate CreateWriteDelegate(Type nodeType, Type delegateType)
            {
                Contract.Requires(nodeType != null);
                Contract.Requires(delegateType != null);

                var method = new DynamicMethod(
                    "Write" + delegateType.Name + nodeType.Name,
                    typeof(void),
                    new Type[] { typeof(XmlWriter), nodeType },
                    nodeType.Module
                    );
                var gen = method.GetILGenerator();
                Label @default = default(Label);

                foreach (var kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    var property = kv.Property;
                    var name = kv.Name;

                    var getMethod = property.GetGetMethod();
                    if (getMethod == null)
                        throw new NotSupportedException(String.Format("Property {0}.{1} has not getter", property.DeclaringType, property.Name));
                    MethodInfo writeValueMethod;
                    if (!Metadata.TryGetWriteValueMethod(property.PropertyType, out writeValueMethod))
                        throw new NotSupportedException(String.Format("Property {0}.{1} type is not supported", property.DeclaringType, property.Name));

                    var defaultValueAttribute =
                        Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute)) 
                        as DefaultValueAttribute;
                    if (defaultValueAttribute != null)
                    {
                        @default = gen.DefineLabel();
                        var value = defaultValueAttribute.Value;
                        if (value != null &&
                            value.GetType() != property.PropertyType)
                            throw new InvalidOperationException("inconsistent default value of property " + property.Name);

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
                        gen.Emit(OpCodes.Ldarg_1);
                        gen.EmitCall(OpCodes.Callvirt, getMethod, null);
                        gen.Emit(OpCodes.Ceq);
                        gen.Emit(OpCodes.Brtrue, @default);
                    }

                    // for each property of the type,
                    // write it to the xmlwriter (we need to take care of value types, etc...)
                    // writer.WriteStartElement("data")
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "data");
                    gen.Emit(OpCodes.Ldstr, GraphMLXmlResolver.GraphMLNamespace);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteStartElementMethod, null);

                    // writer.WriteStartAttribute("key");
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "key");
                    gen.Emit(OpCodes.Ldstr, name);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteAttributeStringMethod, null);

                    // writer.WriteValue(v.xxx);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldarg_1);
                    gen.EmitCall(OpCodes.Callvirt,getMethod,null);
                    gen.EmitCall(OpCodes.Callvirt, writeValueMethod, null);

                    // writer.WriteEndElement()
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteEndElementMethod, null);

                    if (defaultValueAttribute != null)
                    {
                        gen.MarkLabel(@default);
                        @default = default(Label);
                    }
                }

                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }
        }
        #endregion

        public void Serialize(
            XmlWriter writer, 
            TGraph visitedGraph,
            VertexIdentity<TVertex> vertexIdentities,
            EdgeIdentity<TVertex, TEdge> edgeIdentities
            )
        {
            Contract.Requires(writer != null);
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexIdentities != null);
            Contract.Requires(edgeIdentities != null);

            var worker = new WriterWorker(this, writer, visitedGraph, vertexIdentities, edgeIdentities);
            worker.Serialize();
        }

        internal class WriterWorker
        {
            private readonly GraphMLSerializer<TVertex, TEdge,TGraph> serializer;
            private readonly XmlWriter writer;
            private readonly TGraph visitedGraph;
            private readonly VertexIdentity<TVertex> vertexIdentities;
            private readonly EdgeIdentity<TVertex, TEdge> edgeIdentities;

            public WriterWorker(
                GraphMLSerializer<TVertex, TEdge, TGraph> serializer,
                XmlWriter writer,
                TGraph visitedGraph,
                VertexIdentity<TVertex> vertexIdentities,
                EdgeIdentity<TVertex, TEdge> edgeIdentities)
            {
                Contract.Requires(serializer != null);
                Contract.Requires(writer != null);
                Contract.Requires(visitedGraph != null);
                Contract.Requires(vertexIdentities != null);
                Contract.Requires(edgeIdentities != null);

                this.serializer = serializer;
                this.writer = writer;
                this.visitedGraph = visitedGraph;
                this.vertexIdentities = vertexIdentities;
                this.edgeIdentities = edgeIdentities;
            }

            public GraphMLSerializer<TVertex, TEdge, TGraph> Serializer
            {
                get { return this.serializer; }
            }

            public XmlWriter Writer
            {
                get { return this.writer; }
            }

            public TGraph VisitedGraph
            {
                get { return this.visitedGraph; }
            }

            public void Serialize()
            {
                this.WriteHeader();
                this.WriteGraphAttributeDefinitions();
                this.WriteVertexAttributeDefinitions();
                this.WriteEdgeAttributeDefinitions();
                this.WriteGraphHeader();
                this.WriteVertices();
                this.WriteEdges();
                this.WriteGraphFooter();
                this.WriteFooter();
            }

            private void WriteHeader()
            {
                if (this.Serializer.EmitDocumentDeclaration)
                    this.Writer.WriteStartDocument();
                this.Writer.WriteStartElement("", "graphml", GraphMLXmlResolver.GraphMLNamespace);
            }

            private void WriteFooter()
            {
                this.Writer.WriteEndElement();
                this.Writer.WriteEndDocument();
            }

            private void WriteGraphHeader()
            {
                this.Writer.WriteStartElement("graph", GraphMLXmlResolver.GraphMLNamespace);
                this.Writer.WriteAttributeString("id", "G");
                this.Writer.WriteAttributeString("edgedefault",
                    (this.VisitedGraph.IsDirected) ? "directed" : "undirected"
                    );
                this.Writer.WriteAttributeString("parse.nodes", this.VisitedGraph.VertexCount.ToString());
                this.Writer.WriteAttributeString("parse.edges", this.VisitedGraph.EdgeCount.ToString());
                this.Writer.WriteAttributeString("parse.order", "nodesfirst");
                this.Writer.WriteAttributeString("parse.nodeids", "free");
                this.Writer.WriteAttributeString("parse.edgeids", "free");

                GraphMLSerializer<TVertex, TEdge, TGraph>.WriteDelegateCompiler.GraphAttributesWriter(this.Writer, this.VisitedGraph);
            }

            private void WriteGraphFooter()
            {
                this.Writer.WriteEndElement();
            }

            private void WriteGraphAttributeDefinitions()
            {
                string forNode = "graph";
                Type nodeType = typeof(TGraph);

                this.WriteAttributeDefinitions(forNode, nodeType);
            }

            private void WriteVertexAttributeDefinitions()
            {
                string forNode = "node";
                Type nodeType = typeof(TVertex);

                this.WriteAttributeDefinitions(forNode, nodeType);
            }

            private void WriteEdgeAttributeDefinitions()
            {
                string forNode = "edge";
                Type nodeType = typeof(TEdge);

                this.WriteAttributeDefinitions(forNode, nodeType);
            }

            private void WriteAttributeDefinitions(string forNode, Type nodeType)
            {
                Contract.Requires(forNode != null);
                Contract.Requires(nodeType != null);

                foreach (var kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    var property = kv.Property;
                    var name = kv.Name;
                    Type propertyType = property.PropertyType;

                    //<key id="d1" for="edge" attr.name="weight" attr.type="double"/>
                    this.Writer.WriteStartElement("key", GraphMLXmlResolver.GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", name);
                    this.Writer.WriteAttributeString("for", forNode);
                    this.Writer.WriteAttributeString("attr.name", name);

                    switch(Type.GetTypeCode(propertyType))
                    {
                        case TypeCode.Boolean:
                            this.Writer.WriteAttributeString("attr.type", "boolean");
                            break;
                        case TypeCode.Int32:
                            this.Writer.WriteAttributeString("attr.type", "int");
                            break;
                        case TypeCode.Int64:
                            this.Writer.WriteAttributeString("attr.type", "long");
                            break;
                        case TypeCode.Single:
                            this.Writer.WriteAttributeString("attr.type", "float");
                            break;
                        case TypeCode.Double:
                            this.Writer.WriteAttributeString("attr.type", "double");
                            break;
                        case TypeCode.String:
                            this.Writer.WriteAttributeString("attr.type", "string");
                            break;
                        default:
                            throw new NotSupportedException(String.Format("Property type {0}.{1} not supported by the GraphML schema", property.DeclaringType, property.Name));
                    }

                    // <default>...</default>
                    object defaultValue;
                    if (kv.TryGetDefaultValue(out defaultValue))
                    {
                        this.Writer.WriteStartElement("default");
                        switch (Type.GetTypeCode(defaultValue.GetType()))
                        {
                            case TypeCode.Boolean:
                                this.Writer.WriteString(XmlConvert.ToString((bool)defaultValue));
                                break;
                            case TypeCode.Int32:
                                this.Writer.WriteString(XmlConvert.ToString((int)defaultValue));
                                break;
                            case TypeCode.Int64:
                                this.Writer.WriteString(XmlConvert.ToString((long)defaultValue));
                                break;
                            case TypeCode.Single:
                                this.Writer.WriteString(XmlConvert.ToString((float)defaultValue));
                                break;
                            case TypeCode.Double:
                                this.Writer.WriteString(XmlConvert.ToString((double)defaultValue));
                                break;
                            case TypeCode.String:
                                this.Writer.WriteString((string)defaultValue);
                                break;
                            default:
                                throw new NotSupportedException(String.Format("Property type {0}.{1} not supported by the GraphML schema", property.DeclaringType, property.Name));
                        }
                        this.Writer.WriteEndElement();
                    }

                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteVertices()
            {
                foreach (var v in this.VisitedGraph.Vertices)
                {
                    this.Writer.WriteStartElement("node", GraphMLXmlResolver.GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", this.vertexIdentities(v));
                    GraphMLSerializer<TVertex, TEdge,TGraph>.WriteDelegateCompiler.VertexAttributesWriter(this.Writer, v);
                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteEdges()
            {
                foreach (var e in this.VisitedGraph.Edges)
                {
                    this.Writer.WriteStartElement("edge", GraphMLXmlResolver.GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", this.edgeIdentities(e));
                    this.Writer.WriteAttributeString("source", this.vertexIdentities(e.Source));
                    this.Writer.WriteAttributeString("target", this.vertexIdentities(e.Target));
                    GraphMLSerializer<TVertex, TEdge,TGraph>.WriteDelegateCompiler.EdgeAttributesWriter(this.Writer, e);
                    this.Writer.WriteEndElement();
                }
            }
        }
    }
}
