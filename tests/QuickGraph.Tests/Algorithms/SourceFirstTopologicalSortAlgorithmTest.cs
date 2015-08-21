using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.TopologicalSort;

namespace QuickGraph.Algorithms
{
    [TestClass, PexClass]
    public partial class SourceFirstTopologicalSortAlgorithmTest
    {
        [TestMethod]
        public void SortAll()
        {
            foreach(var g in TestGraphFactory.GetAdjacencyGraphs())
                this.Sort(g);
        }

        [PexMethod]
        public void Sort<TVertex, TEdge>([PexAssumeNotNull]IVertexAndEdgeListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var topo = new SourceFirstTopologicalSortAlgorithm<TVertex, TEdge>(g);
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

            var topo = new SourceFirstTopologicalSortAlgorithm<int, Edge<int>>(g);
            topo.Compute();
        }

        [TestMethod]
        [DeploymentItem("GraphML/DCT8.graphml", "GraphML")]
        public void SortDCT()
        {
            var g = TestGraphFactory.LoadBidirectionalGraph("GraphML/DCT8.graphml");

            var topo = new SourceFirstTopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
