using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void IsHamiltonianEmpty()
        {
            var g = constructGraph(new Tuple<int, int>[] { });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonianOneVertexWithCycle()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 1) });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonianTwoVerticesTrue()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2) });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonianTwoVerticesFalse()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 1), new Tuple<int, int>(2, 2) });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonianWithLoops()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 1), new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2), new Tuple<int, int>(2, 2), new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3), new Tuple<int, int>(3, 3)});
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }


        [TestMethod]
        public void IsHamiltonianWithParallelEdges()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2), new Tuple<int, int>(1, 2),
                new Tuple<int, int>(3, 4), new Tuple<int, int>(3, 4)});
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsFalse(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonian10VerticesDiracsTheorem()
        {
            // This graph is hamiltonian and satisfies Dirac's theorem. This test should work faster
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 7),
                new Tuple<int, int>(1, 8), new Tuple<int, int>(1, 10), new Tuple<int, int>(2, 6),
                new Tuple<int, int>(2, 9), new Tuple<int, int>(2, 4), new Tuple<int, int>(2, 5),
                new Tuple<int, int>(3, 4), new Tuple<int, int>(3, 6), new Tuple<int, int>(3, 7),
                new Tuple<int, int>(3, 8), new Tuple<int, int>(4, 6), new Tuple<int, int>(4, 5),
                new Tuple<int, int>(4, 7), new Tuple<int, int>(5, 7), new Tuple<int, int>(5, 6),
                new Tuple<int, int>(5, 9), new Tuple<int, int>(5, 10), new Tuple<int, int>(6, 9),
                new Tuple<int, int>(6, 10), new Tuple<int, int>(6, 7), new Tuple<int, int>(7, 8),
                new Tuple<int, int>(8, 9), new Tuple<int, int>(8, 10), new Tuple<int, int>(9, 10)
            });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }

        [TestMethod]
        public void IsHamiltonian10VerticesNotDiracsTheorem()
        {
            // This graph is hamiltonian but don't satisfy Dirac's theorem. This test should work slowlier
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 7),
                new Tuple<int, int>(1, 8), new Tuple<int, int>(1, 10), new Tuple<int, int>(2, 6),
                new Tuple<int, int>(2, 9), new Tuple<int, int>(2, 4),
                new Tuple<int, int>(3, 4), new Tuple<int, int>(3, 6), new Tuple<int, int>(3, 7),
                new Tuple<int, int>(3, 8), new Tuple<int, int>(4, 6), new Tuple<int, int>(4, 5),
                new Tuple<int, int>(4, 7), new Tuple<int, int>(5, 7), new Tuple<int, int>(5, 6),
                new Tuple<int, int>(5, 9), new Tuple<int, int>(5, 10), new Tuple<int, int>(6, 9),
                new Tuple<int, int>(6, 10), new Tuple<int, int>(6, 7), new Tuple<int, int>(7, 8),
                new Tuple<int, int>(8, 9), new Tuple<int, int>(8, 10), new Tuple<int, int>(9, 10)
            });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            Assert.IsTrue(gAlgo.IsHamiltonian());
        }
        
        private class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            public bool Equals(IEnumerable<T> seq1, IEnumerable<T> seq2)
            {
                return seq1.SequenceEqual(seq2);
            }

            public int GetHashCode(IEnumerable<T> seq)
            {
                int hash = 1234567;
                foreach (T elem in seq)
                    hash = hash * 37 + elem.GetHashCode();
                return hash;
            }
        }

        private int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }

        [TestMethod]
        public void IsHamiltonianTestCyclesBuilder()
        {
            var g = constructGraph(new Tuple<int, int>[] { new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 7),
                new Tuple<int, int>(1, 8), new Tuple<int, int>(1, 10), new Tuple<int, int>(2, 6),
                new Tuple<int, int>(2, 9), new Tuple<int, int>(2, 4),
                new Tuple<int, int>(3, 4), new Tuple<int, int>(3, 6), new Tuple<int, int>(3, 7),
                new Tuple<int, int>(3, 8), new Tuple<int, int>(4, 6), new Tuple<int, int>(4, 5),
                new Tuple<int, int>(4, 7), new Tuple<int, int>(5, 7), new Tuple<int, int>(5, 6),
                new Tuple<int, int>(5, 9), new Tuple<int, int>(5, 10), new Tuple<int, int>(6, 9),
                new Tuple<int, int>(6, 10), new Tuple<int, int>(6, 7), new Tuple<int, int>(7, 8),
                new Tuple<int, int>(8, 9), new Tuple<int, int>(8, 10), new Tuple<int, int>(9, 10)
            });
            var gAlgo = new QuickGraph.Algorithms.IsHamiltonianGraphAlgorithm<int, UndirectedEdge<int>>(g);
            gAlgo.IsHamiltonian();
            
            var hashSet = new HashSet<List<int>>(new SequenceComparer<int>());
            hashSet.UnionWith(gAlgo.permutations);

            Assert.AreEqual(hashSet.Count, Factorial(g.VertexCount));
        }
    }
}