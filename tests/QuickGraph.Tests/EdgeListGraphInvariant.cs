using System;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph
{
    [TestClass, PexClass]
    public static class EdgeListGraphTest
    {
        [PexMethod]
        public static void Iteration<T,E>([PexAssumeUnderTest]IEdgeListGraph<T, E> g)
            where E : IEdge<T>
        {
            int n = g.EdgeCount;
            int i = 0;
            foreach (E e in g.Edges)
                ++i;
        }

        [PexMethod]
        public static void Count<T,E>([PexAssumeUnderTest]IEdgeListGraph<T, E> g)
            where E : IEdge<T>
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
