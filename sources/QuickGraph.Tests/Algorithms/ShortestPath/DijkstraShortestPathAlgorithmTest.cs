using System;
using System.Collections.Generic;

using QuickGraph.Unit;

using QuickGraph.Collections;
using QuickGraph.Predicates;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TypeFixture(typeof(IVertexAndEdgeListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    public class DijkstraShortestPathExamplesTest
    {
        [Test]
        public void Compute(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            if (g.VertexCount == 0) return;

            var rnd = new Random();
            var distances = new Dictionary<Edge<string>, double>();
            foreach(var edge in g.Edges)
                distances.Add(edge, rnd.Next(100));
            var bfs = new DijkstraShortestPathAlgorithm<string, Edge<string>>(g, distances);

            bfs.Compute(TraversalHelper.GetFirstVertex(g));
        }
    }

    [TestFixture, CurrentFixture]
    public class DijkstraShortestPathTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAlgorithmWithNullGraph()
        {
            DijkstraShortestPathAlgorithm<int,Edge<int>> dij = new
                DijkstraShortestPathAlgorithm<int, Edge<int>>(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAlgorithmWithNullWeights()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            DijkstraShortestPathAlgorithm<int, Edge<int>> dij =
                new DijkstraShortestPathAlgorithm<int, Edge<int>>(g, null);
        }

        [Test]
        public void UnaryWeights()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            Dictionary<Edge<int>, double> weights =
                DijkstraShortestPathAlgorithm<int,Edge<int>>.UnaryWeightsFromEdgeList(g);
        }

        [Test]
        public void RunOnLineGraph()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            g.AddEdge(new Edge<int>(1,2));
            g.AddEdge(new Edge<int>(2,3));

            Dictionary<Edge<int>,double> weights = 
                DijkstraShortestPathAlgorithm<int,Edge<int>>.UnaryWeightsFromEdgeList(g);
            DijkstraShortestPathAlgorithm<int, Edge<int>> dij = new DijkstraShortestPathAlgorithm<int, Edge<int>>(g, weights);
            dij.Compute(1);

            Assert.AreEqual<double>(0, dij.Distances[1]);
            Assert.AreEqual<double>(1, dij.Distances[2]);
            Assert.AreEqual<double>(2, dij.Distances[3]);
        }

        [Test]
        public void CheckPredecessorLineGraph()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            Edge<int> e12 = new Edge<int>(1, 2); g.AddEdge(e12);
            Edge<int> e23 = new Edge<int>(2, 3); g.AddEdge(e23);

            Dictionary<Edge<int>, double> weights =
                DijkstraShortestPathAlgorithm<int, Edge<int>>.UnaryWeightsFromEdgeList(g);
            DijkstraShortestPathAlgorithm<int, Edge<int>> dij = new DijkstraShortestPathAlgorithm<int, Edge<int>>(g, weights);
            VertexPredecessorRecorderObserver<int, Edge<int>> vis = new VertexPredecessorRecorderObserver<int, Edge<int>>();
            vis.Attach(dij);
            dij.Compute(1);

            IList<Edge<int>> col = vis.Path(2);
            Assert.AreEqual(1, col.Count);
            Assert.AreEqual(e12, col[0]);

            col = vis.Path(3);
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual(e12, col[0]);
            Assert.AreEqual(e23, col[1]);
        }


        [Test]
        public void RunOnDoubleLineGraph()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            Edge<int> e12 = new Edge<int>(1, 2); g.AddEdge(e12);
            Edge<int> e23 = new Edge<int>(2, 3); g.AddEdge(e23);
            Edge<int> e13 = new Edge<int>(1, 3); g.AddEdge(e13);

            Dictionary<Edge<int>,double> weights = DijkstraShortestPathAlgorithm<int, Edge<int>>.UnaryWeightsFromEdgeList(g);
            DijkstraShortestPathAlgorithm<int, Edge<int>> dij = new DijkstraShortestPathAlgorithm<int, Edge<int>>(g, weights);
            dij.Compute(1);

            Assert.AreEqual(0.0, dij.Distances[1]);
            Assert.AreEqual(1.0, dij.Distances[2]);
            Assert.AreEqual(1.0, dij.Distances[3]);
        }

        [Test]
        public void CheckPredecessorDoubleLineGraph()
        {
            AdjacencyGraph<int, Edge<int>> g = new AdjacencyGraph<int, Edge<int>>(true);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            Edge<int> e12 = new Edge<int>(1, 2); g.AddEdge(e12);
            Edge<int> e23 = new Edge<int>(2, 3); g.AddEdge(e23);
            Edge<int> e13 = new Edge<int>(1, 3); g.AddEdge(e13);

            Dictionary<Edge<int>, double> weights = DijkstraShortestPathAlgorithm<int, Edge<int>>.UnaryWeightsFromEdgeList(g);
            DijkstraShortestPathAlgorithm<int, Edge<int>> dij = new DijkstraShortestPathAlgorithm<int, Edge<int>>(g, weights);
            VertexPredecessorRecorderObserver<int, Edge<int>> vis = new VertexPredecessorRecorderObserver<int, Edge<int>>();
            vis.Attach(dij);
            dij.Compute(1);

            IList<Edge<int>> col = vis.Path(2);
            Assert.AreEqual(1, col.Count);
            Assert.AreEqual(e12, col[0]);

            col = vis.Path(3);
            Assert.AreEqual(1, col.Count);
            Assert.AreEqual(e13, col[0]);
        }
    }

    [TestFixture, CurrentFixture]
    public class DijkstraAlgoTest
    {
        AdjacencyGraph<string, Edge<string>> graph;
        DijkstraShortestPathAlgorithm<string, Edge<string>> algo;
        List<string> path;
        VertexPredecessorRecorderObserver<string, Edge<string>> predecessorObserver;

        [Test]
        public void CreateGraph()
        {
            graph = new AdjacencyGraph<string, Edge<string>>(true);

            // Add some vertices to the graph
            graph.AddVertex("A");
            graph.AddVertex("B");

            graph.AddVertex("D");
            graph.AddVertex("C");
            graph.AddVertex("E");

            // Create the edges
            var a_b = new Edge<string>("A", "B");
            var a_c = new Edge<string>("A", "C");
            var b_e = new Edge<string>("B", "E");
            var c_d = new Edge<string>("C", "D");
            var d_e = new Edge<string>("D", "E");

            // Add edges to the graph
            graph.AddEdge(a_b);
            graph.AddEdge(a_c);
            graph.AddEdge(c_d);
            graph.AddEdge(d_e);
            graph.AddEdge(b_e);

            // Define some weights to the edges
            var weight = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            weight.Add(a_b, 30);
            weight.Add(a_c, 30);
            weight.Add(b_e, 60);
            weight.Add(c_d, 40);
            weight.Add(d_e, 4);

            algo = new DijkstraShortestPathAlgorithm<string, Edge<string>>(graph, weight);

            // Attach a Vertex Predecessor Recorder Observer to give us the paths
            predecessorObserver = new VertexPredecessorRecorderObserver<string, Edge<string>>();

            using (ObserverScope.Create<IVertexPredecessorRecorderAlgorithm<string, Edge<string>>>(algo, predecessorObserver))
            {
                // Run the algorithm with A set to be the source
                algo.Compute("A");
            }

            path = new List<string>();
            PopulatePath("E");

            Assert.IsTrue(algo.Distances["E"] == 74);
            path.Reverse();

            Console.WriteLine(String.Join(" -> ", path.ToArray()));
        }

        void PopulatePath(string vertex)
        {
            path.Add(vertex);
            if (vertex == "A")
                return;
            PopulatePath(predecessorObserver.VertexPredecessors[vertex].Source);
        }
    }

    [TestFixture, CurrentFixture]
    public class BoostDijkstraTest
    {
        [Test]
        public void Compute()
        {
            var g = new AdjacencyGraph<char, Edge<char>>();
            var distances = new Dictionary<Edge<char>, double>();

            g.AddVertexRange("ABCDE");
            AddEdge(g, distances, 'A', 'C', 1);
            AddEdge(g, distances, 'B', 'B', 2);
            AddEdge(g, distances, 'B', 'D', 1);
            AddEdge(g, distances, 'B', 'E', 2);
            AddEdge(g, distances, 'C', 'B', 7);
            AddEdge(g, distances, 'C', 'D', 3);
            AddEdge(g, distances, 'D', 'E', 1);
            AddEdge(g, distances, 'E', 'A', 1);
            AddEdge(g, distances, 'E', 'B', 1);

            var dijkstra = new DijkstraShortestPathAlgorithm<char, Edge<char>>(g, distances);
            var predecessors = new VertexPredecessorRecorderObserver<char, Edge<char>>();

            predecessors.Attach(dijkstra);
            dijkstra.Compute('A');

            Assert.AreEqual(0, dijkstra.Distances['A']);
            Assert.AreEqual(6, dijkstra.Distances['B']);
            Assert.AreEqual(1, dijkstra.Distances['C']);
            Assert.AreEqual(4, dijkstra.Distances['D']);
            Assert.AreEqual(5, dijkstra.Distances['E']);
        }

        private static void AddEdge(
            AdjacencyGraph<char, Edge<char>> g, 
            Dictionary<Edge<char>, double> distances,
            char source, char target, double weight)
        {
            var ac = new Edge<char>(source, target); distances[ac] = weight; g.AddEdge(ac);
        }
    }
}
