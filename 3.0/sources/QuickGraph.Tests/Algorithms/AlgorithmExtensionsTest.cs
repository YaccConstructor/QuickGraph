using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass, PexClass(typeof(AlgorithmExtensions))]
    public partial class AlgorithmExtensionsTest
    {
        [TestMethod]
        public void AdjacencyGraphRoots()
        {
            var g = new AdjacencyGraph<string, Edge<string>>();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");

            g.AddEdge(new Edge<string>("A", "B"));
            g.AddEdge(new Edge<string>("B", "C"));

            var roots = g.Roots().ToList();
            Assert.AreEqual(1, roots.Count);
            Assert.AreEqual("A", roots[0]);
        }

        [TestMethod]
        public void AllAdjacencyGraphRoots()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
                Roots(g);
        }

        [PexMethod]
        public void Roots<T>(IVertexAndEdgeListGraph<T, Edge<T>> g)
        {
            var roots = g.Roots().ToList();
            var notRoots = new HashSet<T>();
            foreach (var edge in g.Edges)
                notRoots.Add(edge.Target);

            Console.WriteLine("{0} roots:", roots.Count);
            foreach(var root in roots)
                Console.WriteLine(root);
            Assert.AreEqual(g.VertexCount - notRoots.Count, roots.Count);
            foreach (var v in g.Vertices)
                Assert.AreEqual(notRoots.Contains(v), !roots.Contains(v));
        }
    }
}
