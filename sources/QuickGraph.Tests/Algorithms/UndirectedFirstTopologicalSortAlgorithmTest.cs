using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms
{
    [TypeFixture(typeof(IUndirectedGraph<string, Edge<string>>))]
    [TypeFactory(typeof(UndirectedGraphFactory))]
    public class UndirectedFirstTopologicalSortAlgorithmTest
    {
        [Test]
        public void Compute(IUndirectedGraph<string, Edge<string>> g)
        {
            UndirectedFirstTopologicalSortAlgorithm<string, Edge<string>> topo =
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
