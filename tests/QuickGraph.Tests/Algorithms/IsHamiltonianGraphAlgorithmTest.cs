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
        public void IsHamiltonianTrue()
        {
            var g = constructGraph(new Tuple<int, int>[] {new Tuple<int, int>(1, 2), new Tuple<int, int>(2, 3),
                    new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4), new Tuple<int, int>(3, 4)});
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonianFalse()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                    new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 4), new Tuple<int, int>(3, 4)});
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }
    }
}
