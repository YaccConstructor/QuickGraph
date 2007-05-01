using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TypeFixture(typeof(IEdgeListGraph<string,Edge<string>>)), PexClass]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public partial class EdgeListGraphTest
    {
        [Test, PexTest]
        public void Iteration([PexAssumeIsNotNull]IEdgeListGraph<string, Edge<string>> g)
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (Edge<string> e in g.Edges)
            {
                e.ToString();
                ++i;
            }
        }

        [Test, PexTest]
        public void Count([PexAssumeIsNotNull]IEdgeListGraph<string, Edge<string>> g)
        {
            int n = g.EdgeCount;
            if (n == 0)
                Assert.IsTrue(g.IsEdgesEmpty);

            int i = 0;
            foreach (Edge<string> e in g.Edges)
            {
                e.ToString();
                ++i;
            }
            Assert.AreEqual(n, i);
        }
    }
}
