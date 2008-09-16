using System;
using System.IO;
using System.Collections.Generic;

using QuickGraph.Unit;

using QuickGraph.Algorithms.Condensation;

namespace QuickGraph.Algorithms.Condensation
{
    [TypeFixture(typeof(IMutableVertexAndEdgeListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public class WeaklyConnectedCondensationGraphAlgorithmTest
    {
        private CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> algo;
        [Test]
        public void CondensateAndCheckVertexCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            this.algo.StronglyConnected = false;
            algo.Compute();
            CheckVertexCount(g);
        }

        [Test]
        public void CondensateAndCheckEdgeCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            this.algo.StronglyConnected = false;
            algo.Compute();
            CheckEdgeCount(g);
        }
        [Test]
        public void CondensateAndCheckComponentCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            this.algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            this.algo.StronglyConnected = false;
            algo.Compute();
            CheckComponentCount(g);
        }

        private void CheckVertexCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            int count = 0;
            foreach (AdjacencyGraph<string, Edge<string>> vertices in this.algo.CondensatedGraph.Vertices)
                count += vertices.VertexCount;
            Assert.AreEqual(g.VertexCount, count, "VertexCount does not match");
        }

        private void CheckEdgeCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // check edge count
            int count = 0;
            foreach (CondensatedEdge<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> edges in this.algo.CondensatedGraph.Edges)
                count += edges.Edges.Count;
            foreach (AdjacencyGraph<string, Edge<string>> vertices in this.algo.CondensatedGraph.Vertices)
                count += vertices.EdgeCount;
            Assert.AreEqual(g.EdgeCount, count, "EdgeCount does not match");
        }


        private void CheckComponentCount(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // check number of vertices = number of storngly connected components
            int components = AlgoUtility.WeaklyConnectedComponents<string, Edge<string>>(g, new Dictionary<string, int>());
            Assert.AreEqual(components, algo.CondensatedGraph.VertexCount, "ComponentCount does not match");
        }
    }
}
