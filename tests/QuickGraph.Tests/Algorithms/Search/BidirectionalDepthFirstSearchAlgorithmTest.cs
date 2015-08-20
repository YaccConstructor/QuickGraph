using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public class BidirectionalDepthFirstSearchAlgorithmTest
    {
        [TestMethod]
        public void ComputeAll()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
                this.Compute(g);
        }

        [PexMethod]
        public void Compute<TVertex,TEdge>(IBidirectionalGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var dfs = new BidirectionalDepthFirstSearchAlgorithm<TVertex, TEdge>(g);
            dfs.Compute();

            // let's make sure
            foreach (var v in g.Vertices)
            {
                Assert.IsTrue(dfs.VertexColors.ContainsKey(v));
                Assert.AreEqual(dfs.VertexColors[v], GraphColor.Black);
            }
        }
    }
}
