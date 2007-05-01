using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    [TypeFixture(typeof(IUndirectedGraph<string,Edge<string>>)), PexClass]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public partial class UndirectedGraphTest
    {
        [Test, PexTest]
        public void IsAdjacentEdgesEmpty([PexAssumeIsNotNull]IUndirectedGraph<string, Edge<string>> g)
        {
            foreach (string v in g.Vertices)
            {
                Console.Write('.');
                Assert.AreEqual(
                    g.IsAdjacentEdgesEmpty(v),
                    g.AdjacentDegree(v) == 0);
            }
        }
    }
}
