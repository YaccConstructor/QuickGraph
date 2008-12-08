using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Algorithms
{
    [TestClass, PexClass]
    public partial class SourceFirstTopologicalSortAlgorithmTest
    {
        [TestMethod]
        [ExpectedException(typeof(NonAcyclicGraphException))]
        public void SortCyclic()
        {
            var g = new AdjacencyGraphFactory().Loop();
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
