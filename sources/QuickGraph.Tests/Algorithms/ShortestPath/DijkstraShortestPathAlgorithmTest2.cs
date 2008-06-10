using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TestFixture, PexClass]
    public partial class DijkstraShortestPathAlgorithmTest2
    {
        [PexMethod]
        public void Compute([PexAssumeNotNull]IVertexAndEdgeListGraph<string,Edge<string>> g)
        {
            List<string> vertices = new List<string>(g.Vertices);
            foreach (string root in vertices)
            {
                Search(g, root);
            }
        }

        private void Search(IVertexAndEdgeListGraph<string,Edge<string>> g, string root)
        {
            DijkstraShortestPathAlgorithm<string,Edge<string>> algo = new DijkstraShortestPathAlgorithm<string,Edge<string>>(g,
                DijkstraShortestPathAlgorithm<string,Edge<string>>.UnaryWeightsFromEdgeList(g)
                );
            VertexPredecessorRecorderObserver<string,Edge<string>> predecessors = new VertexPredecessorRecorderObserver<string,Edge<string>>();
            predecessors.Attach(algo);
            algo.Compute(root);

            Verify(algo, predecessors);
        }

        private static void Verify(DijkstraShortestPathAlgorithm<string, Edge<string>> algo, VertexPredecessorRecorderObserver<string, Edge<string>> predecessors)
        {
            // let's verify the result
            foreach (string v in algo.VisitedGraph.Vertices)
            {
                Edge<string> predecessor;
                if (!predecessors.VertexPredecessors.TryGetValue(v, out predecessor))
                    continue;
                if (predecessor.Source == v)
                    continue;
                Assert.AreEqual(
                    algo.Distances[v], algo.Distances[predecessor.Source] + 1
                    );
            }
        }
    }
}
