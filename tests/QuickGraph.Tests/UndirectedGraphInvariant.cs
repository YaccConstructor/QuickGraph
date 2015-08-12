using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph
{
    [TestClass, PexClass]
    public partial class UndirectedGraphTest
    {
        [PexMethod]
        public static void IsAdjacentEdgesEmpty<T,E>([PexAssumeUnderTest]IUndirectedGraph<T, E> g)
            where E : IEdge<T>
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
