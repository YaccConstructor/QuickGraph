using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class EulerianGraphTest
    {
        [TestMethod]
        public void testIsEulerianOneComponentTrue()
        {
            var g = new UndirectedGraph<int, UndirectedEdge<int>>();
            
            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 2));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 3));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(3, 1));

            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.isEulerian());
        }

        [TestMethod]
        public void testIsEulerianOneComponentFalse()
        {
            var g = new UndirectedGraph<int, UndirectedEdge<int>>();

            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 2));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(2, 3));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(3, 4));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(4, 1));
            g.AddVerticesAndEdge(new UndirectedEdge<int>(1, 3));

            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.isEulerian());
        }

        [TestMethod]
        public void testIsEulerianManyComponentsTrue()
        {
            var g = new UndirectedGraph<String, UndirectedEdge<String>>();

            g.AddVerticesAndEdge(new UndirectedEdge<String>("A", "B"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("B", "C"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("C", "A"));
            g.AddVertex("D");
            g.AddVertex("E");

            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<String, UndirectedEdge<String>>(g);
            Assert.IsTrue(gAlgo.isEulerian());
        }

        [TestMethod]
        public void testIsEulerianManyComponentsFalse()
        {
            var g = new UndirectedGraph<String, UndirectedEdge<String>>();

            g.AddVerticesAndEdge(new UndirectedEdge<String>("A", "B"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("B", "C"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("C", "A"));
            g.AddVertex("D");
            g.AddVerticesAndEdge(new UndirectedEdge<String>("E", "F"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("F", "G"));
            g.AddVerticesAndEdge(new UndirectedEdge<String>("G", "E"));

            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<String, UndirectedEdge<String>>(g);
            Assert.IsFalse(gAlgo.isEulerian());
        }
    }
}
