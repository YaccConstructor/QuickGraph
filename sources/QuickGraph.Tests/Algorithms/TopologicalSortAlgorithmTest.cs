using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class TopologicalSortAlgorithmTest
    {
        [Test, PexTest]
        [ExpectedException(typeof(NonAcyclicGraphException))]
        public void SortCyclic()
        {
            IVertexListGraph<string,Edge<string>> g=new AdjacencyGraphFactory().Loop();
            TopologicalSortAlgorithm<string, Edge<string>> topo = new TopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
