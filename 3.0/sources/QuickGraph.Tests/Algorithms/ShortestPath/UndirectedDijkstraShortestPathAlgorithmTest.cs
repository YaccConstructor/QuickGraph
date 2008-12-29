using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TestClass, PexClass]
    public partial class UndirectedDijkstraShortestPathAlgorithmTest2
    {
        [TestMethod]
        public void UndirectedDijkstraAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
            {
                int cut = 0;
                foreach (var root in g.Vertices)
                {
                    if (cut++ > 10)
                        break;
                    this.UndirectedDijkstra(g, root);
                }
            }
        }

        [PexMethod]
        public void UndirectedDijkstra<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g, TVertex root)
            where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = g.AdjacentDegree(e.Source) + 1;

            var algo = new UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge>(
                g,
                e => distances[e]
                );
            var predecessors = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            using(ObserverScope.Create(algo, predecessors))
                algo.Compute(root);

            Verify(algo, predecessors);
        }

        private static void Verify<TVertex, TEdge>(
            UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge> algo,
            UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge> predecessors)
            where TEdge : IEdge<TVertex>
        {
            // let's verify the result
            foreach (var v in algo.VisitedGraph.Vertices)
            {
                TEdge predecessor;
                if (!predecessors.VertexPredecessors.TryGetValue(v, out predecessor))
                    continue;
                if (predecessor.Source.Equals(v))
                    continue;
                double vd, vp;
                bool found;
                Assert.AreEqual(found = algo.TryGetDistance(v, out vd), algo.TryGetDistance(predecessor.Source, out vp));
               // if (found)
             //       Assert.AreEqual(vd, vp+1);
            }
        }
    }
}
