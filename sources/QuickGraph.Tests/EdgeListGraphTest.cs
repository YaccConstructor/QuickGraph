using System;
using QuickGraph.Unit;

namespace QuickGraph
{
    [TypeFixture(typeof(IEdgeListGraph<string,Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    [TypeFactory(typeof(BidirectionalGraphFactory))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class EdgeListGraphTest
    {
        [Test]
        public void Iteration(IEdgeListGraph<string, Edge<string>> g)
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (Edge<string> e in g.Edges)
            {
                e.ToString();
                ++i;
            }
        }

        [Test]
        public void Count(IEdgeListGraph<string, Edge<string>> g)
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
