using System;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms
{
    [TestFixture]
    public class SourceFirstTopologicalSortAlgorithmTest
    {
        [Test]
        [ExpectedException(typeof(NonAcyclicGraphException))]
        public void SortCyclic()
        {
            IVertexAndEdgeListGraph<string, Edge<string>> g = new AdjacencyGraphFactory().Loop();
            SourceFirstTopologicalSortAlgorithm<string, Edge<string>> topo = new SourceFirstTopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.Compute();
        }
    }
}
