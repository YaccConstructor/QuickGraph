using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Reflection.Emit;

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
        #region Attributes
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

        private static class DelegateCompiler
        {
            private static readonly object syncRoot = new object();
            private static WriteVertexAttributesDelegate writeVertexAttributesDelegate;
            private static WriteEdgeAttributesDelegate writeEdgeAttributesDelegate;
            private static ReadVertexAttributesDelegate readVertexAttributesDelegate;
            private static ReadEdgeAttributesDelegate readEdgeAttributesDelegate;

            public static WriteVertexAttributesDelegate VertexAttributesWriter
            {
                get
                {
                    lock (syncRoot)
                    {
                        if (writeVertexAttributesDelegate == null)
                            DelegateCompiler.CreateWriteDelegates();
                        return writeVertexAttributesDelegate;
                    }
                }
            }

            public static WriteEdgeAttributesDelegate EdgeAttributesWriter
            {
                get
                {
                    lock (syncRoot)
                    {
                        if (writeEdgeAttributesDelegate == null)
                            DelegateCompiler.CreateWriteDelegates();
                        return writeEdgeAttributesDelegate;
                    }
                }
            }

            public static ReadVertexAttributesDelegate VertexAttributesReader
            {
                get
                {
                    lock (syncRoot)
                    {
                        if (readVertexAttributesDelegate == null)
                            DelegateCompiler.CreateReadDelegates();
                        return readVertexAttributesDelegate;
                    }
                }
            }

            public static ReadEdgeAttributesDelegate EdgeAttributesReader
            {
                get
                {
                    lock (syncRoot)
                    {
                        if (readEdgeAttributesDelegate == null)
                            DelegateCompiler.CreateReadDelegates();
                        return readEdgeAttributesDelegate;
                    }
                }
            }

            public static void CreateReadDelegates()
            {
                readVertexAttributesDelegate =
                    (ReadVertexAttributesDelegate)CreateReadDelegate(
                    typeof(ReadVertexAttributesDelegate),
                    typeof(TVertex),
                    "id"
                    );
                readEdgeAttributesDelegate =
                    (ReadEdgeAttributesDelegate)CreateReadDelegate(
                    typeof(ReadEdgeAttributesDelegate),
                    typeof(TEdge),
                    "id", "source", "target"
                    );
            }

            public static void CreateWriteDelegates()
            {
                writeVertexAttributesDelegate =
                    (WriteVertexAttributesDelegate)CreateWriteDelegate(
                        typeof(TVertex),
                        typeof(WriteVertexAttributesDelegate));
                writeEdgeAttributesDelegate =
                    (WriteEdgeAttributesDelegate)CreateWriteDelegate(
                        typeof(TEdge),
                        typeof(WriteEdgeAttributesDelegate)
                        );
            }

            public static Delegate CreateWriteDelegate(Type nodeType, Type delegateType)
            {
                DynamicMethod method = new DynamicMethod(
                    "Write"+delegateType.Name + nodeType.Name,
                    typeof(void),
                    new Type[] { typeof(XmlWriter), nodeType },
                    nodeType.Module
                    );
                ILGenerator gen = method.GetILGenerator();

                MethodInfo writeStartElement =
                    typeof(XmlWriter).GetMethod(
                        "WriteStartElement",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                MethodInfo writeEndElement =
                    typeof(XmlWriter).GetMethod(
                        "WriteEndElement",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { },
                        null);
                MethodInfo writeString =
                    typeof(XmlWriter).GetMethod(
                        "WriteString",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                MethodInfo writeAttributeString =
                    typeof(XmlWriter).GetMethod(
                        "WriteAttributeString",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                MethodInfo toString = typeof(object).GetMethod(
                            "ToString",
                            BindingFlags.Public | BindingFlags.Instance,
                            null,
                            new Type[] { },
                            null);

                foreach (KeyValuePair<PropertyInfo, string> kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    // for each property of the type,
                    // write it to the xmlwriter (we need to take care of value types, etc...)
                    // writer.WriteStartElement("data")
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "data");
                    gen.EmitCall(OpCodes.Callvirt, writeStartElement, null);

                    // writer.WriteAttributeString("key", name);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Ldstr, "key");
                    gen.Emit(OpCodes.Ldstr, kv.Value);
                    gen.EmitCall(OpCodes.Callvirt, writeAttributeString, null);


                    // writer.WriteString(v.xxx);
                    gen.Emit(OpCodes.Ldarg_0);

                    // we now need to load the vertex and invoke the property
                    // load vertex
                    gen.Emit(OpCodes.Ldarg_1);
                    // invoke property
                    MethodInfo getMethod = kv.Key.GetGetMethod();
                    gen.EmitCall(
                        (getMethod.IsVirtual) ? OpCodes.Callvirt : OpCodes.Call,
                        getMethod,
                        null);

                    // since XmlWrite takes to string, we need to convert that
                    // object to a string...
                    // of course, if it's a string, we don't need to do anything
                    Type propertyType = kv.Key.PropertyType;
                    if (propertyType != typeof(string))
                    {
                        // if it's a value type, it has to be boxed before 
                        // invoking ToString
                        if (propertyType.IsValueType)
                            gen.Emit(OpCodes.Box, propertyType);
                        gen.Emit(
                            (toString.IsVirtual) ? OpCodes.Callvirt : OpCodes.Call,
                            toString);
                    }

                    // we now have two string on the stack...
                    gen.EmitCall(OpCodes.Callvirt, writeString, null);

                    // writer.WriteEndElement()
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.EmitCall(OpCodes.Callvirt, writeEndElement, null);
                }

                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }

            public static Delegate CreateReadDelegate(
                Type delegateType,
                Type elementType,
                params string[] ignoredAttributes
                )
            {
                DynamicMethod method = new DynamicMethod(
                    "Read"+elementType.Name,
                    typeof(void),
                    new Type[] { typeof(XmlReader), elementType },
                    elementType.Module
                    );
                ILGenerator gen = method.GetILGenerator();

                MethodInfo readToFollowing =
                    typeof(XmlReader).GetMethod(
                        "ReadToFollowing",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                MethodInfo getAttribute =
                    typeof(XmlReader).GetMethod(
                        "GetAttribute",
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string) },
                        null);
                MethodInfo stringEquals =
                    typeof(string).GetMethod(
                        "op_Equality",
                        BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public,
                        null,
                        new Type[] { typeof(string), typeof(string) },
                        null);
                ConstructorInfo argumentException =
                    typeof(ArgumentException).GetConstructor(new Type[] { });


                // read content as methods
                Dictionary<Type, MethodInfo> readContentMethods = new Dictionary<Type,MethodInfo>();
                readContentMethods.Add(typeof(int), typeof(XmlReader).GetMethod("ReadElementContentAsInt", new Type[] { }));
                readContentMethods.Add(typeof(double), typeof(XmlReader).GetMethod("ReadElementContentAsDouble", new Type[] { }));
                readContentMethods.Add(typeof(bool), typeof(XmlReader).GetMethod("ReadElementContentAsBoolean", new Type[] { }));
                readContentMethods.Add(typeof(DateTime), typeof(XmlReader).GetMethod("ReadElementContentAsDateTime", new Type[] { }));
                readContentMethods.Add(typeof(long), typeof(XmlReader).GetMethod("ReadElementContentAsLong", new Type[] { }));
                readContentMethods.Add(typeof(string), typeof(XmlReader).GetMethod("ReadElementContentAsString", new Type[] { }));


                LocalBuilder key= gen.DeclareLocal(typeof(string));

                Label start = gen.DefineLabel();
                Label doWhile = gen.DefineLabel();

                gen.Emit(OpCodes.Br_S, doWhile);
                gen.MarkLabel(start);
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldstr, "key");
                gen.EmitCall(OpCodes.Callvirt, getAttribute, null);
                gen.Emit(OpCodes.Stloc_0);


                // if (key.Equals("id")) continue;
                foreach (string ignoredAttribute in ignoredAttributes)
                {
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Ldstr, ignoredAttribute);
                    gen.EmitCall(OpCodes.Call, stringEquals, null);
                    gen.Emit(OpCodes.Brtrue_S, doWhile);
                }

                // we need to create the swicth for each property
                Label next = gen.DefineLabel();
                bool first = true;
                foreach (KeyValuePair<PropertyInfo, string> kv in SerializationHelper.GetAttributeProperties(elementType))
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
                    gen.EmitCall(OpCodes.Callvirt, stringEquals,null);
                    // if false jump to next
                    gen.Emit(OpCodes.Brfalse_S, next);

                    // do our stuff
                    MethodInfo readMethod = null;
                    if (!readContentMethods.TryGetValue(kv.Key.PropertyType, out readMethod))
                        throw new ArgumentException(String.Format("Property {0} has a non-supported type",kv.Key.Name));

                    // do we have a set method ?
                    MethodInfo setMethod = kv.Key.GetSetMethod();
                    if (setMethod==null)
                        throw new ArgumentException(
                            String.Format("Property {0} is readonly", kv.Key.Name)
                            );
                    // reader.ReadXXX
                    gen.Emit(OpCodes.Ldarg_1);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.EmitCall(OpCodes.Callvirt, readMethod, null);
                    gen.EmitCall(OpCodes.Callvirt, setMethod, null);

                    // jump to do while
                    gen.Emit(OpCodes.Br_S, doWhile);
                }

                // we don't know this parameter.. we throw
                gen.MarkLabel(next);
                gen.Emit(OpCodes.Newobj, argumentException);
                gen.Emit(OpCodes.Throw);

                gen.MarkLabel(doWhile);
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldstr, "data");
                gen.EmitCall(OpCodes.Callvirt, readToFollowing,null);
                gen.Emit(OpCodes.Brtrue_S, start);

                gen.Emit(OpCodes.Ret);

                //let's bake the method
                return method.CreateDelegate(delegateType);
            }
        }
        #endregion

        public void Serialize(TextWriter writer, IVertexAndEdgeSet<TVertex,TEdge> visitedGraph)
        {
            GraphContracts.AssumeNotNull(writer, "writer");
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            using (var xwriter = new XmlTextWriter(writer))
            {
                xwriter.Formatting = Formatting.Indented;
                Serialize(xwriter, visitedGraph);
            }
        }

        public void Serialize(Stream stream, Encoding encoding, IVertexAndEdgeSet<TVertex,TEdge> visitedGraph)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            using (var xwriter = new XmlTextWriter(stream, encoding))
            {
                xwriter.Formatting = Formatting.Indented;
                Serialize(xwriter, visitedGraph);
            }
        }

        public void Serialize(XmlWriter writer, IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
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
            IIdentifiableVertexFactory<TVertex> vertexFactory,
            IIdentifiableEdgeFactory<TVertex, TEdge> edgeFactory)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            ReaderWorker worker = new ReaderWorker(
                this,
                reader,
                visitedGraph,
                vertexFactory,
                edgeFactory);
            worker.Deserialize();
        }

        class ReaderWorker
        {
            private GraphMLSerializer<TVertex, TEdge> serializer;
            private XmlReader reader;
            private IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph;
            private IIdentifiableVertexFactory<TVertex> vertexFactory;
            private IIdentifiableEdgeFactory<TVertex, TEdge> edgeFactory;

            public ReaderWorker(
                GraphMLSerializer<TVertex,TEdge> serializer,
                XmlReader reader,
                IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
                IIdentifiableVertexFactory<TVertex> vertexFactory,
                IIdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
                )
            {
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
                 if (!this.Reader.ReadToFollowing("graphml"))
                        throw new ArgumentException("graphml node not found");
            }

            private void ReadGraphHeader()
            {
                if (!this.Reader.ReadToDescendant("graph"))
                    throw new ArgumentException("graph node not found");
            }

            private void ReadElements()
            {
                this.Reader.ReadStartElement("graph");

                Dictionary<string, TVertex> vertices = new Dictionary<string, TVertex>();

                // read vertices or edges
                while (this.Reader.Read())
                {
                    if (this.Reader.NodeType == XmlNodeType.Element)
                    {
                        if (this.Reader.Name == "node")
                        {
                            // get subtree
                            XmlReader subReader = this.Reader.ReadSubtree();
                            // read id
                            string id = this.ReadAttributeValue("id");
                            // create new vertex
                            TVertex vertex = vertexFactory.CreateVertex(id);
                            // read data
                            GraphMLSerializer<TVertex, TEdge>.DelegateCompiler.VertexAttributesReader(subReader, vertex);
                            // add to graph
                            this.VisitedGraph.AddVertex(vertex);
                            vertices.Add(vertex.ID, vertex);
                        }
                        else if (this.Reader.Name == "edge")
                        {
                            // get subtree
                            XmlReader subReader = reader.ReadSubtree();
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

                            TEdge edge = this.edgeFactory.CreateEdge(id, source, target);

                            // read data
                            GraphMLSerializer<TVertex, TEdge>.DelegateCompiler.EdgeAttributesReader(subReader, edge);

                            this.VisitedGraph.AddEdge(edge);
                        }
                    }
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

        private sealed class WriterWorker
        {
            private GraphMLSerializer<TVertex, TEdge> serializer;
            private XmlWriter writer;
            private IVertexAndEdgeSet<TVertex, TEdge> visitedGraph;

            public WriterWorker(
                GraphMLSerializer<TVertex,TEdge> serializer,
                XmlWriter writer,
                IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            {
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
                this.Writer.WriteStartElement("graphml");
                this.Writer.WriteAttributeString("xmlns","http://graphml.graphdrawing.org/xmlns"); 
                this.Writer.WriteAttributeString("xmlns:xsi","http://www.w3.org/2001/XMLSchema-instance");
                this.Writer.WriteAttributeString("xsi:schemaLocation", "http://graphml.graphdrawing.org/xmlns  http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd");
            }

            private void WriteFooter()
            {
                this.Writer.WriteEndElement();
                this.Writer.WriteEndDocument();
            }

            private void WriteGraphHeader()
            {
                this.Writer.WriteStartElement("graph");
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
                foreach (KeyValuePair<PropertyInfo, string> kv in SerializationHelper.GetAttributeProperties(nodeType))
                {
                    //<key id="d1" for="edge" attr.name="weight" attr.type="double"/>
                    this.Writer.WriteStartElement("key");
                    this.Writer.WriteAttributeString("id", kv.Value);
                    this.Writer.WriteAttributeString("for", forNode);
                    this.Writer.WriteAttributeString("attr.name", kv.Value);

                    Type propertyType = kv.Key.PropertyType;
                    if (propertyType == typeof(bool))
                        this.Writer.WriteAttributeString("attr.type", "boolean");
                    else if (propertyType == typeof(int))
                        this.Writer.WriteAttributeString("attr.type", "int");
                    else if (propertyType == typeof(long))
                        this.Writer.WriteAttributeString("attr.type", "long");
                    else if (propertyType == typeof(float))
                        this.Writer.WriteAttributeString("attr.type", "float");
                    else if (propertyType == typeof(double))
                        this.Writer.WriteAttributeString("attr.type", "double");
                    else
                        this.Writer.WriteAttributeString("attr.type", "string");
                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteVertices()
            {
                foreach (var v in this.VisitedGraph.Vertices)
                {
                    this.Writer.WriteStartElement("node");
                    this.Writer.WriteAttributeString("id", v.ID);
                    GraphMLSerializer<TVertex, TEdge>.DelegateCompiler.VertexAttributesWriter(this.Writer, v);
                    this.Writer.WriteEndElement();
                }
            }
            
            private void WriteEdges()
            {
                foreach (var e in this.VisitedGraph.Edges)
                {
                    this.Writer.WriteStartElement("edge");
                    this.Writer.WriteAttributeString("id", e.ID);
                    this.Writer.WriteAttributeString("source", e.Source.ID);
                    this.Writer.WriteAttributeString("target", e.Target.ID);
                    GraphMLSerializer<TVertex, TEdge>.DelegateCompiler.EdgeAttributesWriter(this.Writer, e);
                    this.Writer.WriteEndElement();
                }
            }
        }
    }
}
