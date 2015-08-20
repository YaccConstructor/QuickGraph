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
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;

namespace QuickGraph.Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            // new TarjanOfflineLeastCommonAncestorAlgorithmTest().TarjanOfflineLeastCommonAncestorAlgorithmAll();
            // new DijkstraShortestPathAlgorithmTest().DijkstraAll();
            // new MinimumSpanningTreeTest().PrimKruskalMinimumSpanningTreeAll();
            var g = TestGraphFactory.LoadBidirectionalGraph(@"graphml\repro12359.graphml");
            var distances = new Dictionary<Edge<string>, double>(g.EdgeCount);
            foreach (var e in g.Edges)
                distances[e] = g.OutDegree(e.Source) + 1;
            var root = Enumerable.First(g.Vertices);
            foreach (var v in g.Vertices)
            {
                FrontierDijkstra(g, distances, root, v);
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
                AlgorithmExtensions.GetIndexer(distances)
                );
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (predecessors.Attach(algo))
                algo.Compute(root);
        }

        static void FrontierDijkstra<TVertex, TEdge>(
            IBidirectionalGraph<TVertex, TEdge> g,
            Dictionary<TEdge, double> distances,
            TVertex root,
            TVertex target)
            where TEdge : IEdge<TVertex>
        {
            var algo = new BestFirstFrontierSearchAlgorithm<TVertex, TEdge>(
                null,
                g,
                AlgorithmExtensions.GetIndexer(distances),
                DistanceRelaxers.ShortestDistance
                );
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (predecessors.Attach(algo))
                algo.Compute(root, target);
        }
    }
}
