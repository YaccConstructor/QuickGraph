using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Regression
{
    [TestClass]
    public class ShortestPathBellmanFordTest
    {
        [TestMethod]
        public void Repro12901()
        {
            var graph = new BidirectionalGraph<int, Edge<int>>();
            int vertex = 1;
            graph.AddVerticesAndEdge(new Edge<int>(vertex, vertex));
            var pathFinder = AlgorithmExtensions.ShortestPathsBellmanFord<int, Edge<int>>(graph, edge => -1.0, vertex);
            IEnumerable<Edge<int>> path;
            pathFinder(vertex, out path);
        }
    }
}
