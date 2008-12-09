using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public class UndirectedBreadthFirstAlgorithmSearchTest
    {
        private IDictionary<IdentifiableVertex, IdentifiableVertex> parents;
        private IDictionary<IdentifiableVertex, int> distances;
        private IdentifiableVertex currentVertex;
        private IdentifiableVertex sourceVertex;
        private int currentDistance;
        private UndirectedBreadthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> algo;

        private void InitializeVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.White);
        }

        private void ExamineVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            var u = args.Vertex;
            currentVertex = u;
            // Ensure that the distances monotonically increase.
            Assert.IsTrue(
                   distances[u] == currentDistance
                || distances[u] == currentDistance + 1
                );

            if (distances[u] == currentDistance + 1) // new level
                ++currentDistance;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            var u = args.Vertex;

            Assert.AreEqual(algo.VertexColors[u], GraphColor.Gray);
            if (u == sourceVertex)
                currentVertex = sourceVertex;
            else
            {
                Assert.AreEqual(parents[u], currentVertex);
                Assert.AreEqual(distances[u], currentDistance + 1);
                Assert.AreEqual(distances[u], distances[parents[u]] + 1);
            }
        }

        private void TreeEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            IdentifiableVertex u, v;
            if (args.Edge.Source.Equals(currentVertex))
            {
                u = args.Edge.Source;
                v = args.Edge.Target;
            }
            else
            {
                v = args.Edge.Source;
                u = args.Edge.Target;
            }

            Assert.AreEqual(algo.VertexColors[v], GraphColor.White);
            Assert.AreEqual(distances[u], currentDistance);
            parents[v] = u;
            distances[v] = distances[u] + 1;
        }

        private void NonTreeEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            IdentifiableVertex u, v;
            if (args.Edge.Source.Equals(currentVertex))
            {
                u = args.Edge.Source;
                v = args.Edge.Target;
            }
            else
            {
                v = args.Edge.Source;
                u = args.Edge.Target;
            }

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
        }

        private void GrayTarget(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            IdentifiableVertex v;
            if (args.Edge.Source == currentVertex)
            {
                v = args.Edge.Target;
            }
            else
            {
                v = args.Edge.Source;
            }
            Assert.AreEqual(algo.VertexColors[v], GraphColor.Gray);
        }

        private void BlackTarget(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            IdentifiableVertex u, v;
            if (args.Edge.Source == currentVertex)
            {
                u = args.Edge.Source;
                v = args.Edge.Target;
            }
            else
            {
                v = args.Edge.Source;
                u = args.Edge.Target;
            }

            Assert.AreEqual(algo.VertexColors[v], GraphColor.Black);

            foreach (Edge<IdentifiableVertex> e in algo.VisitedGraph.AdjacentEdges(v))
            {
                Assert.IsFalse(algo.VertexColors[
                    (e.Source==v) ? e.Target : e.Source
                    ] == GraphColor.White);
            }
        }

        private void FinishVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.Black);
        }

        public void Init()
        {
            this.parents = new Dictionary<IdentifiableVertex, IdentifiableVertex>();
            this.distances = new Dictionary<IdentifiableVertex, int>();
            this.currentDistance = 0;
            this.currentVertex = null;
            this.algo = null;
        }

        [TestMethod]
        public void UndirectedBreathFirstSearchAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                this.Compute(g);
        }

        [PexMethod]
        public void Compute(IUndirectedGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> graph)
        {
            foreach (var v in graph.Vertices)
                Search(graph, v);
        }

        private void Search(
            IUndirectedGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> graph, 
            IdentifiableVertex rootVertex)
        {
            this.Init();

            algo = new UndirectedBreadthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(graph);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<IdentifiableVertex>(this.InitializeVertex);
                algo.DiscoverVertex += new VertexEventHandler<IdentifiableVertex>(this.DiscoverVertex);
                algo.ExamineVertex += new VertexEventHandler<IdentifiableVertex>(this.ExamineVertex);
                algo.TreeEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.TreeEdge);
                algo.NonTreeEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.NonTreeEdge);
                algo.GrayTarget += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.GrayTarget);
                algo.BlackTarget += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.BlackTarget);
                algo.FinishVertex += new VertexEventHandler<IdentifiableVertex>(this.FinishVertex);

                parents.Clear();
                distances.Clear();
                currentDistance = 0;
                sourceVertex = rootVertex;

                foreach (var v in this.algo.VisitedGraph.Vertices)
                {
                    distances[v] = int.MaxValue;
                    parents[v] = v;
                }
                distances[sourceVertex] = 0;
                algo.Compute(sourceVertex);

                CheckBfs();
            }
            finally
            {
                algo.InitializeVertex -= new VertexEventHandler<IdentifiableVertex>(this.InitializeVertex);
                algo.DiscoverVertex -= new VertexEventHandler<IdentifiableVertex>(this.DiscoverVertex);
                algo.ExamineVertex -= new VertexEventHandler<IdentifiableVertex>(this.ExamineVertex);
                algo.TreeEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.TreeEdge);
                algo.NonTreeEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.NonTreeEdge);
                algo.GrayTarget -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.GrayTarget);
                algo.BlackTarget -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.BlackTarget);
                algo.FinishVertex -= new VertexEventHandler<IdentifiableVertex>(this.FinishVertex);
            }
        }

        protected void CheckBfs()
        {
            // All white vertices should be unreachable from the source.
            foreach (var v in this.algo.VisitedGraph.Vertices)
            {
                if (algo.VertexColors[v] == GraphColor.White)
                {
                    //!IsReachable(start,u,g);
                }
            }

            // The shortest path to a child should be one longer than
            // shortest path to the parent.
            foreach (var v in this.algo.VisitedGraph.Vertices)
            {
                if (parents[v] != v) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }
}
