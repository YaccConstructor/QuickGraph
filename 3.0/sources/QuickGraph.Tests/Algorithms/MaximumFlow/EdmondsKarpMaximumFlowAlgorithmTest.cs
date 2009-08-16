using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.MaximumFlow;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms.MaximumFlow
{
    [TestClass]
    public partial class EdmdndsKarpMaximumFlowAlgorithmTest
    {
        [TestMethod]
        public void EdmondsKarpMaxFlowAll()
        {
            foreach (var g in TestGraphFactory.GetAdjacencyGraphs())
                if (g.VertexCount > 0)
                    this.EdmondsKarpMaxFlow(g);
        }

        [PexMethod]
        public void EdmondsKarpMaxFlow<TVertex, TEdge>([PexAssumeNotNull]IVertexAndEdgeListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            PexAssume.IsTrue(g.VertexCount > 0);

            foreach (var v in g.Vertices)
                foreach (var w in g.Vertices)
                {
                    TryFunc<TVertex, TEdge> flowPredecessors;
                    var flow = AlgorithmExtensions.MaximumFlowEdmondsKarp<TVertex, TEdge>(
                        g,
                        e => 1,
                        v, w,
                        out flowPredecessors
                        );
                }
        }
    }
}
