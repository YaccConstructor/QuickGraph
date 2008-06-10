using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public static class EdgeListGraphTest<T, E> where E : IEdge<T>
    {
        [PexMethod]
        public static void Iteration([PexAssumeUnderTest]IEdgeListGraph<T, E> g)
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (E e in g.Edges)
                ++i;
        }

        [PexMethod]
        public static void Count([PexAssumeUnderTest]IEdgeListGraph<T, E> g)
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
