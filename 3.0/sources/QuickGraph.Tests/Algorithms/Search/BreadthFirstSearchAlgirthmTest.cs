using System;
using System.Linq;
using System.Collections.Generic;
using QuickGraph.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Algorithms.Search
{
    [TestClass]
    public class BreadthFirstAlgorithmSearchTest
    {
        private IDictionary<int, int> parents;
        private IDictionary<int, int> distances;
        private int currentVertex;
        private int sourceVertex;
        private int currentDistance;
        private BreadthFirstSearchAlgorithm<int, Edge<int>> algo;
        private AdjacencyGraph<int, Edge<int>> g;

        private void InitializeVertex(Object sender, VertexEventArgs<int> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.White);
        }

        private void ExamineVertex(Object sender, VertexEventArgs<int> args)
        {
            int u = args.Vertex;
            currentVertex = u;
            // Ensure that the distances monotonically increase.
            Assert.IsTrue(
                   distances[u] == currentDistance
                || distances[u] == currentDistance + 1
                );

            if (distances[u] == currentDistance + 1) // new level
                ++currentDistance;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<int> args)
        {
            int u = args.Vertex;

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

        private void ExamineEdge(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            Assert.AreEqual(args.Edge.Source, currentVertex);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            int u = args.Edge.Source;
            int v = args.Edge.Target;

            Assert.AreEqual(algo.VertexColors[v], GraphColor.White);
            Assert.AreEqual(distances[u], currentDistance);
            parents[v] = u;
            distances[v] = distances[u] + 1;
        }

        private void NonTreeEdge(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            int u = args.Edge.Source;
            int v = args.Edge.Target;

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

        private void GrayTarget(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Edge.Target], GraphColor.Gray);
        }

        private void BlackTarget(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Edge.Target], GraphColor.Black);

            foreach (Edge<int> e in algo.VisitedGraph.OutEdges(args.Edge.Target))
                Assert.IsFalse(algo.VertexColors[e.Target] == GraphColor.White);
        }

        private void FinishVertex(Object sender, VertexEventArgs<int> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.Black);
        }

        [TestInitialize]
        public void Init()
        {
            this.parents = new Dictionary<int, int>();
            this.distances = new Dictionary<int, int>();
            this.currentDistance = 0;
            this.currentVertex = 0;
            this.algo = null;
            this.g = null;
        }

        class IntFactory
        {
            int id;
            public int Next()
            {
                return this.id++;
            }
        }


        [TestMethod]
        public void GraphWithSelfEdgesBig()
        {
            Random rnd = new Random();
            g = new AdjacencyGraph<int, Edge<int>>(true);
            var next = new IntFactory();
            RandomGraphFactory.Create<int, Edge<int>>(g,
                next.Next,
                (s, t) => new Edge<int>(s, t),
                rnd, 5000, 20000, false);

            var sv = g.Vertices.FirstOrDefault();
            this.sourceVertex = sv;
            RunBfs();
        }

        private void RunBfs()
        {
            algo = new BreadthFirstSearchAlgorithm<int, Edge<int>>(g);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<int>(this.InitializeVertex);
                algo.DiscoverVertex += new VertexEventHandler<int>(this.DiscoverVertex);
                algo.ExamineEdge += new EdgeEventHandler<int, Edge<int>>(this.ExamineEdge);
                algo.ExamineVertex += new VertexEventHandler<int>(this.ExamineVertex);
                algo.TreeEdge += new EdgeEventHandler<int, Edge<int>>(this.TreeEdge);
                algo.NonTreeEdge += new EdgeEventHandler<int, Edge<int>>(this.NonTreeEdge);
                algo.GrayTarget += new EdgeEventHandler<int, Edge<int>>(this.GrayTarget);
                algo.BlackTarget += new EdgeEventHandler<int, Edge<int>>(this.BlackTarget);
                algo.FinishVertex += new VertexEventHandler<int>(this.FinishVertex);

                parents.Clear();
                distances.Clear();
                currentDistance = 0;

                foreach (int v in g.Vertices)
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
                algo.InitializeVertex -= new VertexEventHandler<int>(this.InitializeVertex);
                algo.DiscoverVertex -= new VertexEventHandler<int>(this.DiscoverVertex);
                algo.ExamineEdge -= new EdgeEventHandler<int, Edge<int>>(this.ExamineEdge);
                algo.ExamineVertex -= new VertexEventHandler<int>(this.ExamineVertex);
                algo.TreeEdge -= new EdgeEventHandler<int, Edge<int>>(this.TreeEdge);
                algo.NonTreeEdge -= new EdgeEventHandler<int, Edge<int>>(this.NonTreeEdge);
                algo.GrayTarget -= new EdgeEventHandler<int, Edge<int>>(this.GrayTarget);
                algo.BlackTarget -= new EdgeEventHandler<int, Edge<int>>(this.BlackTarget);
                algo.FinishVertex -= new VertexEventHandler<int>(this.FinishVertex);
            }
        }

        protected void CheckBfs()
        {
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
                if (parents[v] != v) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }

    [TestClass]
    public class BreadthFirstAlgorithmSearchGraphMLTest
    {
        private IDictionary<IdentifiableVertex, IdentifiableVertex> parents;
        private IDictionary<IdentifiableVertex, int> distances;
        private IdentifiableVertex currentVertex;
        private IdentifiableVertex sourceVertex;
        private int currentDistance;
        private BreadthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> algo;
        private AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> g;

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
            if (u.Equals(sourceVertex))
                currentVertex = sourceVertex;
            else
            {
                Assert.AreEqual(parents[u], currentVertex);
                Assert.AreEqual(distances[u], currentDistance + 1);
                Assert.AreEqual(distances[u], distances[parents[u]] + 1);
            }
        }

        private void ExamineEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(args.Edge.Source, currentVertex);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            var u = args.Edge.Source;
            var v = args.Edge.Target;

            Assert.AreEqual(algo.VertexColors[v], GraphColor.White);
            Assert.AreEqual(distances[u], currentDistance);
            parents[v] = u;
            distances[v] = distances[u] + 1;
        }

        private void NonTreeEdge(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            var u = args.Edge.Source;
            var v = args.Edge.Target;

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
            Assert.AreEqual(algo.VertexColors[args.Edge.Target], GraphColor.Gray);
        }

        private void BlackTarget(Object sender, EdgeEventArgs<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Edge.Target], GraphColor.Black);

            foreach (var e in algo.VisitedGraph.OutEdges(args.Edge.Target))
                Assert.IsFalse(algo.VertexColors[e.Target] == GraphColor.White);
        }

        private void FinishVertex(Object sender, VertexEventArgs<IdentifiableVertex> args)
        {
            Assert.AreEqual(algo.VertexColors[args.Vertex], GraphColor.Black);
        }

        private void Init()
        {
            this.parents = new Dictionary<IdentifiableVertex, IdentifiableVertex>();
            this.distances = new Dictionary<IdentifiableVertex, int>();
            this.currentDistance = 0;
            this.currentVertex = null;
            this.algo = null;
            this.g = null;
        }

        [TestMethod]
        public void AllGraphML()
        {
            foreach (var g in GraphMLFilesHelper.GetGraphs())
                foreach (var source in g.Vertices)
                    RunBfs(g, source);
        }

        private void RunBfs(
            AdjacencyGraph<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>> g,
            IdentifiableVertex source)
        {
            this.Init();

            this.g = g;
            this.sourceVertex = source;

            this.algo = new BreadthFirstSearchAlgorithm<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(g);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<IdentifiableVertex>(this.InitializeVertex);
                algo.DiscoverVertex += new VertexEventHandler<IdentifiableVertex>(this.DiscoverVertex);
                algo.ExamineEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.ExamineEdge);
                algo.ExamineVertex += new VertexEventHandler<IdentifiableVertex>(this.ExamineVertex);
                algo.TreeEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.TreeEdge);
                algo.NonTreeEdge += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.NonTreeEdge);
                algo.GrayTarget += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.GrayTarget);
                algo.BlackTarget += new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.BlackTarget);
                algo.FinishVertex += new VertexEventHandler<IdentifiableVertex>(this.FinishVertex);

                foreach (var v in g.Vertices)
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
                algo.ExamineEdge -= new EdgeEventHandler<IdentifiableVertex, IdentifiableEdge<IdentifiableVertex>>(this.ExamineEdge);
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
                if (parents[v] != v) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }

}
