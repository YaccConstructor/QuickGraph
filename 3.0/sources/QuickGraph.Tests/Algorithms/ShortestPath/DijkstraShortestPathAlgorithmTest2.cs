using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TestClass, PexClass]
    public partial class DijkstraShortestPathAlgorithmTest2
    {
        [TestMethod]
        public void DijkstraAll()
        {
            foreach (var g in GraphMLFilesHelper.GetGraphs())
                this.Compute(g);
        }

        [PexMethod]
        public void Compute<TVertex, TEdge>([PexAssumeNotNull]IVertexAndEdgeListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var vertices = new List<TVertex>(g.Vertices);
            foreach (var root in vertices)
                Search(g, root);
        }

        private void Search<TVertex, TEdge>(IVertexAndEdgeListGraph<TVertex, TEdge> g, TVertex root)
            where TEdge : IEdge<TVertex>
        {
            var algo = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(
                g,
                e => 1
                );
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            predecessors.Attach(algo);
            algo.Compute(root);

            Verify(algo, predecessors);
        }

        private static void Verify<TVertex, TEdge>(
            DijkstraShortestPathAlgorithm<TVertex, TEdge> algo,
            VertexPredecessorRecorderObserver<TVertex, TEdge> predecessors)
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
                if (found)
                    Assert.AreEqual(vd, vp+1);
            }
        }
    }
}
