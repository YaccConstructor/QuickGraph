using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Tests.Algorithms;
using QuickGraph.Tests.Algorithms.MinimumSpanningTree;

namespace QuickGraph.Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            // new TarjanOfflineLeastCommonAncestorAlgorithmTest().TarjanOfflineLeastCommonAncestorAlgorithmAll();
            new DijkstraShortestPathAlgorithmTest().DijkstraAll();
            // new MinimumSpanningTreeTest().PrimKruskalMinimumSpanningTreeAll();
        }
    }
}
