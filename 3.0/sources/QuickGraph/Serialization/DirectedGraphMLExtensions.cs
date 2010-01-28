using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms;
using QuickGraph.Serialization.DirectedGraphML;
using System.Xml.Serialization;

namespace QuickGraph.Serialization
{
    /// <summary>
    /// Directed Graph Markup Language extensions
    /// </summary>
    public static class DirectedGraphMLExtensions
    {
        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexIdentities"></param>
        /// <param name="_formatNode"></param>
        /// <param name="_formatEdge"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
        TGraph visitedGraph,
        VertexIdentity<TVertex> vertexIdentities)
            where TEdge : IEdge<TVertex>
            where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexIdentities != null);
            return ToDirectedGraphML<TVertex, TEdge, TGraph>(visitedGraph, vertexIdentities, null, null);
        }

        /// <summary>
        /// Populates a DGML graph from a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexIdentities"></param>
        /// <param name="_formatNode"></param>
        /// <param name="_formatEdge"></param>
        /// <returns></returns>
        public static DirectedGraph ToDirectedGraphML<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
        TGraph visitedGraph,
        VertexIdentity<TVertex> vertexIdentities,
        Action<TVertex, DirectedGraphNode> _formatNode,
        Action<TEdge, DirectedGraphLink> _formatEdge)
            where TEdge : IEdge<TVertex>
            where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexIdentities != null);

            var algorithm = new DirectedGraphMLAlgorithm<TVertex, TEdge, TGraph>(
                visitedGraph, 
                vertexIdentities);
            if (_formatNode != null)
                algorithm.FormatNode += _formatNode;
            if (_formatEdge != null)
                algorithm.FormatEdge += _formatEdge;
            algorithm.Compute();

            return algorithm.DirectedGraph;
        }

    }
}
