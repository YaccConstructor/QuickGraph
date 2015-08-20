using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.Search
{
    [TestFixture]
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

        [SetUp]
        public void Init()
        {
            this.parents = new Dictionary<int, int>();
            this.distances = new Dictionary<int, int>();
            this.currentDistance = 0;
            this.currentVertex = 0;
            this.algo = null;
            this.g = null;
        }

        private class IntVertexFactory : IVertexFactory<int>
        {
            private int id = 0;
            public int CreateVertex()
            {
                return id++;
            }
        }

        [CombinatorialTest]
        public void GraphWithSelfEdges(
            [UsingLinear(2, 9)] int i,
            [UsingLinear(0, 10)] int j
            )
        {
            if (i == 0 && j == 0)
                return;

            Random rnd = new Random();

            g = new AdjacencyGraph<int, Edge<int>>(true);
            RandomGraphFactory.Create<int, Edge<int>>(g,
                new IntVertexFactory(),
                FactoryCompiler.GetEdgeFactory<int,Edge<int>>(),
                rnd, i, j, true);

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
                sourceVertex = RandomGraphFactory.GetVertex(g, rnd);

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
            foreach (int v in g.Vertices)
            {
                if (algo.VertexColors[v] == GraphColor.White)
                {
                    //!IsReachable(start,u,g);
                }
            }

            // The shortest path to a child should be one longer than
            // shortest path to the parent.
            foreach (int v in g.Vertices)
            {
                if (parents[v] != v) // *ui not the root of the bfs tree
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
            }
        }
    }
}
