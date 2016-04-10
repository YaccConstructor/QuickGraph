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
        private UndirectedGraph<int, UndirectedEdge<int>> constructGraph(Tuple<int, int>[] vertices)
        {
            var g = new UndirectedGraph<int, UndirectedEdge<int>>();
            foreach (var pair in vertices)
            {
                g.AddVerticesAndEdge(new UndirectedEdge<int>(pair.Item1, pair.Item2));
            }
            return g;
        }

        [TestMethod]
        public void IsEulerianOneComponentTrue()
        {
            var g = constructGraph(new Tuple<int, int>[] {new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 1)});
            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.isEulerian());
        }

        [TestMethod]
        public void IsEulerianOneComponentFalse()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                    new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 4),
                    new Tuple<int, int>(4, 1), new Tuple<int, int>(1, 3)});
            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.isEulerian());
        }

        [TestMethod]
        public void IsEulerianManyComponentsTrue()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 1) });
            g.AddVertex(4);
            g.AddVertex(5);

            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.isEulerian());
        }

        [TestMethod]
        public void IsEulerianManyComponentsFalse()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                    new Tuple<int, int>(2, 3), new Tuple<int, int>(3, 1),
                    new Tuple<int, int>(4, 5), new Tuple<int, int>(5, 6),
                    new Tuple<int, int>(6, 4)});
            g.AddVertex(7);           
            var gAlgo = new QuickGraph.Algorithms.IsEulerianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.isEulerian());
        }
    }
}
