using System;
using System.IO;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Algorithms.Condensation
{
    [TestClass, PexClass]
    public partial class StronglyConnectedCondensationGraphAlgorithmTest
    {
        [PexMethod]
        public void CondensateAndCheckVertexCount(
            [PexAssumeNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckVertexCount(g, algo);
        }

        [PexMethod]
        public void CondensateAndCheckEdgeCount(
            [PexAssumeNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckEdgeCount(g, algo);
        }
        [PexMethod]
        public void CondensateAndCheckComponentCount(
            [PexAssumeNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckComponentCount(g, algo);
        }

        [PexMethod]
        public void CondensateAndCheckDAG(
            [PexAssumeNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var algo = new CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>>(g);
            algo.Compute();
            CheckDAG(g, algo);
        }

        private void CheckVertexCount(
            IVertexAndEdgeListGraph<string, Edge<string>> g,
            CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> algo)
        {
            int count = 0;
            foreach (AdjacencyGraph<string,Edge<string>> vertices in algo.CondensatedGraph.Vertices)
                count += vertices.VertexCount;
            Assert.AreEqual(g.VertexCount, count, "VertexCount does not match");
        }

        private void CheckEdgeCount(IVertexAndEdgeListGraph<string, Edge<string>> g,
            CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> algo)
        {
            // check edge count
            int count = 0;
            foreach (CondensatedEdge<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> edges in algo.CondensatedGraph.Edges)
                count += edges.Edges.Count;
            foreach (AdjacencyGraph<string, Edge<string>> vertices in algo.CondensatedGraph.Vertices)
                count += vertices.EdgeCount;
            Assert.AreEqual(g.EdgeCount, count, "EdgeCount does not match");
        }


        private void CheckComponentCount(IVertexAndEdgeListGraph<string, Edge<string>> g,
            CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> algo)
        {
            // check number of vertices = number of storngly connected components
            int components = g.StronglyConnectedComponents<string, Edge<string>>(new Dictionary<string, int>());
            Assert.AreEqual(components, algo.CondensatedGraph.VertexCount, "ComponentCount does not match");
        }

        private void CheckDAG(IVertexAndEdgeListGraph<string, Edge<string>> g,
            CondensationGraphAlgorithm<string, Edge<string>, AdjacencyGraph<string, Edge<string>>> algo)
        {
            // check it's a dag
            try
            {
                algo.CondensatedGraph.TopologicalSort();
            }
            catch (NonAcyclicGraphException)
            {
                Assert.Fail("Graph is not a DAG.");
            }

        }
    }
}
