using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Tests.Algorithms;
using QuickGraph.Tests.Algorithms.MinimumSpanningTree;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;

namespace QuickGraph.Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            // new TarjanOfflineLeastCommonAncestorAlgorithmTest().TarjanOfflineLeastCommonAncestorAlgorithmAll();
            // new DijkstraShortestPathAlgorithmTest().DijkstraAll();
            // new MinimumSpanningTreeTest().PrimKruskalMinimumSpanningTreeAll();
            var g = TestGraphFactory.LoadGraph(@"graphml\repro12359.graphml");
            var distances = new Dictionary<Edge<string>, double>(g.EdgeCount);
            foreach (var e in g.Edges)
                distances[e] = g.OutDegree(e.Source) + 1;
            int i = 0;
            foreach (var v in g.Vertices)
            {
                if (i++ > 5) break;
                Dijkstra(g, distances, v);
            }
        }

        static void Dijkstra<TVertex, TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> g,
            Dictionary<TEdge, double> distances,
            TVertex root)
            where TEdge : IEdge<TVertex>
        {
            var algo = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(
                g,
                e => distances[e]
                );
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (predecessors.Attach(algo))
                algo.Compute(root);
        }
    }
}
