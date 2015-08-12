using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms;
using QuickGraph.Serialization.DirectedGraphML;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace QuickGraph.Serialization
{
    /// <summary>
    /// Directed Graph Markup Language extensions
    /// </summary>
    public static class DirectedGraphMLExtensions
    {
        private static XmlSerializer _directedGraphSerializer;
        /// <summary>
        /// Gets the DirectedGraph xml serializer
        /// </summary>
        public static XmlSerializer DirectedGraphSerializer
        {
            get
            {
                if (_directedGraphSerializer == null)
                    _directedGraphSerializer = new XmlSerializer(typeof(DirectedGraph));
                return _directedGraphSerializer;
            }
        }

        /// <summary>
        /// Writes the dgml data structure to the xml writer
        /// </summary>
        /// <param name="fileName"/>
        /// <param name="graph"></param>
        public static void WriteXml(
#if !NET20
this 
#endif
            DirectedGraph graph,
            string fileName)
        {
            Contract.Requires(graph != null); 
            Contract.Requires(!String.IsNullOrEmpty(fileName));
            using (var stream = File.CreateText(fileName))
                WriteXml(graph, stream);
        }

        /// <summary>
        /// Writes the dgml data structure to the xml writer
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="writer"></param>
        public static void WriteXml(
#if !NET20
this 
#endif
            DirectedGraph graph,
            XmlWriter writer)
        {
            Contract.Requires(graph != null);
            Contract.Requires(writer != null);

            DirectedGraphSerializer.Serialize(writer, graph);
        }

        /// <summary>
        /// Writes the dgml data structure to the xml writer
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="stream"></param>
        public static void WriteXml(
#if !NET20
this 
#endif
            DirectedGraph graph,
            Stream stream)
        {
            Contract.Requires(graph != null);
            Contract.Requires(stream != null);

            DirectedGraphSerializer.Serialize(stream, graph);
        }

        /// <summary>
        /// Writes the dgml data structure to the xml writer
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="writer"></param>
        public static void WriteXml(
#if !NET20
this 
#endif
            DirectedGraph graph,
            TextWriter writer)
        {
            Contract.Requires(graph != null);
            Contract.Requires(writer != null);

            DirectedGraphSerializer.Serialize(writer, graph);
        }

        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge>(
#if !NET20
this 
#endif
        IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Ensures(Contract.Result<DirectedGraph>() != null);

            return ToDirectedGraphML<TVertex, TEdge>(
                visitedGraph,
                AlgorithmExtensions.GetVertexIdentity<TVertex>(visitedGraph),
                AlgorithmExtensions.GetEdgeIdentity<TVertex, TEdge>(visitedGraph)
                );
        }

        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexColors"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge>(
#if !NET20
this 
#endif
        IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
        Func<TVertex, GraphColor> vertexColors)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexColors != null);
            Contract.Ensures(Contract.Result<DirectedGraph>() != null);

            return ToDirectedGraphML<TVertex, TEdge>(
                visitedGraph,
                AlgorithmExtensions.GetVertexIdentity<TVertex>(visitedGraph),
                AlgorithmExtensions.GetEdgeIdentity<TVertex, TEdge>(visitedGraph),
                (v, n) =>
                {
                    var color = vertexColors(v);
                    switch(color)
                    {
                        case GraphColor.Black: 
                            n.Background = "Black"; break;
                        case GraphColor.Gray:
                            n.Background = "LightGray"; break;
                        case GraphColor.White:
                            n.Background = "White"; break;
                    }
                },
                null
                );
        }

        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexIdentities"></param>
        /// <param name="edgeIdentities"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge>(
#if !NET20
this 
#endif
        IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
        VertexIdentity<TVertex> vertexIdentities,
        EdgeIdentity<TVertex, TEdge> edgeIdentities)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexIdentities != null);
            Contract.Requires(edgeIdentities != null);
            Contract.Ensures(Contract.Result<DirectedGraph>() != null);

            return ToDirectedGraphML<TVertex, TEdge>(
                visitedGraph, 
                vertexIdentities,
                edgeIdentities, null, null);
        }

        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexIdentities"></param>
        /// <param name="edgeIdentities"></param>
        /// <param name="_formatNode"></param>
        /// <param name="_formatEdge"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge>(
#if !NET20
this 
#endif
        IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
        VertexIdentity<TVertex> vertexIdentities,
        EdgeIdentity<TVertex, TEdge> edgeIdentities,
        Action<TVertex, DirectedGraphNode> _formatNode,
        Action<TEdge, DirectedGraphLink> _formatEdge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexIdentities != null);
            Contract.Requires(edgeIdentities != null);
            Contract.Ensures(Contract.Result<DirectedGraph>() != null);

            var algorithm = new DirectedGraphMLAlgorithm<TVertex, TEdge>(
                visitedGraph, 
                vertexIdentities, 
                edgeIdentities
                );
            if (_formatNode != null)
                algorithm.FormatNode += _formatNode;
            if (_formatEdge != null)
                algorithm.FormatEdge += _formatEdge;
            algorithm.Compute();

            return algorithm.DirectedGraph;
        }


        public static void OpenAsDGML<TVertex, TEdge>(
#if !NET20
this 
#endif
        IVertexAndEdgeListGraph<TVertex, TEdge> graph, string filename)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);

            if (filename == null) filename = "graph.dgml";

            WriteXml(ToDirectedGraphML(graph), filename);
        }
    }
}
