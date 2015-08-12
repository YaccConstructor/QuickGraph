using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    /// <summary>
    /// Helper extensions to render graphs to graphviz
    /// </summary>
    public static class GraphvizExtensions
    {
        /// <summary>
        /// Renders a graph to the Graphviz DOT format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string ToGraphviz<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph
            )
            where TEdge : IEdge<TVertex>
        {
            var algorithm = new GraphvizAlgorithm<TVertex, TEdge>(graph);
            return algorithm.Generate();
        }

        /// <summary>
        /// Renders a graph to the Graphviz DOT format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="initialization">delegate that initializes the algorithm</param>
        /// <returns></returns>
        public static string ToGraphviz<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph,
            Action<GraphvizAlgorithm<TVertex, TEdge>> initialization
            )
            where TEdge : IEdge<TVertex>
        {
            var algorithm = new GraphvizAlgorithm<TVertex, TEdge>(graph);
            initialization(algorithm);
            return algorithm.Generate();
        }
    }
}
