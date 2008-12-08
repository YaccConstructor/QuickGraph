using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms
{
    [TestClass, PexClass]
    public partial class TopologicalSortAlgorithmTest
    {
        [TestMethod]
        public void TopologicalSortAll()
        {
            foreach (var g in GraphMLFilesHelper.GetGraphs())
                this.SortCyclic(g);
        }

        [PexMethod]
        public void SortCyclic<TVertex,TEdge>(
            [PexAssumeNotNull]IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var topo = new TopologicalSortAlgorithm<TVertex, TEdge>(g);
            topo.Compute();
        }

        [TestMethod]
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
