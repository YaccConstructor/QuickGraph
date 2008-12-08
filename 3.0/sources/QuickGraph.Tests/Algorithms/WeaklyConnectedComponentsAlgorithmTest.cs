using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms
{
    [TestClass, PexClass]
    public partial class WeaklyConnectedComponentsAlgorithmTest
    {
        [TestMethod]
        public void WeaklyConnectedComponentsAll()
        {
            foreach (var g in GraphMLFilesHelper.GetGraphs())
                this.Compute(g);
        }

        [PexMethod]
        public void Compute<TVertex,TEdge>([PexAssumeNotNull]IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            GraphConsoleSerializer.DisplayGraph(g);

            var dfs = 
                new WeaklyConnectedComponentsAlgorithm<TVertex,TEdge>(g);
            dfs.Compute();

            Console.WriteLine("Weak components: {0}", dfs.ComponentCount);
            Assert.AreEqual(g.VertexCount, dfs.Components.Count);

            foreach(var kv in dfs.Components)
            {
                Assert.IsTrue(0 <= kv.Value);
                Assert.IsTrue(kv.Value < dfs.ComponentCount);
            }

            foreach(var vertex in g.Vertices)
                foreach (var edge in g.OutEdges(vertex))
                {
                    Assert.AreEqual(dfs.Components[edge.Source], dfs.Components[edge.Target]);
                }
        }
    }
}
