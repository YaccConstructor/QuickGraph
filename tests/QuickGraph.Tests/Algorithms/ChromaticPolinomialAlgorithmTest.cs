﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;
using System.Linq;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class ChromaticPolinomialAlgorithmTest
    {
        [TestMethod]
        public void ChromaticPolinomialFullGraph()
        {
            var graph = BuildGraph(new int[] { 1, 2, 3, 4 },
                new int[] { 1, 2,  1, 3,  1, 4,
                            2, 3,  2, 4,  3, 4});
            CollectionAssert.AreEqual(new int[] { 1, -6, 11, -6, 0 }, ChromaticPolynomial.findChromaticPolynomial(graph));
        }

        [TestMethod]
        public void ChromaticPolinomialAlmostFullGraph()
        {
            var graph = BuildGraph(new int[] { 1, 2, 3, 4 },
                new int[] { 1, 2,  1, 3,  1, 4,
                            2, 3,  2, 4});
            CollectionAssert.AreEqual(new int[] { 1, -5, 8, -4, 0 }, ChromaticPolynomial.findChromaticPolynomial(graph));
        }

        [TestMethod]
        public void ChromaticPolinomialEmptyGraph()
        {
            var graph = BuildGraph(new int[] { 1, 2, 3 },
                new int[] {});
            CollectionAssert.AreEqual(new int[] { 1, 0, 0, 0}, ChromaticPolynomial.findChromaticPolynomial(graph));
        }

        private UndirectedGraph<int, UndirectedEdge<int>> BuildGraph(int[] verticies, int[] edges)
        {
            var graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            graph.AddVertexRange(verticies);
            var convEdges = new UndirectedEdge<int>[edges.Length / 2];
            for (int i = 0; i < edges.Length; i += 2)
            {
                convEdges[i / 2] = new UndirectedEdge<int>(edges[i], edges[i + 1]);
            }
            graph.AddEdgeRange(convEdges);
            return graph;
        }
    }
}
