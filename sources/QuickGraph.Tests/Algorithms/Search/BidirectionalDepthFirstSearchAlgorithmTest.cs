using System;
using System.Collections.Generic;
using MbUnit.Framework;

namespace QuickGraph.Algorithms.Search
{
    [TypeFixture(typeof(IBidirectionalGraph<string, Edge<string>>))]
    [ProviderFactory(typeof(BidirectionalGraphFactory), typeof(IBidirectionalGraph<string, Edge<string>>))]
    public class BidirectionalDepthFirstSearchAlgorithmTest
    {
        private BidirectionalDepthFirstSearchAlgorithm<string, Edge<string>> dfs;

        [Test]
        public void EmptyGraph(IBidirectionalGraph<string, Edge<string>> g)
        {
            this.dfs = new BidirectionalDepthFirstSearchAlgorithm<string, Edge<string>>(g);
            this.dfs.Compute();

            VerifyDfs();
        }

        private void VerifyDfs()
        {
            // let's make sure
            foreach (string v in dfs.VisitedGraph.Vertices)
            {
                Assert.IsTrue(dfs.VertexColors.ContainsKey(v));
                Assert.AreEqual(dfs.VertexColors[v], GraphColor.Black);
            }
        }
    }
}
