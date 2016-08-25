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
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using QuickGraph;
using QuickGraph.Collections;

namespace QuickGraph.Tests.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public partial class MinimumSpanningTreeTest
    {
        private UndirectedGraph<string, TaggedEdge<string, double>> GetUndirectedFullGraph(int vert)
        {
            Console.WriteLine("Start");
            var usedEdge = new List<KeyValuePair<int, int>>();
            var random = new Random();
            var graph = new UndirectedGraph<string, TaggedEdge<string, double>>();
            var trueGraph = new UndirectedGraph<string, TaggedEdge<string, double>>();
            var ds = new ForestDisjointSet<string>(vert);
            for (int i = 0; i < vert; i++)
            {
                graph.AddVertex(i.ToString());
                trueGraph.AddVertex(i.ToString());
                ds.MakeSet(i.ToString());
            }
            for (int i = 0; i < vert; i++)
                for (int j = i + 1; j < vert; j++)
                    graph.AddEdge(new TaggedEdge<string, double>(i.ToString(), j.ToString(), random.Next(100)));
            return graph;
        }
        [TestMethod]
        public void Prim10()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(10);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Prim50()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(50);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Prim100()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(100);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Prim200()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(200);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Prim300()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(300);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Prim400()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(400);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyPrim(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal10()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(10);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal50()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(50);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal100()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(100);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal200()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(200);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal300()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(300);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }
        [TestMethod]
        public void Kruskal400()
        {
            string m = "";
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            var graph = GetUndirectedFullGraph(400);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            MyKruskal(graph, x => x.Tag);
            m += DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            System.Console.Write(m);
        }

        [TestMethod]
        public void KruskalMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                Kruskal(g);
        }

        [PexMethod]
        public void Kruskal<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = g.AdjacentDegree(e.Source) + 1;

            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => distances[e]);
            AssertMinimumSpanningTree<TVertex, TEdge>(g, kruskal);
        }

        [PexMethod]
        public void MyPrim<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g, Func<TEdge, double> edgeWeights)
            where TEdge : IEdge<TVertex>
        {
            var ed = g.Edges.ToList();
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = edgeWeights(e);

            var prim = new PrimMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => distances[e]);
            AssertMinimumSpanningTree<TVertex, TEdge>(g, prim);
        }
        [PexMethod]
        public void MyKruskal<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g, Func<TEdge, double> edgeWeights)
            where TEdge : IEdge<TVertex>
        {
            var ed = g.Edges.ToList();
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = edgeWeights(e);

            var prim = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => distances[e]);
            AssertMinimumSpanningTree<TVertex, TEdge>(g, prim);
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
            using (edgeRecorder.Attach(algorithm))
                algorithm.Compute();

            Console.WriteLine("tree cost: {0}", edgeRecorder.Edges.Count);
            AssertSpanningTree<TVertex, TEdge>(g, edgeRecorder.Edges);
        }

        private static void AssertSpanningTree<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> g,
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

        private static double Cost<TVertex, TEdge>(IDictionary<TVertex, TEdge> tree)
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
                        "{0} - {1}", kv.Value, right.TryGetValue(kv.Key, out e) ? e.ToString() : "missing");
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
                System.Console.Write("cost do not match");
            }

            return kruskalCost;
        }

        [TestMethod]
        [WorkItem(12240)]
        public void Prim12240()
        {
            var g = new UndirectedGraph<int, Edge<int>>();
            g.AddVerticesAndEdge(new Edge<int>(1, 2));
            g.AddVerticesAndEdge(new Edge<int>(3, 2));
            g.AddVerticesAndEdge(new Edge<int>(3, 4));
            g.AddVerticesAndEdge(new Edge<int>(1, 4));

            var cost = CompareRoot(g);
            Assert.AreEqual(9, cost);
        }

        [TestMethod]
        [WorkItem(12240)]
        public void Prim12240WithDelegate()
        {
            var vertices = new int[] { 1, 2, 3, 4 };
            var g = vertices.ToDelegateUndirectedGraph(
                delegate (int v, out IEnumerable<EquatableEdge<int>> ov)
                {
                    switch (v)
                    {
                        case 1: ov = new EquatableEdge<int>[] { new EquatableEdge<int>(1, 2), new EquatableEdge<int>(1, 4) }; break;
                        case 2: ov = new EquatableEdge<int>[] { new EquatableEdge<int>(1, 2), new EquatableEdge<int>(3, 1) }; break;
                        case 3: ov = new EquatableEdge<int>[] { new EquatableEdge<int>(3, 2), new EquatableEdge<int>(3, 4) }; break;
                        case 4: ov = new EquatableEdge<int>[] { new EquatableEdge<int>(1, 4), new EquatableEdge<int>(3, 4) }; break;
                        default: ov = null; break;
                    }
                    return ov != null;
                });
            var cost = CompareRoot(g);
            Assert.AreEqual(9, cost);
        }

        [TestMethod]
        [WorkItem(12273)]
        [DeploymentItem("GraphML/repro12273.xml", "GraphML")]
        public void Prim12273()
        {
            var ug = XmlReader.Create("GraphML/repro12273.xml").DeserializeFromXml(
                "graph", "node", "edge", "",
                reader => new UndirectedGraph<string, TaggedEdge<string, double>>(),
                reader => reader.GetAttribute("id"),
                reader => new TaggedEdge<string, double>(
                    reader.GetAttribute("source"),
                    reader.GetAttribute("target"),
                    int.Parse(reader.GetAttribute("weight"))
                    )
                );

            var prim = ug.MinimumSpanningTreePrim(e => e.Tag).ToList();
            var pcost = prim.Sum(e => e.Tag);
            Console.WriteLine("prim cost {0}", pcost);
            foreach (var e in prim)
                Console.WriteLine(e);

            var kruskal = ug.MinimumSpanningTreeKruskal(e => e.Tag).ToList();
            var kcost = kruskal.Sum(e => e.Tag);
            Console.WriteLine("kruskal cost {0}", kcost);
            foreach (var e in kruskal)
                Console.WriteLine(e);

            Assert.AreEqual(pcost, 63);
            Assert.AreEqual(pcost, kcost);
        }
    }
}
