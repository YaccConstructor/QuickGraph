using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    [CurrentFixture]
    public partial class TopologicalSortAlgorithmTest
    {
        [PexMethod]
        public void SortCyclic(
            [PexAssumeNotNull]IVertexListGraph<string,Edge<string>> g)
        {
            TopologicalSortAlgorithm<string, Edge<string>> topo = new TopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }

        [Test]
        public void OneTwo()
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddEdge(new Edge<int>(1, 2));
            var t = new TopologicalSortAlgorithm<int, Edge<int>>(graph);
            t.Compute();
        }
    }
}
