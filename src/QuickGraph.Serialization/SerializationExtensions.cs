using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using System.Xml;
#if !SILVERLIGHT
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.XPath;
using System.Xml.Serialization;
#endif

namespace QuickGraph.Serialization
{
    public static class SerializationExtensions
    {
#if !SILVERLIGHT
        /// <summary>
        /// Deserializes a graph from a generic xml stream, using an <see cref="XPathDocument"/>.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="doc">input xml document</param>
        /// <param name="graphXPath">xpath expression to the graph node. The first node is considered</param>
        /// <param name="verticesXPath">xpath expression from the graph node to the vertex nodes.</param>
        /// <param name="edgesXPath">xpath expression from the graph node to the edge nodes.</param>
        /// <param name="graphFactory">delegate that instantiate the empty graph instance, given the graph node</param>
        /// <param name="vertexFactory">delegate that instantiate a vertex instance, given the vertex node</param>
        /// <param name="edgeFactory">delegate that instantiate an edge instance, given the edge node</param>
        /// <returns></returns>
        public static TGraph DeserializeFromXml<TVertex, TEdge, TGraph>(
#if !NET20
            this 
#endif
            IXPathNavigable doc,
            string graphXPath,
            string verticesXPath,
            string edgesXPath,
            Func<XPathNavigator, TGraph> graphFactory,
            Func<XPathNavigator, TVertex> vertexFactory,
            Func<XPathNavigator, TEdge> edgeFactory
            )
            where TGraph : IMutableVertexAndEdgeSet<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(doc != null);
            Contract.Requires(graphXPath != null);
            Contract.Requires(verticesXPath != null);
            Contract.Requires(edgesXPath != null);
            Contract.Requires(graphFactory != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            var graphNode = doc.CreateNavigator().SelectSingleNode(graphXPath);
            if (graphNode == null)
                throw new ArgumentException("could not find graph node");
            var g = graphFactory(graphNode);
            foreach (XPathNavigator vertexNode in graphNode.Select(verticesXPath))
            {
                var vertex = vertexFactory(vertexNode);
                g.AddVertex(vertex);
            }

            foreach (XPathNavigator edgeNode in graphNode.Select(edgesXPath))
            {
                var edge = edgeFactory(edgeNode);
                g.AddEdge(edge);
            }

            return g;
        }
#endif
        /// <summary>
        /// Deserializes a graph from a generic xml stream, using an <see cref="XmlReader"/>.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="reader">input xml document</param>
        /// <param name="graphPredicate">predicate that returns a value indicating if the current xml node is a graph. The first match is considered</param>
        /// <param name="vertexPredicate">predicate that returns a value indicating if the current xml node is a vertex.</param>
        /// <param name="edgePredicate">predicate that returns a value indicating if the current xml node is an edge.</param>
        /// <param name="graphFactory">delegate that instantiate the empty graph instance, given the graph node</param>
        /// <param name="vertexFactory">delegate that instantiate a vertex instance, given the vertex node</param>
        /// <param name="edgeFactory">delegate that instantiate an edge instance, given the edge node</param>
        /// <returns></returns>
        public static TGraph DeserializeFromXml<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
            XmlReader reader,
            Predicate<XmlReader> graphPredicate,
            Predicate<XmlReader> vertexPredicate,
            Predicate<XmlReader> edgePredicate,
            Func<XmlReader, TGraph> graphFactory,
            Func<XmlReader, TVertex> vertexFactory,
            Func<XmlReader, TEdge> edgeFactory
            )
            where TGraph : class, IMutableVertexAndEdgeSet<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(reader != null);
            Contract.Requires(graphPredicate != null);
            Contract.Requires(vertexPredicate != null);
            Contract.Requires(edgePredicate != null);
            Contract.Requires(graphFactory != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            // find the graph node
            TGraph g = null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element &&
                    graphPredicate(reader))
                {
                    g = graphFactory(reader);
                    break;
                }
            }
            if (g == null)
                throw new ArgumentException("could not find graph node");

            using (var graphReader = reader.ReadSubtree())
            {
                while (graphReader.Read())
                {
                    if (graphReader.NodeType == XmlNodeType.Element)
                    {
                        if (vertexPredicate(graphReader))
                        {

                            var vertex = vertexFactory(graphReader);
                            g.AddVertex(vertex);
                        }
                        else if (edgePredicate(reader))
                        {
                            var edge = edgeFactory(graphReader);
                            g.AddEdge(edge);
                        }
                    }
                }
            }

            return g;
        }

        /// <summary>
        /// Deserializes a graph from a generic xml stream, using an <see cref="XPathDocument"/>.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="reader">input xml document</param>
        /// <param name="namespaceUri">xml namespace</param>
        /// <param name="graphElementName">name of the xml node holding graph information. The first node is considered</param>
        /// <param name="vertexElementName">name of the xml node holding vertex information</param>
        /// <param name="edgeElementName">name of the xml node holding edge information</param>
        /// <param name="graphFactory">delegate that instantiate the empty graph instance, given the graph node</param>
        /// <param name="vertexFactory">delegate that instantiate a vertex instance, given the vertex node</param>
        /// <param name="edgeFactory">delegate that instantiate an edge instance, given the edge node</param>
        /// <returns></returns>
        public static TGraph DeserializeFromXml<TVertex, TEdge, TGraph>(
#if !NET20
            this 
#endif
            XmlReader reader,
            string graphElementName,
            string vertexElementName,
            string edgeElementName,
            string namespaceUri,
            Func<XmlReader, TGraph> graphFactory,
            Func<XmlReader, TVertex> vertexFactory,
            Func<XmlReader, TEdge> edgeFactory
            )
            where TGraph : class, IMutableVertexAndEdgeSet<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(reader != null);
            Contract.Requires(graphElementName != null);
            Contract.Requires(vertexElementName != null);
            Contract.Requires(edgeElementName != null);
            Contract.Requires(graphFactory != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            return DeserializeFromXml(
                reader,
                r => r.Name == graphElementName && r.NamespaceURI == namespaceUri,
                r => r.Name == vertexElementName && r.NamespaceURI == namespaceUri,
                r => r.Name == edgeElementName && r.NamespaceURI == namespaceUri,
                graphFactory,
                vertexFactory,
                edgeFactory
                );
        }

        /// <summary>
        /// Serializes a graph to a generic xml stream, using an <see cref="XmlWriter"/>.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <typeparam name="TGraph">The type of the graph.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="writer">The writer.</param>
        /// <param name="vertexIdentity">The vertex identity.</param>
        /// <param name="edgeIdentity">The edge identity.</param>
        /// <param name="graphElementName">Name of the graph element.</param>
        /// <param name="vertexElementName">Name of the vertex element.</param>
        /// <param name="edgeElementName">Name of the edge element.</param>
        /// <param name="namespaceUri">The namespace URI.</param>
        public static void SerializeToXml<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
            TGraph graph,
            XmlWriter writer,
            VertexIdentity<TVertex> vertexIdentity,
            EdgeIdentity<TVertex, TEdge> edgeIdentity,
            string graphElementName,
            string vertexElementName,
            string edgeElementName,
            string namespaceUri
            )
            where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            SerializeToXml(
                graph,
                writer,
                vertexIdentity,
                edgeIdentity,
                graphElementName,
                vertexElementName,
                edgeElementName,
                namespaceUri,
                null,
                null,
                null);
        }

        /// <summary>
        /// Serializes a graph to a generic xml stream, using an <see cref="XmlWriter"/>.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <typeparam name="TGraph">The type of the graph.</typeparam>
        /// <param name="writer">The writer.</param>
        /// <param name="graph">The graph.</param>
        /// <param name="vertexIdentity">The vertex identity.</param>
        /// <param name="edgeIdentity">The edge identity.</param>
        /// <param name="graphElementName">Name of the graph element.</param>
        /// <param name="vertexElementName">Name of the vertex element.</param>
        /// <param name="edgeElementName">Name of the edge element.</param>
        /// <param name="namespaceUri">The namespace URI (optional).</param>
        /// <param name="writeGraphAttributes">The write graph attributes (optional).</param>
        /// <param name="writeVertexAttributes">The write vertex attributes (optional).</param>
        /// <param name="writeEdgeAttributes">The write edge attributes (optional).</param>
        public static void SerializeToXml<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
            TGraph graph,
            XmlWriter writer,
            VertexIdentity<TVertex> vertexIdentity,
            EdgeIdentity<TVertex, TEdge> edgeIdentity,
            string graphElementName,
            string vertexElementName,
            string edgeElementName,
            string namespaceUri,
            Action<XmlWriter, TGraph> writeGraphAttributes,
            Action<XmlWriter, TVertex> writeVertexAttributes,
            Action<XmlWriter, TEdge> writeEdgeAttributes
            )
            where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);
            Contract.Requires(writer != null);
            Contract.Requires(vertexIdentity != null);
            Contract.Requires(edgeIdentity != null);
            Contract.Requires(!String.IsNullOrEmpty(graphElementName));
            Contract.Requires(!String.IsNullOrEmpty(vertexElementName));
            Contract.Requires(!String.IsNullOrEmpty(edgeElementName));

            writer.WriteStartElement(graphElementName, namespaceUri);
            if (writeGraphAttributes != null)
                writeGraphAttributes(writer, graph);
            foreach (var vertex in graph.Vertices)
            {
                writer.WriteStartElement(vertexElementName, namespaceUri);
                writer.WriteAttributeString("id", namespaceUri, vertexIdentity(vertex));
                if (writeVertexAttributes != null)
                    writeVertexAttributes(writer, vertex);
                writer.WriteEndElement();
            }
            foreach (var edge in graph.Edges)
            {
                writer.WriteStartElement(edgeElementName, namespaceUri);
                writer.WriteAttributeString("id", namespaceUri, edgeIdentity(edge));
                writer.WriteAttributeString("source", namespaceUri, vertexIdentity(edge.Source));
                writer.WriteAttributeString("target", namespaceUri, vertexIdentity(edge.Target));
                if (writeEdgeAttributes != null)
                    writeEdgeAttributes(writer, edge);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
