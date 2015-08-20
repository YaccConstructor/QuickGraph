using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public static class VertexListGraphTest<T, E>
        where E : IEdge<T>
    {
        [PexMethod]
        public static void Iteration([PexAssumeUnderTest]IVertexListGraph<T,E> g)
        {
            int i = 0;
            foreach (T v in PexSymbolicValue.IgnoreEnumeration(g.Vertices))
                ++i;
            Assert.AreEqual(g.VertexCount, i);
        }
    }
}
