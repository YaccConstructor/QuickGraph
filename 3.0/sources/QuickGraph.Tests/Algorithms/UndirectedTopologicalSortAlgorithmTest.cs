using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class UndirectedTopologicalSortAlgorithmTest
    {
        [PexMethod]
        public void Compute([PexAssumeNotNull]IUndirectedGraph<string, Edge<string>> g)
        {
            UndirectedTopologicalSortAlgorithm<string, Edge<string>> topo =
                new UndirectedTopologicalSortAlgorithm<string, Edge<string>>(g);
            topo.AllowCyclicGraph = true;
            topo.Compute();

            Display(topo);
        }

        private void Display(UndirectedTopologicalSortAlgorithm<string, Edge<string>> topo)
        {
            int index = 0;
            foreach (string v in topo.SortedVertices)
                Console.WriteLine("{0}: {1}", index++, v);
        }
    }
}
