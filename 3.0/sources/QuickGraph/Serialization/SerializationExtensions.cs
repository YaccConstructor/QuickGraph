using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics.Contracts;
using System.Xml.XPath;

namespace QuickGraph.Serialization
{
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes the graph to the stream using the .Net serialization binary format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        public static void SerializeToBinary<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IGraph<TVertex, TEdge> graph,
            Stream stream)
            where TEdge : IEdge<TVertex>
        {            
            Contract.Requires(graph != null);
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanWrite);

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, graph);
        }

        /// <summary>
        /// Deserializes a graph instance from a stream that was serialized using the .Net serialization binary format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static TGraph DeserializeFromBinary<TVertex, TEdge, TGraph>(
#if !NET20
            this 
#endif
            Stream stream)
            where TGraph : IGraph<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanRead);

            var formatter = new BinaryFormatter();
            var result = formatter.Deserialize(stream);
            return (TGraph)result;
        }

        /// <summary>
        /// Deserializes a graph from a generic xml stream, using an <see cref="XPathDocument"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
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
    }
}
