using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Invariants;

namespace QuickGraph
{
    [PexInvariantClass]
    public static class VertexListGraphTest<T,E>
        where E : IEdge<T>
    {
        [PexInvariant]
        public static void Iteration(IVertexListGraph<T,E> g)
        {
            int i = 0;
            foreach (T v in PexSymbolic.DropEnumeration(g.Vertices))
                ++i;
            Assert.AreEqual(g.VertexCount, i);
        }
    }
}
