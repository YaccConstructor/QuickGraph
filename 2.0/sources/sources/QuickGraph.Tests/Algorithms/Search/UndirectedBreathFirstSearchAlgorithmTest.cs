using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.Search
{
    [TypeFixture(typeof(IUndirectedGraph<string, Edge<string>>))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class UndirectedBreadthFirstAlgorithmSearchTest
    {
        private IDictionary<string, string> parents;
        private IDictionary<string, int> distances;
        private string currentVertex;
        private string sourceVertex;
        private int currentDistance;
        private UndirectedBreadthFirstSearchAlgorithm<string,Edge<string>> algo;

        private void InitializeVertex(Object sender, VertexEventArgs<string> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.White);
        }

        private void ExamineVertex(Object sender, VertexEventArgs<string> args)
        {
            string u = args.Vertex;
            currentVertex = u;
            // Ensure that the distances monotonically increase.
            Assert.IsTrue(
                   distances[u] == currentDistance
                || distances[u] == currentDistance + 1
                );

            if (distances[u] == currentDistance + 1) // new level
                ++currentDistance;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<string> args)
        {
            string u = args.Vertex;

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

        private void TreeEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            string u, v;
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

            Assert.AreEqual(algo.VertexColors[v], GraphColor.White);
            Assert.AreEqual(distances[u], currentDistance);
            parents[v] = u;
            distances[v] = distances[u] + 1;
        }

        private void NonTreeEdge(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            string u, v;
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

        private void GrayTarget(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            string v;
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

        private void BlackTarget(Object sender, EdgeEventArgs<string,Edge<string>> args)
        {
            string u, v;
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

            foreach (Edge<string> e in algo.VisitedGraph.AdjacentEdges(v))
            {
                Assert.IsFalse(algo.VertexColors[
                    (e.Source==v) ? e.Target : e.Source
                    ] == GraphColor.White);
            }
        }

        private void FinishVertex(Object sender, VertexEventArgs<string> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.Black);
        }

        [SetUp]
        public void Init()
        {
            this.parents = new Dictionary<string, string>();
            this.distances = new Dictionary<string, int>();
            this.currentDistance = 0;
            this.currentVertex = null;
            this.algo = null;
        }

        [Test]
        public void GraphWithSelfEdges(IUndirectedGraph<string,Edge<string>> graph)
        {
            List<string> vertices = new List<string>(graph.Vertices);

            foreach (string v in vertices)
                Search(graph, v);
        }

        private void Search(IUndirectedGraph<string,Edge<string>> graph, string rootVertex)
        {
            Console.WriteLine(rootVertex);
            algo = new UndirectedBreadthFirstSearchAlgorithm<string,Edge<string>>(graph);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<string>(this.InitializeVertex);
                algo.DiscoverVertex += new VertexEventHandler<string>(this.DiscoverVertex);
                algo.ExamineVertex += new VertexEventHandler<string>(this.ExamineVertex);
                algo.TreeEdge += new EdgeEventHandler<string,Edge<string>>(this.TreeEdge);
                algo.NonTreeEdge += new EdgeEventHandler<string,Edge<string>>(this.NonTreeEdge);
                algo.GrayTarget += new EdgeEventHandler<string,Edge<string>>(this.GrayTarget);
                algo.BlackTarget += new EdgeEventHandler<string,Edge<string>>(this.BlackTarget);
                algo.FinishVertex += new VertexEventHandler<string>(this.FinishVertex);

                parents.Clear();
                distances.Clear();
                currentDistance = 0;
                sourceVertex = rootVertex;

                foreach (string v in this.algo.VisitedGraph.Vertices)
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
                algo.InitializeVertex -= new VertexEventHandler<string>(this.InitializeVertex);
                algo.DiscoverVertex -= new VertexEventHandler<string>(this.DiscoverVertex);
                algo.ExamineVertex -= new VertexEventHandler<string>(this.ExamineVertex);
                algo.TreeEdge -= new EdgeEventHandler<string,Edge<string>>(this.TreeEdge);
                algo.NonTreeEdge -= new EdgeEventHandler<string,Edge<string>>(this.NonTreeEdge);
                algo.GrayTarget -= new EdgeEventHandler<string,Edge<string>>(this.GrayTarget);
                algo.BlackTarget -= new EdgeEventHandler<string,Edge<string>>(this.BlackTarget);
                algo.FinishVertex -= new VertexEventHandler<string>(this.FinishVertex);
            }
        }

        protected void CheckBfs()
        {
            // All white vertices should be unreachable from the source.
            foreach (string v in this.algo.VisitedGraph.Vertices)
            {
                if (algo.VertexColors[v] == GraphColor.White)
                {
                    //!IsReachable(start,u,g);
                }
            }

            // The shortest path to a child should be one longer than
            // shortest path to the parent.
            foreach (string v in this.algo.VisitedGraph.Vertices)
            {
                if (parents[v] != v) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }
}
