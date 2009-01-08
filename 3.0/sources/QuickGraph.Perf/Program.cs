using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Perf
{
    class Program
    {
        static void Main(string[] args)
        {
            new DijkstraShortestPathAlgorithmTest().DijkstraAll();
        }
    }
}
