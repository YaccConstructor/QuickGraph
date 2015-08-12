using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms;

namespace QuickGraph.Serialization
{
    public static class GraphMLExtensions
    {
        public static void SerializeToGraphML<TVertex, TEdge,TGraph>(
#if !NET20
            this 
#endif
            TGraph graph,
            XmlWriter writer,
            VertexIdentity<TVertex> vertexIdentities,
            EdgeIdentity<TVertex, TEdge> edgeIdentities)
            where TEdge : IEdge<TVertex>
            where TGraph : IEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(writer != null);

            var serializer = new GraphMLSerializer<TVertex, TEdge,TGraph>();
            serializer.Serialize(writer, graph, vertexIdentities, edgeIdentities);
        }

        public static void SerializeToGraphML<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
            TGraph graph,
            XmlWriter writer)
            where TEdge : IEdge<TVertex>
            where TGraph : IEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(writer != null);

            var vertexIdentity = AlgorithmExtensions.GetVertexIdentity<TVertex>(graph);
            var edgeIdentity = AlgorithmExtensions.GetEdgeIdentity<TVertex, TEdge>(graph);

            SerializeToGraphML<TVertex, TEdge, TGraph>(
                graph,
                writer,
                vertexIdentity,
                edgeIdentity
                );
        }

#if !SILVERLIGHT
        public static void DeserializeFromGraphML<TVertex, TEdge,TGraph>(
#if !NET20
            this 
#endif
            TGraph graph,
            TextReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(reader != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            var settings = new XmlReaderSettings();
#if !SILVERLIGHT
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.ValidationFlags = XmlSchemaValidationFlags.None;
#endif
            settings.XmlResolver = new GraphMLXmlResolver();

            using(var xreader = XmlReader.Create(reader, settings))
                DeserializeFromGraphML<TVertex, TEdge,TGraph>(graph, xreader, vertexFactory, edgeFactory);
        }

        public static void DeserializeFromGraphML<TVertex, TEdge,TGraph>(
#if !NET20
            this 
#endif
            TGraph graph,
            XmlReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(reader != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            var serializer = new GraphMLDeserializer<TVertex, TEdge,TGraph>();
            serializer.Deserialize(reader, graph, vertexFactory, edgeFactory);
        }

        public static void DeserializeAndValidateFromGraphML<TVertex, TEdge,TGraph>(
#if !NET20
            this 
#endif
            TGraph graph,
            TextReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(reader != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

			DeserializeAndValidateFromGraphML(graph, XmlReader.Create(reader), vertexFactory, edgeFactory);
        }

		public static void DeserializeAndValidateFromGraphML<TVertex, TEdge,TGraph>(
#if !NET20
            this
#endif
            TGraph graph,
            XmlReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(graph != null);
            Contract.Requires(reader != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            var serializer = new GraphMLDeserializer<TVertex, TEdge,TGraph>();
            var settings = new XmlReaderSettings();
            // add graphxml schema
            settings.ValidationType = ValidationType.Schema;
            settings.XmlResolver = new GraphMLXmlResolver();
            AddGraphMLSchema<TVertex, TEdge, TGraph>(settings);

            try
            {
                settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(ValidationEventHandler);

                // reader and validating
                using (var xreader = XmlReader.Create(reader, settings))
                    serializer.Deserialize(xreader, graph, vertexFactory, edgeFactory);
            }
            finally
            {
                settings.ValidationEventHandler -= new System.Xml.Schema.ValidationEventHandler(ValidationEventHandler);
            }
        }

        private static void AddGraphMLSchema<TVertex, TEdge,TGraph>(XmlReaderSettings settings)
            where TEdge : IEdge<TVertex>
            where TGraph : IEdgeListGraph<TVertex, TEdge>
        {
            using (var xsdStream = typeof(GraphMLExtensions).Assembly.GetManifestResourceStream(typeof(GraphMLExtensions), "graphml.xsd"))
            using (var xsdReader = XmlReader.Create(xsdStream, settings))
                settings.Schemas.Add(GraphMLXmlResolver.GraphMLNamespace, xsdReader);
        }

        static void ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if(e.Severity == XmlSeverityType.Error)
                throw new InvalidOperationException(e.Message);
        }
#endif
    }
}
