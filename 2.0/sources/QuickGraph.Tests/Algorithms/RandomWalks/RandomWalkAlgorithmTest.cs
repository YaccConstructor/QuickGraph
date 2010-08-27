using System;
using QuickGraph.Unit;

using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.RandomWalks
{
    [TypeFixture(typeof(IVertexListGraph<string,Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public class RandomWalkAlgorithmTest
    {
        [Test]
        public void RoundRobinTest(IVertexListGraph<string, Edge<string>> g)
        {
            if (g.VertexCount == 0)
                return;

            RandomWalkAlgorithm<String, Edge<string>> walker =
                new RandomWalkAlgorithm<String, Edge<string>>(g);
            walker.EdgeChain = new NormalizedMarkovEdgeChain<string, Edge<string>>();

            string root = TraversalHelper.GetFirstVertex(g);
            walker.Generate(root);
        }

        [Test]
        public void RoundRobinTestWithVisitor(IVertexListGraph<string, Edge<string>> g)
        {
            if (g.VertexCount == 0)
                return;

            RandomWalkAlgorithm<String, Edge<string>> walker =
                new RandomWalkAlgorithm<String, Edge<string>>(g);
            walker.EdgeChain = new NormalizedMarkovEdgeChain<string, Edge<string>>();

            string root = TraversalHelper.GetFirstVertex(g);

            EdgeRecorderObserver<string, Edge<string>> vis = new EdgeRecorderObserver<string, Edge<string>>();
            vis.Attach(walker);
            walker.Generate(root);
            vis.Detach(walker);
        }

    }
}
