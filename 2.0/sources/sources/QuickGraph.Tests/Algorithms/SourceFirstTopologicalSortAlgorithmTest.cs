using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class SourceFirstTopologicalSortAlgorithmTest
    {
        [Test]
        [ExpectedException(typeof(NonAcyclicGraphException))]
        public void SortCyclic()
        {
            IVertexAndEdgeListGraph<string, Edge<string>> g = new AdjacencyGraphFactory().Loop();
            this.Sort(g);
        }

        [PexMethod]
        public void Sort([PexAssumeNotNull]IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            SourceFirstTopologicalSortAlgorithm<string, Edge<string>> topo = new SourceFirstTopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
