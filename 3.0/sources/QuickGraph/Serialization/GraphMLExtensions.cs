using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace QuickGraph.Serialization
{
    public static class GraphMLExtensions
    {
        public static void SerializeToGraphML<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> graph,
            XmlWriter writer)
            where TVertex : IIdentifiable
            where TEdge : IIdentifiable, IEdge<TVertex>
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            if (writer == null)
                throw new ArgumentNullException("writer");

            var serializer = new GraphMLSerializer<TVertex, TEdge>();
            serializer.Serialize(writer, graph);
        }

        public static void DeserializeFromGraphML<TVertex, TEdge>(
            this IMutableVertexAndEdgeListGraph<TVertex, TEdge> graph,
            XmlReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TVertex : IIdentifiable
            where TEdge : IIdentifiable, IEdge<TVertex>
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            var serializer = new GraphMLSerializer<TVertex, TEdge>();
            serializer.Deserialize(reader, graph, vertexFactory, edgeFactory);
        }

        public static void DeserializeAndValidateFromGraphML<TVertex, TEdge>(
            this IMutableVertexAndEdgeListGraph<TVertex, TEdge> graph,
            TextReader reader,
            IdentifiableVertexFactory<TVertex> vertexFactory,
            IdentifiableEdgeFactory<TVertex, TEdge> edgeFactory
            )
            where TVertex : IIdentifiable
            where TEdge : IIdentifiable, IEdge<TVertex>
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            var serializer = new GraphMLSerializer<TVertex, TEdge>();
            var settings = new XmlReaderSettings();
            // add graphxml schema
            using (var xsdStream = typeof(GraphMLExtensions).Assembly.GetManifestResourceStream(typeof(GraphMLExtensions), "graphml.xsd"))
            using (var xsdReader = XmlReader.Create(xsdStream))
                settings.Schemas.Add(GraphMLSerializer<TVertex, TEdge>.GraphMLNamespace, xsdReader);
            settings.ValidationType = ValidationType.Schema;

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

        static void ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if(e.Severity == XmlSeverityType.Error)
                throw new InvalidOperationException(e.Message);
        }
    }
}
