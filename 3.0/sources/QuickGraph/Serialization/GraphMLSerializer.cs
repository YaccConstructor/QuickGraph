using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Reflection.Emit;
using System.Diagnostics;

namespace QuickGraph.Serialization
{
    /// <summary>
    /// A GraphML ( http://graphml.graphdrawing.org/ ) format serializer.
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <remarks>
    /// <para>
    /// Custom vertex and edge attributes can be specified by 
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
    public sealed class GraphMLSerializer<TVertex,TEdge> 
        : SerializerBase<TVertex,TEdge>
        where TVertex : IIdentifiable
        where TEdge : IIdentifiable, IEdge<TVertex>
    {
        public const string GraphMLNamespace = "http://graphml.graphdrawing.org/xmlns";

        #region Compiler
        private delegate void WriteVertexAttributesDelegate(
            XmlWriter writer,
            TVertex v);
        private delegate void WriteEdgeAttributesDelegate(
            XmlWriter writer,
            TEdge e);
        private delegate void ReadVertexAttributesDelegate(
            XmlReader reader,
            TVertex v);
        private delegate void ReadEdgeAttributesDelegate(
            XmlReader reader,
            TEdge e);

        public static bool MoveNextData(XmlReader reader)
        {
            GraphContracts.AssumeNotNull(reader, "reader");
            return
                reader.NodeType == XmlNodeType.Element &&
                reader.Name == "data" &&
                reader.NamespaceURI == GraphMLNamespace;
        }

        static class ReadDelegateCompiler
        {
            public static readonly ReadVertexAttributesDelegate VertexAttributesReader;
            public static readonly ReadEdgeAttributesDelegate EdgeAttributesReader;

            static ReadDelegateCompiler()
            {
                VertexAttributesReader =
                    (ReadVertexAttributesDelegate)CreateReadDelegate(
                    typeof(ReadVertexAttributesDelegate),
                    typeof(TVertex),
                    "id"
                    );
                EdgeAttributesReader =
                    (ReadEdgeAttributesDelegate)CreateReadDelegate(
                    typeof(ReadEdgeAttributesDelegate),
                    typeof(TEdge),
                    "id", "source", "target"
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
                    GraphContracts.AssumeNotNull(type, "type");
                    bool result = ReadContentMethods.TryGetValue(type, out method);
                    GraphContracts.Assert(!result || method != null, type.FullName);
                    return result;
                }
            }

            public static Delegate CreateReadDelegate(
                Type delegateType,
                Type elementType,
                params string[] ignoredAttributes
                )
            {
                GraphContracts.AssumeNotNull(delegateType, "delegateType");
                GraphContracts.AssumeNotNull(elementType, "elementType");

                var method = new DynamicMethod(
                    "Read"+elementType.Name,
                    typeof(void),
                    new Type[] { typeof(XmlReader), elementType },
                    elementType.Module
                    );
                var gen = method.GetILGenerator();

                LocalBuilder key= gen.DeclareLocal(typeof(string));

                Label start = gen.DefineLabel();
                Label doWhile = gen.DefineLabel();

                gen.Emit(OpCodes.Br, doWhile);
                gen.MarkLabel(start);

                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldstr, "key");
                gen.EmitCall(OpCodes.Callvirt, Metadata.GetAttributeMethod, null);
                gen.Emit(OpCodes.Stloc_0);

                // if (String.Equals(key, "id")) continue;
                foreach (string ignoredAttribute in ignoredAttributes)
                {
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Ldstr, ignoredAttribute);
                    gen.EmitCall(OpCodes.Call, Metadata.StringEqualsMethod, null);
                    gen.Emit(OpCodes.Brtrue, doWhile);
                }

                // we need to create the swicth for each property
                Label next = gen.DefineLabel();
                bool first = true;
                foreach (var kv in SerializationHelper.GetAttributeProperties(elementType))
                {
                    if (!first)
                    {
                        gen.MarkLabel(next);
                        next = gen.DefineLabel();
                    }
                    first = false;

                    // if (!key.Equals("foo"))
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Ldstr, kv.Value);
                    gen.EmitCall(OpCodes.Call, Metadata.StringEqualsMethod,null);
                    // if false jump to next
                    gen.Emit(OpCodes.Brfalse, next);

                    // do our stuff
                    MethodInfo readMethod = null;
                    if (!Metadata.TryGetReadContentMethod(kv.Key.PropertyType, out readMethod))
                        throw new ArgumentException(String.Format("Property {0} has a non-supported type",kv.Key.Name));

                    // do we have a set method ?
                    var setMethod = kv.Key.GetSetMethod();
                    if (setMethod==null)
                        throw new ArgumentException(String.Format("Property {0}.{1} has not set method", kv.Key.DeclaringType, kv.Key.Name));
                    // reader.ReadXXX
                    gen.Emit(OpCodes.Ldarg_1);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "data");
                    gen.Emit(OpCodes.Ldstr, GraphMLNamespace);
                    gen.EmitCall(OpCodes.Callvirt, readMethod, null);
                    gen.EmitCall(OpCodes.Callvirt, setMethod, null);

                    // jump to do while
                    gen.Emit(OpCodes.Br, doWhile);
                }

                // we don't know this parameter.. we throw
                gen.MarkLabel(next);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Newobj, Metadata.ArgumentExceptionCtor);
                gen.Emit(OpCodes.Throw);

                gen.MarkLabel(doWhile);
                gen.Emit(OpCodes.Ldarg_0);
                gen.EmitCall(OpCodes.Call, new Func<XmlReader, bool>(MoveNextData).Method ,null);

//                gen.Emit(OpCodes.Ldstr, "data");
  //              gen.Emit(OpCodes.Ldstr, GraphMLNamespace);
    //            gen.EmitCall(OpCodes.Callvirt, Metadata.ReadToFollowingMethod,null);
                gen.Emit(OpCodes.Brtrue, start);

                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }
        }

        static class WriteDelegateCompiler
        {
            public static readonly WriteVertexAttributesDelegate VertexAttributesWriter;
            public static readonly WriteEdgeAttributesDelegate EdgeAttributesWriter;

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
                    GraphContracts.AssumeNotNull(valueType, "valueType");
                    return WriteValueMethods.TryGetValue(valueType, out method);
                }
            }

            public static Delegate CreateWriteDelegate(Type nodeType, Type delegateType)
            {
                GraphContracts.AssumeNotNull(nodeType, "nodeType");
                GraphContracts.AssumeNotNull(delegateType, "delegateType");

                var method = new DynamicMethod(
                    "Write" + delegateType.Name + nodeType.Name,
                    typeof(void),
                    new Type[] { typeof(XmlWriter), nodeType },
                    nodeType.Module
                    );
                var gen = method.GetILGenerator();

                foreach (var kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    var property = kv.Key;
                    var getMethod = property.GetGetMethod();
                    if (getMethod == null)
                        throw new NotSupportedException(String.Format("Property {0}.{1} has not getter", property.DeclaringType, property.Name));
                    MethodInfo writeValueMethod;
                    if (!Metadata.TryGetWriteValueMethod(property.PropertyType, out writeValueMethod))
                        throw new NotSupportedException(String.Format("Property {0}.{1} type is not supported", property.DeclaringType, property.Name));

                    // for each property of the type,
                    // write it to the xmlwriter (we need to take care of value types, etc...)
                    // writer.WriteStartElement("data")
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "data");
                    gen.Emit(OpCodes.Ldstr, GraphMLNamespace);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteStartElementMethod, null);

                    // writer.WriteStartAttribute("key");
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "key");
                    gen.Emit(OpCodes.Ldstr, kv.Value);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteAttributeStringMethod, null);

                    // writer.WriteValue(v.xxx);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldarg_1);
                    gen.EmitCall(OpCodes.Callvirt,getMethod,null);
                    gen.EmitCall(OpCodes.Callvirt, writeValueMethod, null);

                    // writer.WriteEndElement()
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.EmitCall(OpCodes.Callvirt, Metadata.WriteEndElementMethod, null);
                }

                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }
        }
        #endregion

        public void Serialize(
            XmlWriter writer, 
            IVertexAndEdgeSet<TVertex, TEdge> visitedGraph
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            var worker = new WriterWorker(this, writer, visitedGraph);
            worker.Serialize();
        }

        public void Deserialize(
            XmlReader reader,
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

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
            private readonly GraphMLSerializer<TVertex, TEdge> serializer;
            private readonly XmlReader reader;
            private readonly IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph;
            private readonly IdentifiableVertexFactory<TVertex> vertexFactory;
            private readonly IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory;

            public ReaderWorker(
                GraphMLSerializer<TVertex,TEdge> serializer,
                XmlReader reader,
                IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
                IdentifiableVertexFactory<TVertex> vertexFactory,
                IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
                )
            {
                GraphContracts.AssumeNotNull(serializer, "serializer");
                GraphContracts.AssumeNotNull(reader, "reader");
                GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
                GraphContracts.AssumeNotNull(vertexFactory, "vertexFactory");
                GraphContracts.AssumeNotNull(edgeFactory, "edgeFactory");

                this.serializer = serializer;
                this.reader = reader;
                this.visitedGraph = visitedGraph;
                this.vertexFactory = vertexFactory;
                this.edgeFactory = edgeFactory;
            }

            public GraphMLSerializer<TVertex, TEdge> Serializer
            {
                get { return this.serializer; }
            }

            public XmlReader Reader
            {
                get { return this.reader; }
            }

            public IMutableVertexAndEdgeListGraph<TVertex, TEdge> VisitedGraph
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
                 if (!this.Reader.ReadToFollowing("graphml", GraphMLNamespace))
                        throw new ArgumentException("graphml node not found");
            }

            private void ReadGraphHeader()
            {
                if (!this.Reader.ReadToDescendant("graph", GraphMLNamespace))
                    throw new ArgumentException("graph node not found");
            }

            private void ReadElements()
            {
                GraphContracts.Assert(
                    this.Reader.Name == "graph" &&
                    this.Reader.NamespaceURI == GraphMLNamespace,
                    "incorrect reader position");

                var vertices = new Dictionary<string, TVertex>();

                // read vertices or edges
                while (this.Reader.Read())
                {
                    if (this.Reader.NodeType != XmlNodeType.Element)
                        continue;

                    if (this.Reader.Name == "node" &&
                        this.Reader.NamespaceURI == GraphMLNamespace)
                    {
                        // get subtree
                        XmlReader subReader = this.Reader.ReadSubtree();
                        // read id
                        string id = this.ReadAttributeValue("id");
                        // create new vertex
                        TVertex vertex = vertexFactory(id);
                        // read data
                        if (subReader.ReadToFollowing("data", GraphMLNamespace))
                            GraphMLSerializer<TVertex, TEdge>.ReadDelegateCompiler.VertexAttributesReader(subReader, vertex);
                        // add to graph
                        this.VisitedGraph.AddVertex(vertex);
                        vertices.Add(vertex.ID, vertex);
                    }
                    else if (this.Reader.Name == "edge" &&
                            this.Reader.NamespaceURI == GraphMLNamespace)
                    {
                        // get subtree
                        var subReader = reader.ReadSubtree();
                        // read id
                        string id = this.ReadAttributeValue("id");
                        string sourceid = this.ReadAttributeValue("source");
                        TVertex source;
                        if (!vertices.TryGetValue(sourceid, out source))
                            throw new ArgumentException("Could not find vertex " + sourceid);
                        string targetid = this.ReadAttributeValue("target");
                        TVertex target;
                        if (!vertices.TryGetValue(targetid, out target))
                            throw new ArgumentException("Could not find vertex " + targetid);

                        TEdge edge = this.edgeFactory(source, target, id);

                        // read data
                        if (subReader.ReadToFollowing("data", GraphMLNamespace))
                            GraphMLSerializer<TVertex, TEdge>.ReadDelegateCompiler.EdgeAttributesReader(subReader, edge);

                        this.VisitedGraph.AddEdge(edge);
                    }
                    else
                        throw new InvalidOperationException(String.Format("invalid reader position {0}:{1}", this.Reader.NamespaceURI, this.Reader.Name));
                }
            }

            private string ReadAttributeValue(string attributeName)
            {
                this.Reader.MoveToAttribute(attributeName);
                if (!this.Reader.ReadAttributeValue())
                    throw new ArgumentException("missing "+ attributeName +" attribute");
                return this.Reader.Value;
            }
        }

        class WriterWorker
        {
            private readonly GraphMLSerializer<TVertex, TEdge> serializer;
            private readonly XmlWriter writer;
            private readonly IVertexAndEdgeSet<TVertex, TEdge> visitedGraph;

            public WriterWorker(
                GraphMLSerializer<TVertex,TEdge> serializer,
                XmlWriter writer,
                IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            {
                GraphContracts.AssumeNotNull(serializer, "serializer");
                GraphContracts.AssumeNotNull(writer, "writer");
                GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

                this.serializer = serializer;
                this.writer = writer;
                this.visitedGraph = visitedGraph;
            }

            public GraphMLSerializer<TVertex, TEdge> Serializer
            {
                get { return this.serializer; }
            }

            public XmlWriter Writer
            {
                get { return this.writer; }
            }

            public IVertexAndEdgeSet<TVertex, TEdge> VisitedGraph
            {
                get { return this.visitedGraph; }
            }

            public void Serialize()
            {
                this.WriteHeader();
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
                this.Writer.WriteStartElement("", "graphml", GraphMLNamespace);
            }

            private void WriteFooter()
            {
                this.Writer.WriteEndElement();
                this.Writer.WriteEndDocument();
            }

            private void WriteGraphHeader()
            {
                this.Writer.WriteStartElement("graph", GraphMLNamespace);
                this.Writer.WriteAttributeString("id", "G");
                this.Writer.WriteAttributeString("edgedefault",
                    (this.VisitedGraph.IsDirected) ? "directed" : "undirected"
                    );
                this.Writer.WriteAttributeString("parse.nodes", this.VisitedGraph.VertexCount.ToString());
                this.Writer.WriteAttributeString("parse.edges", this.VisitedGraph.EdgeCount.ToString());
                this.Writer.WriteAttributeString("parse.order", "nodefirst");
                this.Writer.WriteAttributeString("parse.nodeids", "free");
                this.Writer.WriteAttributeString("parse.edgeids", "free");
            }

            private void WriteGraphFooter()
            {
                this.Writer.WriteEndElement();
            }

            private void WriteVertexAttributeDefinitions()
            {
                string forNode = "node";
                Type nodeType = typeof(TVertex);

                WriteAttributeDefinitions(forNode, nodeType);
            }

            private void WriteEdgeAttributeDefinitions()
            {
                string forNode = "edge";
                Type nodeType = typeof(TEdge);

                WriteAttributeDefinitions(forNode, nodeType);
            }

            private void WriteAttributeDefinitions(string forNode, Type nodeType)
            {
                GraphContracts.AssumeNotNull(forNode, "forNode");
                GraphContracts.AssumeNotNull(nodeType, "nodeType");

                foreach (var kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    //<key id="d1" for="edge" attr.name="weight" attr.type="double"/>
                    this.Writer.WriteStartElement("key", GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", kv.Value);
                    this.Writer.WriteAttributeString("for", forNode);
                    this.Writer.WriteAttributeString("attr.name", kv.Value);

                    var property = kv.Key;
                    Type propertyType = property.PropertyType;
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
                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteVertices()
            {
                foreach (var v in this.VisitedGraph.Vertices)
                {
                    this.Writer.WriteStartElement("node", GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", v.ID);
                    GraphMLSerializer<TVertex, TEdge>.WriteDelegateCompiler.VertexAttributesWriter(this.Writer, v);
                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteEdges()
            {
                foreach (var e in this.VisitedGraph.Edges)
                {
                    this.Writer.WriteStartElement("edge", GraphMLNamespace);
                    this.Writer.WriteAttributeString("id", e.ID);
                    this.Writer.WriteAttributeString("source", e.Source.ID);
                    this.Writer.WriteAttributeString("target", e.Target.ID);
                    GraphMLSerializer<TVertex, TEdge>.WriteDelegateCompiler.EdgeAttributesWriter(this.Writer, e);
                    this.Writer.WriteEndElement();
                }
            }
        }
    }
}
