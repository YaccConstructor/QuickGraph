using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.MinimumSpanningTree;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public partial class MinimumSpanningTreeTest
    {
        [TestMethod]
        public void KruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
            {
                GraphConsoleSerializer.DisplayGraph(g);
                Kruskal(g);
            }
        }

        private static void Kruskal<TVertex,TEdge>(IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            kruskal.ExamineEdge += new EdgeEventHandler<TVertex, TEdge>((sender, e) =>
            {
                Console.WriteLine("\texamining {0}", e.Edge);
            });
            kruskal.TreeEdge += new EdgeEventHandler<TVertex, TEdge>((sender, e) =>
            {
                Console.WriteLine("\ttree edge {0}", e.Edge);
            });
            AssertMinimumSpanningTree<TVertex, TEdge>(g, kruskal);
        }

        private static void AssertMinimumSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> g, 
            IMinimumSpanningTreeAlgorithm<TVertex, TEdge> kruskal) 
            where TEdge : IEdge<TVertex>
        {
            var kruskalTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(kruskal, kruskalTree))
                kruskal.Compute();

            Console.WriteLine("kruskal cost: {0}", Cost(kruskalTree.VertexPredecessors));
            AssertSpanningTree<TVertex, TEdge>(g, kruskalTree.VertexPredecessors);
        }

        private static void AssertSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex,TEdge> g, 
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

            // find vertices that are connected to some edge
            Dictionary<TVertex, TEdge> treeable = new Dictionary<TVertex, TEdge>();
            foreach (var e in g.Edges)
                treeable[e.Source] = treeable[e.Target] = e;

            foreach (var v in treeable.Keys)
                Assert.IsTrue(spanned.ContainsKey(v), "{0} not in tree", v);
        }

        private static double Cost<TVertex,TEdge>(IDictionary<TVertex, TEdge> tree)
        {
            return tree.Count;
        }

        private static void AssertAreEqual<TVertex, TEdge>(
            IDictionary<TVertex, TEdge> left, 
            IDictionary<TVertex, TEdge> right)
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

        [TestMethod]
        public void PrimKruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                this.CompareRoot(g);
        }

        [PexMethod]
        public void CompareRoot<TVertex, TEdge>(IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var prim = new PrimMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var primTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(prim, primTree))
                prim.Compute();

            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var kruskalTree = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(kruskal, kruskalTree))
                kruskal.Compute();

            Console.WriteLine("prim cost: {0}", Cost(primTree.VertexPredecessors));
            Console.WriteLine("kruskal cost: {0}", Cost(kruskalTree.VertexPredecessors));
            AssertAreEqual(primTree.VertexPredecessors, kruskalTree.VertexPredecessors);
        }

    }
}
