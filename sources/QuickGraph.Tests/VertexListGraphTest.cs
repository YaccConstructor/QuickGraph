using System;
using QuickGraph.Unit;

namespace QuickGraph
{
    [TypeFixture(typeof(IVertexListGraph<string,Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
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
