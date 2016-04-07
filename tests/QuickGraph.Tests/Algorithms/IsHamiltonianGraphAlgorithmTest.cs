using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class IsHamiltonianGraphAlgorithmTest
    {
        [TestMethod]
        public void testIsHamiltonianTrue()
        {
            var g = new UndirectedGraph<int, UndirectedEdge<int>>();

            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 2));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 3));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 3));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 4));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(3, 4));

            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void testIsHamiltonianFalse()
        {
            var g = new UndirectedGraph<int, UndirectedEdge<int>>();

            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 2));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 3));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 4));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(3, 4));

            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }
    }
}
