using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.MinimumSpanningTree
{
    [TypeFixture(typeof(IUndirectedGraph<string, Edge<string>>))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class PrimMinimumSpanningTreeAlgorithmTest
    {
        [Test]
        public void Compute(IUndirectedGraph<string, Edge<string>> g)
        {
            Dictionary<Edge<string>, double> distances = new Dictionary<Edge<string>,double>();
            foreach(Edge<string> edge in g.Edges)
                distances.Add(edge, 1);
            PrimMinimumSpanningTreeAlgorithm<string, Edge<string>> prim = new PrimMinimumSpanningTreeAlgorithm<string, Edge<string>>(g, distances);

            VertexPredecessorRecorderObserver<string, Edge<string>> predecessors = new VertexPredecessorRecorderObserver<string, Edge<string>>();
            predecessors.Attach(prim);
            prim.Compute();

            foreach (string v in g.Vertices)
            {
                Edge<string> edge;
                if (predecessors.VertexPredecessors.TryGetValue(v, out edge))
                    Console.WriteLine("{0}: {1}", v, edge);
                else
                    Console.WriteLine("{0}", v);
            }
        }
    }
}
