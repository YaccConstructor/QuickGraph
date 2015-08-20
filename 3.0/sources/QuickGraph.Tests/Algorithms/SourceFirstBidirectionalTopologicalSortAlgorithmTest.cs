using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.TopologicalSort;

namespace QuickGraph.Algorithms
{
    [TestClass, PexClass]
    public partial class SourceFirstBidirectionalTopologicalSortAlgorithmTest
    {
        [TestMethod]
        public void SortAll()
        {
            foreach (var g in TestGraphFactory.GetBidirectionalGraphs())
            {
                this.Sort(g, TopologicalSortDirection.Forward);
                this.Sort(g, TopologicalSortDirection.Backward);
            }
        }

        [PexMethod]
        public void Sort<TVertex, TEdge>([PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> g, TopologicalSortDirection direction)
            where TEdge : IEdge<TVertex>
        {
            var topo = new SourceFirstBidirectionalTopologicalSortAlgorithm<TVertex, TEdge>(g, direction);
            try
            {
                topo.Compute();
            }
            catch (NonAcyclicGraphException)
            { }
        }

        [TestMethod]
        public void SortAnotherOne()
        {
            var g = new BidirectionalGraph<int, Edge<int>>();

            g.AddVertexRange(new int[5] { 0, 1, 2, 3, 4 });
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(1, 2));
            g.AddEdge(new Edge<int>(1, 3));
            g.AddEdge(new Edge<int>(2, 3));
            g.AddEdge(new Edge<int>(3, 4));

            SourceFirstBidirectionalTopologicalSortAlgorithm<int, Edge<int>> topo;

            topo = new SourceFirstBidirectionalTopologicalSortAlgorithm<int, Edge<int>>(g, TopologicalSortDirection.Forward);
            topo.Compute();

            topo = new SourceFirstBidirectionalTopologicalSortAlgorithm<int, Edge<int>>(g, TopologicalSortDirection.Backward);
            topo.Compute();
        }

        [TestMethod]
        public void SortDCT()
        {
            var g = TestGraphFactory.LoadBidirectionalGraph("DCT8.graphml");

            SourceFirstBidirectionalTopologicalSortAlgorithm<string, Edge<string>> topo;

            topo = new SourceFirstBidirectionalTopologicalSortAlgorithm<string, Edge<string>>(g, TopologicalSortDirection.Forward);
            topo.Compute();

            topo = new SourceFirstBidirectionalTopologicalSortAlgorithm<string, Edge<string>>(g, TopologicalSortDirection.Backward);
            topo.Compute();
        }
    }
}
