using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Unit;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;

namespace QuickGraph.Tests.Algorithms.ShortestPath
{
    [TestFixture, CurrentFixture]
    public class BellmanFordShortestPathTest
    {
        [Test]
        public void Sample()
        {
            var testGraph = new AdjacencyGraph<int, Edge<int>>();
            testGraph.AddVerticesAndEdge(new Edge<int>(1, 2));
            testGraph.AddVerticesAndEdge(new Edge<int>(1, 3));
            testGraph.AddVerticesAndEdge(new Edge<int>(3, 4));
            testGraph.AddVerticesAndEdge(new Edge<int>(1, 4));
            var testPath = testGraph.ShortestPathsBellmanFord<int, Edge<int>>(e => 1.0, 1);
            foreach(var i in testGraph.Vertices)
            {
                IEnumerable<Edge<int>> es;
                if (testPath(i, out es))
                    Console.WriteLine("{0}: {1}", i, es.Count());
            }
        }
    }
}
