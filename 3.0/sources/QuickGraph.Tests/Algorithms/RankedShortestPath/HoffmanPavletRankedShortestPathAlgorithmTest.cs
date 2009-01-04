using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using Microsoft.Pex.Framework;
using QuickGraph.Algorithms.RankedShortestPath;

namespace QuickGraph.Tests.Algorithms.RankedShortestPath
{
    [TestClass]
    public partial class HoffmanPavletRankedShortestPathAlgorithmTest
    {
        [TestMethod]
        public void HoffmanPavletRankedShortestPathNetwork()
        {
            // create network graph
            var g = new BidirectionalGraph<int, Edge<int>>();
            var weights = new Dictionary<Edge<int>, double>();
            var data = new int[] {
                1,4,3, //
                4,1,3,

                1,2,1,
                2,1,1,

                2,3,3,
                3,2,3,

                4,5,1,
                5,4,1,

                1,5,2,
                5,1,2,

                2,5,2,
                5,2,3,

                2,6,5,
                6,2,5,

                2,8,2,
                8,2,2,

                6,9,2,
                9,6,2,

                6,8,4,
                8,6,4,

                5,8,2,
                8,5,2,

                5,7,2,
                7,5,2,

                4,7,3,
                7,4,3,

                7,8,4,
                8,7,4,

                9,8,5
            };
            int i = 0;
            for (; i < data.Length; i+=3)
            {
                Edge<int> edge = new Edge<int>(data[i * 3 + 0], data[i * 3 + 1]);
                g.AddVerticesAndEdge(edge);
                weights[edge] = data[i * 3 + 2];
            }
            Assert.AreEqual(data.Length, i);
        }

        [PexMethod]
        public IEnumerable<IEnumerable<TEdge>> HoffmanPavletRankedShortestPath<TVertex,TEdge>(
            [PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g,
            [PexAssumeNotNull]Dictionary<TEdge, double> edgeWeights,
            TVertex rootVertex,
            TVertex goalVertex,
            int pathCount
            )
            where TEdge : IEdge<TVertex>
        {
            var target = new HoffmanPavletRankedShortestPathAlgorithm<TVertex, TEdge>(g, e => edgeWeights[e]);
            target.ShortestPathCount = pathCount;
            target.Compute(rootVertex, goalVertex);

            return target.ComputedShortestPaths;
        }
    }
}
