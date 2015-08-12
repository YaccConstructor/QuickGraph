using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms.ConnectedComponents
{
    [TestClass]
    public partial class IncrementalConnectedComponentsAlgorithmTest
    {
        [TestMethod]
        public void IncrementalConnectedComponent()
        {
            var g = new AdjacencyGraph<int, SEquatableEdge<int>>();
            g.AddVertexRange(new int[] { 0, 1, 2, 3 });
            var components = AlgorithmExtensions.IncrementalConnectedComponents(g);

            var current = components();
            Assert.AreEqual(4, current.Key);

            g.AddEdge(new SEquatableEdge<int>(0, 1));
            current = components();
            Assert.AreEqual(3, current.Key);

            g.AddEdge(new SEquatableEdge<int>(2, 3));
            current = components();
            Assert.AreEqual(2, current.Key);

            g.AddEdge(new SEquatableEdge<int>(1, 3));
            current = components();
            Assert.AreEqual(1, current.Key);
        }
    }
}
