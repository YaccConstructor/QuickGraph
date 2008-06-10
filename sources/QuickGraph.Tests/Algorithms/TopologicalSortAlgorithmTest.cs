using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class TopologicalSortAlgorithmTest
    {
        [PexMethod]
        public void SortCyclic(
            [PexAssumeNotNull]IVertexListGraph<string,Edge<string>> g)
        {
            TopologicalSortAlgorithm<string, Edge<string>> topo = new TopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
