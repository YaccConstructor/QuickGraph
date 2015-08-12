using System;
using System.Linq;
using System.Collections.Generic;
using QuickGraph.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public class BreadthFirstAlgorithmSearchTest
    {
        [TestMethod]
        public void BreadthFirstSearchAll()
        {
            Parallel.ForEach(TestGraphFactory.GetAdjacencyGraphs(), g =>
                {
                    foreach (var v in g.Vertices)
                        RunBfs(g, v);
                });
         }

        [PexMethod]
        public void RunBfs<TVertex, TEdge>(IVertexAndEdgeListGraph<TVertex, TEdge> g, TVertex sourceVertex)
            where TEdge : IEdge<TVertex>
        {
            var parents = new Dictionary<TVertex, TVertex>();
            var distances = new Dictionary<TVertex, int>();
            TVertex currentVertex = default(TVertex);
            int currentDistance = 0;
            var algo = new BreadthFirstSearchAlgorithm<TVertex, TEdge>(g);

            algo.InitializeVertex += u =>
            {
                Assert.AreEqual(algo.VertexColors[u], GraphColor.White);
            };

            algo.DiscoverVertex += u =>
            {
                Assert.AreEqual(algo.VertexColors[u], GraphColor.Gray);
                if (u.Equals(sourceVertex))
                    currentVertex = sourceVertex;
                else
                {
                    Assert.IsNotNull(currentVertex);
                    Assert.AreEqual(parents[u], currentVertex);
                    Assert.AreEqual(distances[u], currentDistance + 1);
                    Assert.AreEqual(distances[u], distances[parents[u]] + 1);
                }
            };
            algo.ExamineEdge += args =>
            {
                Assert.AreEqual(args.Source, currentVertex);
            };

            algo.ExamineVertex += args =>
            {
                var u = args;
                currentVertex = u;
                // Ensure that the distances monotonically increase.
                Assert.IsTrue(
                       distances[u] == currentDistance
                    || distances[u] == currentDistance + 1
                    );

                if (distances[u] == currentDistance + 1) // new level
                    ++currentDistance;
            };
            algo.TreeEdge += args =>
            {
                var u = args.Source;
                var v = args.Target;

                Assert.AreEqual(algo.VertexColors[v], GraphColor.White);
                Assert.AreEqual(distances[u], currentDistance);
                parents[v] = u;
                distances[v] = distances[u] + 1;
            };
            algo.NonTreeEdge += args =>
            {
                var u = args.Source;
                var v = args.Target;

                Assert.IsFalse(algo.VertexColors[v] == GraphColor.White);

                if (algo.VisitedGraph.IsDirected)
                {
                    // cross or back edge
                    Assert.IsTrue(distances[v] <= distances[u] + 1);
                }
                else
                {
                    // cross edge (or going backwards on a tree edge)
                    Assert.IsTrue(
                        distances[v] == distances[u]
                        || distances[v] == distances[u] + 1
                        || distances[v] == distances[u] - 1
                        );
                }
            };

            algo.GrayTarget += args =>
            {
                Assert.AreEqual(algo.VertexColors[args.Target], GraphColor.Gray);
            };
            algo.BlackTarget += args =>
            {
                Assert.AreEqual(algo.VertexColors[args.Target], GraphColor.Black);

                foreach (var e in algo.VisitedGraph.OutEdges(args.Target))
                    Assert.IsFalse(algo.VertexColors[e.Target] == GraphColor.White);
            };

            algo.FinishVertex += args =>
            {
                Assert.AreEqual(algo.VertexColors[args], GraphColor.Black);
            };


            parents.Clear();
            distances.Clear();
            currentDistance = 0;

            foreach (var v in g.Vertices)
            {
                distances[v] = int.MaxValue;
                parents[v] = v;
            }
            distances[sourceVertex] = 0;
            algo.Compute(sourceVertex);

            // All white vertices should be unreachable from the source.
            foreach (var v in g.Vertices)
            {
                if (algo.VertexColors[v] == GraphColor.White)
                {
                    //!IsReachable(start,u,g);
                }
            }

            // The shortest path to a child should be one longer than
            // shortest path to the parent.
            foreach (var v in g.Vertices)
            {
                if (!parents[v].Equals(v)) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }
}
