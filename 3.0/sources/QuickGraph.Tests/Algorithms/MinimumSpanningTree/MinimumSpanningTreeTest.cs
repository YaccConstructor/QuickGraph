using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.MinimumSpanningTree;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public partial class MinimumSpanningTreeTest
    {
        [TestMethod]
        public void PrimKruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                this.Compare(g);
        }

        [PexMethod]
        public void Compare<TVertex, TEdge>(IUndirectedGraph<TVertex, TEdge> g)
            where TEdge :IEdge<TVertex>
        {
            foreach (var v in g.Vertices)
            {
                CompareRoot<TVertex, TEdge>(g, v);
            }
        }

        private void CompareRoot<TVertex, TEdge>(IUndirectedGraph<TVertex, TEdge> g, TVertex v) where TEdge : IEdge<TVertex>
        {
            var prim = new PrimMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var primTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(prim, primTree))
                prim.Compute(v);

            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var kruskalTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(kruskal, kruskalTree))
                kruskal.Compute(v);

            AssertAreEqual(primTree.VertexPredecessors, kruskalTree.VertexPredecessors);
        }

        private void AssertAreEqual<TVertex, TEdge>(IDictionary<TVertex, TEdge> left, IDictionary<TVertex, TEdge> right)
            where TEdge : IEdge<TVertex>
        {
            Assert.AreEqual(left.Count, right.Count);
            foreach (var kv in left)
                Assert.AreEqual(kv.Value, right[kv.Key]);
        }
    }
}
