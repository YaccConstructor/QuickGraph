using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public static class VertexListGraphTest<T, E>
        where E : IEdge<T>
    {
#if PEX
        [PexMethod]
        public static void Iteration([PexAssumeUnderTest]IVertexListGraph<T,E> g)
        {
            int i = 0;
            foreach (T v in PexSymbolic.DropEnumeration(g.Vertices))
                ++i;
            Assert.AreEqual(g.VertexCount, i);
        }
#endif
    }
}
