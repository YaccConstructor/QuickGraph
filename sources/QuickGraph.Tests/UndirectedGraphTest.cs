using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph
{
    [TypeFixture(typeof(IUndirectedGraph<string,Edge<string>>))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class UndirectedGraphTest
    {
        [Test]
        public void IsAdjacentEdgesEmpty(IUndirectedGraph<string, Edge<string>> g)
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
