using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Invariants;

namespace QuickGraph
{
    [PexInvariantClass]
    public static class EdgeListGraphTest<T,E> where E : IEdge<T>
    {
        [PexInvariant]
        public static void Iteration(IEdgeListGraph<T, E> g)
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (E e in g.Edges)
                ++i;
        }

        [PexInvariant]
        public static void Count(IEdgeListGraph<T, E> g)
        {
            int n = g.EdgeCount;
            if (n == 0)
                Assert.IsTrue(g.IsEdgesEmpty);

            int i = 0;
            foreach (E e in g.Edges)
            {
                e.ToString();
                ++i;
            }
            Assert.AreEqual(n, i);
        }
    }
}
