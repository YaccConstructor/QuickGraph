using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TypeFixture(typeof(IUndirectedGraph<string, Edge<string>>)), PexClass]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public partial class UndirectedFirstTopologicalSortAlgorithmTest
    {

        [Test, PexMethod]
        public void Compute([PexAssumeNotNull]IUndirectedGraph<string, Edge<string>> g)
        {
            var topo =
                new UndirectedFirstTopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.AllowCyclicGraph = true;
            topo.Compute();

            Display(topo);
        }

        private void Display(UndirectedFirstTopologicalSortAlgorithm<string, Edge<string>> topo)
        {
            int index = 0;
            foreach (string v in topo.SortedVertices)
                Console.WriteLine("{0}: {1}", index++, v);
        }
    }
}
