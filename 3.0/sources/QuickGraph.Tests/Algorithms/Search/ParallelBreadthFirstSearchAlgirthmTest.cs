using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph.Operations;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Search
{
    [TestFixture]
    public class ParallelBreadthFirstAlgorithmSearchTest
    {
        private IDictionary<int, int> parents;
        private IDictionary<int, int> distances;
        private int sourceVertex;
        private int currentDistance;
        private ParallelBreadthFirstSearchAlgorithm<int, Edge<int>> algo;
        private AdjacencyGraph<int, Edge<int>> g;

        private void InitializeVertex(Object sender, VertexEventArgs<int> args)
        {
            Assert.AreEqual(algo.GetVertexColor(args.Vertex), GraphColor.White);
        }

        private void ExamineVertex(Object sender, VertexEventArgs<int> args)
        {
            int u = args.Vertex;
            // Ensure that the distances monotonically increase.
            Assert.IsTrue(distances[u] == currentDistance
                       || distances[u] == currentDistance + 1);
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<int> args)
        {
            int u = args.Vertex;

            Assert.AreEqual(algo.GetVertexColor(u), GraphColor.Gray);
            if (u != sourceVertex)
            {
                Assert.AreEqual(distances[u], currentDistance + 1);
                Assert.AreEqual(distances[u], distances[parents[u]] + 1);
            }
        }

        private void TreeEdge(Object sender, EdgeEventArgs<int, Edge<int>> args)
        {
            int u = args.Edge.Source;
            int v = args.Edge.Target;

            Assert.AreEqual(distances[u], currentDistance);
            this.parents[v] = u;
            this.distances[v] = distances[u] + 1;
        }

        private void FinishVertex(Object sender, VertexEventArgs<int> args)
        {
            Assert.AreEqual(algo.GetVertexColor(args.Vertex), GraphColor.Gray);
        }

        [SetUp]
        public void Init()
        {
            this.parents = new Dictionary<int, int>();
            this.distances = new Dictionary<int, int>();
            this.currentDistance = 0;
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

        [Test]
        public void GraphWithSelfEdgesBig()
        {
            Console.WriteLine("processors: {0}", TaskManager.Current.Policy.IdealProcessors);
            Random rnd = new Random();
            g = new AdjacencyGraph<int, Edge<int>>(true);
            RandomGraphFactory.Create<int, Edge<int>>(g,
                new IntVertexFactory(),
                FactoryCompiler.GetEdgeFactory<int, Edge<int>>(),
                rnd, 10000, 100000, false);

            var sv = g.GetFirstVertexOrDefault();
            this.sourceVertex = sv;
            RunBfs();
        }

        [CombinatorialTest(CombinationType.PairWize)]
        public void GraphWithSelfEdges(
            [UsingLinear(2, 5)] int i,
            [UsingLinear(0, 25)] int j
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

            foreach (var sv in g.Vertices)
            {
                this.sourceVertex = sv;
                RunBfs();
            }
        }

        private void RunBfs()
        {
            algo = new ParallelBreadthFirstSearchAlgorithm<int, Edge<int>>(g);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<int>(this.InitializeVertex);
                algo.DiscoverVertex += new VertexEventHandler<int>(this.DiscoverVertex);
                algo.ExamineVertex += new VertexEventHandler<int>(this.ExamineVertex);
                algo.TreeEdge += new EdgeEventHandler<int, Edge<int>>(this.TreeEdge);
                algo.NextLevel += new EventHandler(algo_NextLevel);
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

                this.CheckBfs();
            }
            finally
            {
                algo.InitializeVertex -= new VertexEventHandler<int>(this.InitializeVertex);
                algo.DiscoverVertex -= new VertexEventHandler<int>(this.DiscoverVertex);
                algo.ExamineVertex -= new VertexEventHandler<int>(this.ExamineVertex);
                algo.TreeEdge -= new EdgeEventHandler<int, Edge<int>>(this.TreeEdge);
                algo.FinishVertex -= new VertexEventHandler<int>(this.FinishVertex);
            }
        }

        void algo_NextLevel(object sender, EventArgs e)
        {
            this.currentDistance++;
        }

        protected void CheckBfs()
        {
            // All white vertices should be unreachable from the source.
            foreach (int v in g.Vertices)
            {
                if (algo.GetVertexColor(v) == GraphColor.White)
                {
                    //!IsReachable(start,u,g);
                }
            }

            // The shortest path to a child should be one longer than
            // shortest path to the parent.
            foreach (int v in g.Vertices)
            {
                if (parents[v] != v) // *ui not the root of the bfs tree
                {
                    Assert.AreEqual(distances[v], distances[parents[v]] + 1);
                }
            }
        }
    }
}
