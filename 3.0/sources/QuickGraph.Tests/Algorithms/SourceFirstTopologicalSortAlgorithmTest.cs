using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

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
            topo.Compute();
        }
    }
}
