using System;
using System.Collections.Generic;

using MbUnit.Framework;

using QuickGraph.Collections;
using QuickGraph.Predicates;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath
{
    [TestFixture]
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

            Assert.AreEqual(0, dij.Distances[1], 0.0001);
            Assert.AreEqual(1, dij.Distances[2], 0.0001);
            Assert.AreEqual(2, dij.Distances[3], 0.0001);
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
}
