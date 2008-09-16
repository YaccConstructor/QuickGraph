using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TestFixture, PexClass]
    public partial class UndirectedGraphTest<T,E> where E : IEdge<T>
    {
        [PexMethod]
        public static void IsAdjacentEdgesEmpty([PexAssumeUnderTest]IUndirectedGraph<T, E> g)
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
