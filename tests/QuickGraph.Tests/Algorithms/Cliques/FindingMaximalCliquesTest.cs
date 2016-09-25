using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.Cliques;

namespace QuickGraph.Tests.Algorithms.Cliques
{
    [TestClass]
    public class FindingMaximalCliquesTest
    {
        private Graph GenerateGraph()
        {
            var graph = new Graph();
            graph.Edges = new List<Edge>();
            graph.Vertexes = new List<char>();
            graph.Vertexes.AddRange(new[] { 'a', 'b', 'c', 'd', 'e' });
            graph.Edges.Add(new Edge('a', 'b'));
            graph.Edges.Add(new Edge('a', 'c'));
            graph.Edges.Add(new Edge('b', 'c'));
            graph.Edges.Add(new Edge('b', 'd'));
            graph.Edges.Add(new Edge('c', 'd'));
            graph.Edges.Add(new Edge('d', 'e'));
            return graph;
        }

        [TestMethod]
        public void TestFindNeighbors()
        {
            var graph = GenerateGraph();
            FindingMaximalCliques fmc = new FindingMaximalCliques(graph);

            fmc.FindNeighbors();

            var neighbors = fmc.neighbors;
            Assert.AreEqual(neighbors['a'].ToArray().SequenceEqual(new [] { 'b', 'c'}), true);
            Assert.AreEqual(neighbors['b'].ToArray().SequenceEqual(new [] { 'a', 'c', 'd' }), true);
            Assert.AreEqual(neighbors['c'].ToArray().SequenceEqual(new [] { 'a', 'b', 'd' }), true);
            Assert.AreEqual(neighbors['d'].ToArray().SequenceEqual(new [] { 'b', 'c', 'e' }), true);
            Assert.AreEqual(neighbors['e'].ToArray().SequenceEqual(new [] { 'd' }), true);
        }

        [TestMethod]
        public void TestFindCliques()
        {
            var graph = GenerateGraph();
            FindingMaximalCliques fmc = new FindingMaximalCliques(graph);
            fmc.FindNeighbors();
            fmc.Run();
            var cliques = fmc.cliques;
            Assert.AreEqual(cliques.ElementAt(0).ToArray().SequenceEqual(new[] { 'a', 'b', 'c' }), true);
            Assert.AreEqual(cliques.ElementAt(0).ToArray().SequenceEqual(new[] { 'c', 'b', 'd' }), true);
            Assert.AreEqual(cliques.ElementAt(0).ToArray().SequenceEqual(new[] { 'e', 'd' }), true);

        }
    }
}
