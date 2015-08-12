using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph.Tests
{
    /// <summary>
    /// A collection of utility methods for unit tests to use
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Create the set of all Edges {(left, right)}
        /// such that every element in leftVertices is matched with every element in rightVertices
        /// This is especially useful for creating bipartite graphs
        /// </summary>
        /// <param name="leftVertices">A collection of vertices</param>
        /// <param name="rightVertices">A collection of vertices</param>
        /// <param name="edgeFactory">An object to use for creating edges</param>
        /// <returns></returns>
        public static List<Edge<TVertex>> CreateAllPairwiseEdges<TVertex>(
            IEnumerable<TVertex> leftVertices, IEnumerable<TVertex> rightVertices,
            EdgeFactory<TVertex, Edge<TVertex>> edgeFactory)
        {
            var edges = new List<Edge<TVertex>>();

            foreach (var left in leftVertices)
            {
                foreach (var right in rightVertices)
                {
                    edges.Add(edgeFactory(left, right));
                }
            }

            return edges;
        }
    }
}
