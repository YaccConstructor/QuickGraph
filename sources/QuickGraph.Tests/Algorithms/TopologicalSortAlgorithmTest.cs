using System;
using MbUnit.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture]
    public class TopologicalSortAlgorithmTest
    {
        [Test]
        [ExpectedException(typeof(NonAcyclicGraphException))]
        public void SortCyclic()
        {
            IVertexListGraph<string,Edge<string>> g=new AdjacencyGraphFactory().Loop();
            TopologicalSortAlgorithm<string, Edge<string>> topo = new TopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
