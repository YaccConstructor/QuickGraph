using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;
using System.Threading.Tasks;

namespace QuickGraph.Tests.Algorithms.MaximumFlow
{
    [TestClass]
    public partial class EdmondsKarpMaximumFlowAlgorithmTest
    {
        [TestMethod]
        public void EdmondsKarpMaxFlowAll()
        {
            Parallel.ForEach(TestGraphFactory.GetAdjacencyGraphs(), g =>
            {
                if (g.VertexCount > 0)
                    this.EdmondsKarpMaxFlow(g, (source, target) => new Edge<string>(source, target));
            });
        }


        [PexMethod]
        public void EdmondsKarpMaxFlow<TVertex, TEdge>([PexAssumeNotNull]IMutableVertexAndEdgeListGraph<TVertex, TEdge> g, 
            EdgeFactory<TVertex, TEdge> edgeFactory)
            where TEdge : IEdge<TVertex>
        {
            PexAssume.IsTrue(g.VertexCount > 0);

            foreach (var source in g.Vertices)
                foreach (var sink in g.Vertices)
                {
                    if (source.Equals(sink)) continue;

                    RunMaxFlowAlgorithm<TVertex, TEdge>(g, edgeFactory, source, sink);
                }
        }

        private static double RunMaxFlowAlgorithm<TVertex, TEdge>(IMutableVertexAndEdgeListGraph<TVertex, TEdge> g, EdgeFactory<TVertex, TEdge> edgeFactory, TVertex source, TVertex sink) where TEdge : IEdge<TVertex>
        {
            TryFunc<TVertex, TEdge> flowPredecessors;
            var flow = AlgorithmExtensions.MaximumFlowEdmondsKarp<TVertex, TEdge>(
                g,
                e => 1,
                source, sink,
                out flowPredecessors,
                edgeFactory
                );

            return flow;
        }

    }
}
