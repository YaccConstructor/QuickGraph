using System;
using System.Linq;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Search
{
#if PARALLEL
    using System.Threading.Tasks;
    [TestFixture]
    public class ParallelBreadthFirstAlgorithmSearchTest
    {
        private IDictionary<int, int> parents;
        private IDictionary<int, int> distances;
        private int sourceVertex;
        private int currentDistance;
        private ParallelBreadthFirstSearchAlgorithm<int, Edge<int>, int> algo;
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

        class IntFactory
        {
            int id;
            public int Next()
            {
                return this.id++;
            }
        }

        [Test]
        public void GraphWithSelfEdgesBig()
        {
            TestConsole.WriteLine("processors: {0}", TaskManager.Current.Policy.IdealProcessors);
            Random rnd = new Random();
            g = new AdjacencyGraph<int, Edge<int>>(true);
            var next = new IntFactory();
            RandomGraphFactory.Create<int, Edge<int>>(g,
                next.Next,
                (s,t) => new Edge<int>(s,t),
                rnd, 5000, 20000, false);

            var sv = g.Vertices.FirstOrDefault();
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

            var next = new IntFactory();
            g = new AdjacencyGraph<int, Edge<int>>(true);
            RandomGraphFactory.Create<int, Edge<int>>(g,
                next.Next,
                (s, t) => new Edge<int>(s, t),
                rnd, i, j, true);

            foreach (var sv in g.Vertices)
            {
                this.sourceVertex = sv;
                RunBfs();
            }
        }

        private void RunBfs()
        {
            algo = new ParallelBreadthFirstSearchAlgorithm<int, Edge<int>, int>(g);
            try
            {
                algo.InitializeVertex += new VertexEventHandler<int>(this.InitializeVertex);
                algo.DiscoverVertex += new ParallelVertexEventHandler<int,int>(this.DiscoverVertex);
                algo.ExamineVertex += new ParallelVertexEventHandler<int, int>(this.ExamineVertex);
                algo.TreeEdge += new ParallelEdgeAction<int, Edge<int>, int>(this.TreeEdge);
                algo.NextLevel += new EventHandler(algo_NextLevel);
                algo.FinishVertex += new ParallelVertexEventHandler<int,int>(this.FinishVertex);

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
                algo.DiscoverVertex -= new ParallelVertexEventHandler<int,int>(this.DiscoverVertex);
                algo.ExamineVertex -= new ParallelVertexEventHandler<int,int>(this.ExamineVertex);
                algo.TreeEdge -= new ParallelEdgeAction<int, Edge<int>,int>(this.TreeEdge);
                algo.FinishVertex -= new ParallelVertexEventHandler<int,int>(this.FinishVertex);
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
#endif
}
