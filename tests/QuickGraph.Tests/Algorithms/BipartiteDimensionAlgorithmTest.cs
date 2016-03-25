using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    class BipartiteDimensionAlgorithmTest
    {
        [TestMethod]
        public void BipartiteDimension5Verticies()
        {
            var g = new UndirectedGraph<int, IEdge<int>>();
            g.AddVertexRange(new int[] { 1, 2, 3, 4, 5 });
            g.AddEdge(new Edge<int>(1, 3));
            g.AddEdge(new Edge<int>(1, 4));
            g.AddEdge(new Edge<int>(2, 3));
            g.AddEdge(new Edge<int>(2, 4));
            g.AddEdge(new Edge<int>(2, 5));
            var algo = new BipartiteDimensionAlgorithm(g);
            algo.Compute();
            Assert.AreEqual(algo.BipartiteDimensionValue, 2);
        }
    }
}
