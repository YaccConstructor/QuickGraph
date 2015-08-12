using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using QuickGraph.Algorithms.ConnectedComponents;
using System.Linq;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.ConnectedComponents
{
    [TestClass, PexClass]
    public partial class ConnectedComponentsAlgorithmTest
    {
        [TestMethod]
        [TestCategory(TestCategories.LongRunning)]
        public void ConnectedComponentsAll()
        {
            Parallel.ForEach(TestGraphFactory.GetUndirectedGraphs(), g =>
            {
                while (g.EdgeCount > 0)
                {
                    this.Compute(g);
                    g.RemoveEdge(Enumerable.First(g.Edges));
                }
            });
        }

        [PexMethod]
        public void Compute<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
             where TEdge : IEdge<TVertex>
        {
            var dfs = new ConnectedComponentsAlgorithm<TVertex, TEdge>(g);
            dfs.Compute();
            if (g.VertexCount == 0)
            {
                Assert.IsTrue(dfs.ComponentCount == 0);
                return;
            }

            Assert.IsTrue(0 < dfs.ComponentCount);
            Assert.IsTrue(dfs.ComponentCount <= g.VertexCount);
            foreach (var kv in dfs.Components)
            {
                Assert.IsTrue(0 <= kv.Value);
                Assert.IsTrue(kv.Value < dfs.ComponentCount, "{0} < {1}", kv.Value, dfs.ComponentCount);
            }

            foreach (var vertex in g.Vertices)
                foreach (var edge in g.AdjacentEdges(vertex))
                {
                    Assert.AreEqual(dfs.Components[edge.Source], dfs.Components[edge.Target]);
                }
        }
    }
}
