using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class BipartiteDimensionAlgorithmTest
    {
        [TestMethod]
        public void BipartiteDimension5Verticies()
        {
            Assert.AreEqual(2, CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 3,  2, 3,  1, 4,  2, 4,  2, 5 }));
            var g = new UndirectedGraph<int, IEdge<int>>();
        }

        [TestMethod]
        public void BipartiteDimension6Verticies()
        {
            Assert.AreEqual(3, CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5, 6 },
                new int[] { 1, 5,  1, 6,  2, 4,  2, 6,  3, 4,  3, 5 }));
        }

        [TestMethod]
        public void BipartiteDimension5Verticies4Edges()
        {
            Assert.AreEqual(2, CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 4,  2, 4,  2, 5,  3, 5 }));
        }

        [TestMethod]
        public void BipartiteDimensionReducable()
        {
            Assert.AreEqual(1, CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                new int[] { 1, 5,  1, 6,  1, 7,
                            2, 5,  2, 6,  2, 7,
                            3, 5,  3, 6,  3, 7, 
                            4, 5,  4, 6,  4, 7,
                }));
        }

        [TestMethod]
        public void BipartiteDimensionNoEdges()
        {
            Assert.AreEqual(0, CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] {}));
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void BipartiteDimensionNonBipartiteGraph()
        {
            CalcBicliqueCount(new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 3,  2, 3,  1, 4,  2, 4,  2, 5,  4, 5 });
        }

        private int CalcBicliqueCount(int[] verticies, int[] edges)
        {
            var g = new UndirectedGraph<int, IEdge<int>>();
            g.AddVertexRange(verticies);
            var convEdges = new Edge<int>[edges.Length / 2];
            for (int i = 0; i < edges.Length; i += 2)
            {
                convEdges[i / 2] = new Edge<int>(edges[i], edges[i + 1]);
            }
            g.AddEdgeRange(convEdges);
            var algo = new BipartiteDimensionAlgorithm(g);
            algo.Compute();
            return algo.BipartiteDimensionValue;
        } 
    }
}
