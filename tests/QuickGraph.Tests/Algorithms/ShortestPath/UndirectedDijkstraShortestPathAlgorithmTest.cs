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
            System.Threading.Tasks.Parallel.ForEach(TestGraphFactory.GetUndirectedGraphs(), g =>
            {
                int cut = 0;
                foreach (var root in g.Vertices)
                {
                    if (cut++ > 10)
                        break;
                    this.UndirectedDijkstra(g, root);
                }
            });
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
            using(predecessors.Attach(algo))
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

        [TestMethod]
        [WorkItem(42450)]
        public void Repro42450()
        {
            var ug = new UndirectedGraph<object, Edge<object>>(true);
            object v1 = "vertex1";
            object v2 = "vertex2";
            object v3 = "vertex3";
            var e1 = new Edge<object>(v1, v2);
            var e2 = new Edge<object>(v2, v3);
            var e3 = new Edge<object>(v3, v1);
            ug.AddVertex(v1);
            ug.AddVertex(v2);
            ug.AddVertex(v3);
            ug.AddEdge(e1);
            ug.AddEdge(e2);
            ug.AddEdge(e3);

            var udspa =
                new UndirectedDijkstraShortestPathAlgorithm<object, QuickGraph.Edge<object>>(ug, edge => (double)1);
            var observer =
                new UndirectedVertexPredecessorRecorderObserver<object, Edge<object>>();
            using (observer.Attach(udspa))
                udspa.Compute(v1);
            IEnumerable<QuickGraph.Edge<object>> path;
            observer.TryGetPath(v3, out path);
        }
    }
}
