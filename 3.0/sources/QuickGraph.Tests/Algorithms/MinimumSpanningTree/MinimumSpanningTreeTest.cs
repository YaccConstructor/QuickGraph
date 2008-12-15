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
        public void KruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                foreach (var v in g.Vertices)
                {
                    Console.WriteLine("start from {0}", v);
                    Kruskal(g, v);
                }
        }

        private static void Kruskal<TVertex,TEdge>(IUndirectedGraph<TVertex, TEdge> g, TVertex v)
            where TEdge : IEdge<TVertex>
        {
            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var kruskalTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(kruskal, kruskalTree))
                kruskal.Compute(v);

            Console.WriteLine("kruskal cost: {0}", Cost(kruskalTree.VertexPredecessors));
            AssertSpanningTree<TVertex, TEdge>(g, kruskalTree.VertexPredecessors);
        }

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

            Console.WriteLine("prim cost: {0}", Cost(primTree.VertexPredecessors));
            Console.WriteLine("kruskal cost: {0}", Cost(kruskalTree.VertexPredecessors));
            AssertSpanningTree<TVertex, TEdge>(g, primTree.VertexPredecessors);
            AssertSpanningTree<TVertex, TEdge>(g, kruskalTree.VertexPredecessors);
            AssertAreEqual(primTree.VertexPredecessors, kruskalTree.VertexPredecessors);
        }

        private static void AssertSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> g, 
            IDictionary<TVertex, TEdge> tree)
            where TEdge : IEdge<TVertex>
        {
            var spanned = new Dictionary<TVertex, TEdge>();
            Console.WriteLine("tree:");
            foreach (var e in tree)
            {
                Console.WriteLine("\t{0}", e);
                spanned[e.Value.Source] = spanned[e.Value.Target] = default(TEdge);
            }

            foreach (var v in g.Vertices)
                Assert.IsTrue(spanned.ContainsKey(v), "{0} not in tree", v);
        }

        private static double Cost<TVertex,TEdge>(IDictionary<TVertex, TEdge> tree)
        {
            return tree.Count;
        }

        private static void AssertAreEqual<TVertex, TEdge>(IDictionary<TVertex, TEdge> left, IDictionary<TVertex, TEdge> right)
            where TEdge : IEdge<TVertex>
        {
            try
            {
                Assert.AreEqual(left.Count, right.Count);
                foreach (var kv in left)
                    Assert.AreEqual(kv.Value, right[kv.Key]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Count: {0} - {1}", left.Count, right.Count);
                foreach (var kv in left)
                {
                    TEdge e;
                    Assert.AreEqual("{0} - {1}", kv.Value, right.TryGetValue(kv.Key, out e) ? e.ToString() : "missing"  );
                }

                throw new AssertFailedException("comparison failed", ex);
            }
        }
    }
}
