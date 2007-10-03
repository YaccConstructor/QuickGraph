using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public static class EdgeListGraphTest<T, E> where E : IEdge<T>
    {
        [PexTest]
        public static void Iteration([PexTarget]IEdgeListGraph<T, E> g)
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (E e in g.Edges)
                ++i;
        }

        [PexTest]
        public static void Count([PexTarget]IEdgeListGraph<T, E> g)
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
