using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TestClass, PexClass]
    public partial class DijkstraShortestPathAlgorithmTest
    {
        [TestMethod]
        public void DijkstraAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
                foreach (var root in g.Vertices)
                    this.Dijkstra(g, root);
        }

        [PexMethod]
        public void Dijkstra<TVertex, TEdge>(IVertexAndEdgeListGraph<TVertex, TEdge> g, TVertex root)
            where TEdge : IEdge<TVertex>
        {
            var distances = new Dictionary<TEdge, double>();
            foreach (var e in g.Edges)
                distances[e] = g.OutDegree(e.Source) + 1;

            var algo = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(
                g,
                e => distances[e]
                );
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algo, predecessors))
                algo.Compute(root);

            Verify(algo, predecessors);
        }

        private static void Verify<TVertex, TEdge>(
            DijkstraShortestPathAlgorithm<TVertex, TEdge> algo,
            VertexPredecessorRecorderObserver<TVertex, TEdge> predecessors
            )
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
                Assert.AreEqual(
                    found = algo.TryGetDistance(v, out vd), 
                    algo.TryGetDistance(predecessor.Source, out vp)
                    );
            }
        }
    }
}
