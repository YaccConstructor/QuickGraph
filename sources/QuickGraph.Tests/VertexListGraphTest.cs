using System;
using MbUnit.Framework;

namespace QuickGraph
{
    [TypeFixture(typeof(IVertexListGraph<string,Edge<string>>))]
    [ProviderFactory(typeof(AdjacencyGraphFactory), typeof(IVertexListGraph<string, Edge<string>>))]
    [ProviderFactory(typeof(BidirectionalGraphFactory), typeof(IVertexListGraph<string, Edge<string>>))]
    [ProviderFactory(typeof(UndirectedGraphFactory), typeof(IVertexListGraph<string, Edge<string>>))]
    public class VertexListGraphTest
    {
        [Test]
        public void Count(IVertexListGraph<string,Edge<string>> g)
        {
            int n = g.VertexCount;
        }

        [Test]
        public void Iteration(IVertexListGraph<string, Edge<string>> g)
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
