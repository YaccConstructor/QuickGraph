using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TypeFixture(typeof(IVertexListGraph<string,Edge<string>>)), PexClass]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public partial class VertexListGraphTest
    {
        [Test, PexTest]
        public void Count([PexAssumeIsNotNull]IVertexListGraph<string,Edge<string>> g)
        {
            int n = g.VertexCount;
        }

        [Test, PexTest]
        public void Iteration([PexAssumeIsNotNull]IVertexListGraph<string, Edge<string>> g)
        {
            int n = g.VertexCount;
            int i = 0;
            foreach (string v in g.Vertices)
            {
                v.ToString();
                ++i;
            }
            Assert.AreEqual(n, i);
        }
    }
}
