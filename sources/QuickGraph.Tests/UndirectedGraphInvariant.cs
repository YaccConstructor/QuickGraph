using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Invariants;

namespace QuickGraph
{
    [PexInvariantClass]
    public partial class UndirectedGraphTest<T,E> where E : IEdge<T>
    {
        [PexInvariant]
        public static void IsAdjacentEdgesEmpty(IUndirectedGraph<T, E> g)
        {
            foreach (T v in g.Vertices)
            {
                Assert.AreEqual(
                    g.IsAdjacentEdgesEmpty(v),
                    g.AdjacentDegree(v) == 0);
            }
        }
    }
}
