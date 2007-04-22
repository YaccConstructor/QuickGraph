using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms
{
    [TypeFixture(typeof(IUndirectedGraph<string, Edge<string>>))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class UndirectedTopologicalSortAlgorithmTest
    {
        [Test]
        public void Compute(IUndirectedGraph<string, Edge<string>> g)
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
