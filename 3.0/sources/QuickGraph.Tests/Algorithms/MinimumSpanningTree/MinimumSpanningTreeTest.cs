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
using QuickGraph.Msagl;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;

namespace QuickGraph.Tests.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public partial class MinimumSpanningTreeTest
    {
        [TestMethod]
        public void KruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                Kruskal(g);
        }

        [PexMethod]
        public void Kruskal<TVertex,TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach(var e in g.Edges)
                distances[e] = g.AdjacentDegree(e.Source) + 1;

            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => distances[e]);
            AssertMinimumSpanningTree<TVertex, TEdge>(g, kruskal);
        }

        [TestMethod]
        public void PrimMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                Prim(g);
        }

        [PexMethod]
        public void Prim<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
             where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = g.AdjacentDegree(e.Source) + 1;

            var edges = AlgorithmExtensions.MinimumSpanningTreePrim(g, e => distances[e]);
            AssertSpanningTree(g, edges);
        }

        private static void AssertMinimumSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> g, 
            IMinimumSpanningTreeAlgorithm<TVertex, TEdge> algorithm) 
            where TEdge : IEdge<TVertex>
        {
            var edgeRecorder = new EdgeRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, edgeRecorder))
                algorithm.Compute();

            Console.WriteLine("tree cost: {0}", edgeRecorder.Edges.Count);
            AssertSpanningTree<TVertex, TEdge>(g, edgeRecorder.Edges);
        }

        private static void AssertSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex,TEdge> g, 
            IEnumerable<TEdge> tree)
            where TEdge : IEdge<TVertex>
        {
            var spanned = new Dictionary<TVertex, TEdge>();
            Console.WriteLine("tree:");
            foreach (var e in tree)
            {
                Console.WriteLine("\t{0}", e);
                spanned[e.Source] = spanned[e.Target] = default(TEdge);
            }

            // find vertices that are connected to some edge
            Dictionary<TVertex, TEdge> treeable = new Dictionary<TVertex, TEdge>();
            foreach (var e in g.Edges)
                treeable[e.Source] = treeable[e.Target] = e;

            // ensure they are in the tree
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
                    Console.WriteLine(
                        "{0} - {1}", kv.Value, right.TryGetValue(kv.Key, out e) ? e.ToString() : "missing"  );
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
        public double CompareRoot<TVertex, TEdge>(IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = g.AdjacentDegree(e.Source) + 1;

            var prim = new List<TEdge>(g.MinimumSpanningTreePrim(e => distances[e]));
            var kruskal = new List<TEdge>(g.MinimumSpanningTreeKruskal(e => distances[e]));

            var primCost = prim.Sum(e => distances[e]);
            var kruskalCost = kruskal.Sum(e => distances[e]);
            Console.WriteLine("prim cost: {0}", primCost);
            Console.WriteLine("kruskal cost: {0}", kruskalCost);
            if (primCost != kruskalCost)
            {
                GraphConsoleSerializer.DisplayGraph(g);
                Console.WriteLine("prim: {0}", String.Join(", ", Array.ConvertAll(prim.ToArray(), e => e.ToString() + ':' + distances[e])));
                Console.WriteLine("krus: {0}", String.Join(", ", Array.ConvertAll(kruskal.ToArray(), e => e.ToString() + ':' + distances[e])));
                Assert.Fail("cost do not match");
            }

            return kruskalCost;
        }

        [TestMethod]
        [WorkItem(12240)]
        public void Prim12240()
        {
            var g = new UndirectedGraph<int, Edge<int>>();
            // (1,2), (3,2),(3,4),(1,4)
            g.AddVerticesAndEdge(new Edge<int>(1, 2));
            g.AddVerticesAndEdge(new Edge<int>(3, 2));
            g.AddVerticesAndEdge(new Edge<int>(3, 4));
            g.AddVerticesAndEdge(new Edge<int>(1, 4));

            var cost = CompareRoot(g);
            Assert.AreEqual(cost, 3);
        }

        public class WeightedEdge
            : IdentifiableEdge<string>
        {
            public WeightedEdge(string source, string target, string id, int weight)
                :base(source, target, id)
            {
                this.Weight = weight;
            }

            public readonly int Weight;

            public override string ToString()
            {
                return String.Format("{0}: {1}", this.ID, this.Weight);
            }
        }

        [TestMethod]
        [WorkItem(12273)]
        public void Prim12273()
        {
            var doc = new XPathDocument("repro12273.xml");
            var ug = new UndirectedGraph<string, WeightedEdge>();
            foreach (XPathNavigator v in doc.CreateNavigator().Select("graph/node"))
                ug.AddVertex(v.GetAttribute("id", ""));
            foreach (XPathNavigator e in doc.CreateNavigator().Select("graph/edge"))
                ug.AddEdge(new WeightedEdge(
                    e.GetAttribute("source", ""),
                    e.GetAttribute("target", ""),
                    e.GetAttribute("id", ""),
                    int.Parse(e.GetAttribute("weight", ""))
                    )
                );

            //MsaglGraphExtensions.ShowMsaglGraph(ug);
            var prim = new List<WeightedEdge>(ug.MinimumSpanningTreePrim(e => e.Weight));
            var pcost = prim.Sum(e => e.Weight);
            Console.WriteLine("prim cost {0}", pcost);
            foreach(var e in prim)
                Console.WriteLine(e);

            var kruskal = new List<WeightedEdge>(ug.MinimumSpanningTreeKruskal(e => e.Weight));
            var kcost = kruskal.Sum(e => e.Weight);
            Console.WriteLine("kruskal cost {0}", kcost);
            foreach (var e in kruskal)
                Console.WriteLine(e);

            Assert.AreEqual(pcost, 63);
            Assert.AreEqual(pcost, kcost);
        }
    }
}
