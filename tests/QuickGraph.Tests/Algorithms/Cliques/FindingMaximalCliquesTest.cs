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
        [TestMethod]
        public void TestFindCliques()
        {
            var graph = new UndirectedGraph<char, EquatableEdge<char>>();
            graph.AddVertexRange(new[] { 'a', 'b', 'c', 'd', 'e' });
            graph.AddEdge(new EquatableEdge<char>('a', 'b'));
            graph.AddEdge(new EquatableEdge<char>('a', 'c'));
            graph.AddEdge(new EquatableEdge<char>('b', 'c'));
            graph.AddEdge(new EquatableEdge<char>('b', 'd'));
            graph.AddEdge(new EquatableEdge<char>('c', 'd'));
            graph.AddEdge(new EquatableEdge<char>('d', 'e'));
            FindingMaximalCliques<char> fmc = new FindingMaximalCliques<char>(graph);
            var cliques = fmc.FindCliques();
            Assert.AreEqual(cliques.ElementAt(0).ToArray().SequenceEqual(new[] { 'a', 'b', 'c' }), true);
            Assert.AreEqual(cliques.ElementAt(1).ToArray().SequenceEqual(new[] { 'b', 'c', 'd' }), true);
            Assert.AreEqual(cliques.ElementAt(2).ToArray().SequenceEqual(new[] { 'd', 'e' }), true);
        }

        [TestMethod]
        public void TestMultigraph()
        {
            var graph = new UndirectedGraph<char, EquatableEdge<char>>(true);
            graph.AddVertexRange(new[] { 'a', 'b' });
            graph.AddEdge(new EquatableEdge<char>('a', 'b'));
            graph.AddEdge(new EquatableEdge<char>('a', 'b'));
            var fmc = new FindingMaximalCliques<char>(graph);
            var cliques = fmc.FindCliques();
            Assert.AreEqual(cliques.Count == 1, true);
        }

        [TestMethod]
        public void TestPseudograph()
        {
            var graph = new UndirectedGraph<char, EquatableEdge<char>>(true);
            graph.AddVertexRange(new[] { 'a', 'b' });
            graph.AddEdge(new EquatableEdge<char>('a', 'a'));
            graph.AddEdge(new EquatableEdge<char>('a', 'b'));
            FindingMaximalCliques<char> fmc = new FindingMaximalCliques<char>(graph);
            var cliques = fmc.FindCliques();
            Assert.AreEqual(cliques.ElementAt(0).ToArray().SequenceEqual(new[] { 'a', 'b'}), true);
        }

        [TestMethod]
        public void TestEmptyGraph()
        {
            var graph = new UndirectedGraph<char, EquatableEdge<char>>(true);
            FindingMaximalCliques<char> fmc = new FindingMaximalCliques<char>(graph);
            var cliques = fmc.FindCliques();
            Assert.AreEqual(cliques.Count > 0, false);
        }

        [TestMethod]
        public void TestFullGraph()
        {
            var graph = new UndirectedGraph<char, EquatableEdge<char>>(true);
            graph.AddVertexRange(new[] { 'a', 'b', 'c', 'd' });
            graph.AddEdge(new EquatableEdge<char>('a', 'b'));
            graph.AddEdge(new EquatableEdge<char>('a', 'c'));
            graph.AddEdge(new EquatableEdge<char>('a', 'd'));
            graph.AddEdge(new EquatableEdge<char>('b', 'c'));
            graph.AddEdge(new EquatableEdge<char>('b', 'd'));
            graph.AddEdge(new EquatableEdge<char>('c', 'd'));
            FindingMaximalCliques<char> fmc = new FindingMaximalCliques<char>(graph);
            var cliques = fmc.FindCliques();
            Assert.AreEqual(cliques.Count == 1, true);
        }
    }
}
