using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TypeFixture(typeof(IVertexListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public class DagShortestPathAlgorithmTest
    {
        [Test]
        public void Compute(IVertexListGraph<string, Edge<string>> g)
        {
            // is this a dag ?
            bool isDag = AlgoUtility.IsDirectedAcyclicGraph(g);

            List<string> vertices = new List<string>(g.Vertices);
            foreach (string root in vertices)
            {
                if (isDag)
                    Search(g, root);
                else
                {
                    try
                    {
                        Search(g, root);
                    }
                    catch (NonAcyclicGraphException)
                    {
                        Console.WriteLine("NonAcyclicGraphException caught (as expected)");
                    }
                }
            }
        }

        private void Search(IVertexListGraph<string, Edge<string>> g, string root)
        {
            DagShortestPathAlgorithm<string, Edge<string>> algo = 
                new DagShortestPathAlgorithm<string, Edge<string>>(
                    g,
                    DagShortestPathAlgorithm<string, Edge<string>>.UnaryWeightsFromVertexList(g)
                    );
            VertexPredecessorRecorderObserver<string, Edge<string>> predecessors = new VertexPredecessorRecorderObserver<string, Edge<string>>();
            predecessors.Attach(algo);
            algo.Compute(root);

            Verify(algo, predecessors);
        }

        private static void Verify(DagShortestPathAlgorithm<string, Edge<string>> algo, VertexPredecessorRecorderObserver<string, Edge<string>> predecessors)
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
