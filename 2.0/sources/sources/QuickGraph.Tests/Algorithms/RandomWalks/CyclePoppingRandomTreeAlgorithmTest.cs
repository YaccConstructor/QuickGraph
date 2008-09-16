using System;
using QuickGraph.Unit;
using QuickGraph.Algorithms.RandomWalks;

namespace QuickGraph.Algorithms.RandomWalks
{
    [TestFixture]
    public class CyclePoppingRandomTreeAlgorithmTest
    {
        private CyclePoppingRandomTreeAlgorithm<int,Edge<int>> target = null;

        [Test]
        public void IsolatedVertex()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(0);

            target = new CyclePoppingRandomTreeAlgorithm<int, Edge<int>>(g);
            target.RandomTree();
        }

        [Test]
        public void RootIsNotAccessible()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(0);
            g.AddVertex(1);
            g.AddEdge(new Edge<int>(0, 1));

            target = new CyclePoppingRandomTreeAlgorithm<int, Edge<int>>(g);
            target.RandomTreeWithRoot(0);
        }

        [Test]
        public void Loop()
        {
            CyclePoppingRandomTreeAlgorithm<string, Edge<string>>  target = new CyclePoppingRandomTreeAlgorithm<string, Edge<string>>(new AdjacencyGraphFactory().Loop());
            target.RandomTree();
        }

    }
}