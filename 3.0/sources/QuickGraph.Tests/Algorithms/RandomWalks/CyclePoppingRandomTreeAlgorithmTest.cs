using System;
using QuickGraph.Algorithms.RandomWalks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.RandomWalks
{
    [TestClass]
    public class CyclePoppingRandomTreeAlgorithmTest
    {
        [TestMethod]
        public void CyclePoppingRandomTreeAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
            {
                foreach (var v in g.Vertices)
                {
                    var target = new CyclePoppingRandomTreeAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(g);
                    target.Compute(v);
                }
            }
        }

        [TestMethod]
        public void IsolatedVertex()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(0);

            var target = new CyclePoppingRandomTreeAlgorithm<int, Edge<int>>(g);
            target.RandomTree();
        }

        [TestMethod]
        public void RootIsNotAccessible()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(0);
            g.AddVertex(1);
            g.AddEdge(new Edge<int>(0, 1));

            var target = new CyclePoppingRandomTreeAlgorithm<int, Edge<int>>(g);
            target.RandomTreeWithRoot(0);
        }
    }
}