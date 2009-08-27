using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests
{
    using QuickGraph;
    using QuickGraph.Algorithms;

    [TestClass]
    public class WikiSamples
    {
        [TestMethod]
        public void EasyCreation()
        {
            var edges = new SEdge<int>[] { new SEdge<int>(1,2), new SEdge<int>(0,1) };
            var graph = edges.ToAdjacencyGraph<int, SEdge<int>>();
        }

        [TestMethod]
        public void ShortestPath()
        {
            IVertexAndEdgeListGraph<int, Edge<int>> cities = new AdjacencyGraph<int, Edge<int>>(); // a graph of cities
            Func<Edge<int>, double> cityDistances = e => e.Target + e.Source ; // a delegate that gives the distance between cities

            int sourceCity = 0; // starting city
            int targetCity = 1; // ending city

            // vis can create all the shortest path in the graph
            // and returns a delegate vertex -> path
            var tryGetPath = cities.ShortestPathsDijkstra(cityDistances, sourceCity);
            IEnumerable<Edge<int>> path;
            if (tryGetPath(targetCity, out path))
                foreach (var e in path)
                    Console.WriteLine(e);
        }

        [TestMethod]
        public void DelegateGraph()
        {
            // a simple adjacency graph representation
            int[][] graph = new int[5][];
            graph[0] = new int[] { 1 };
            graph[1] = new int[] { 2, 3 };
            graph[2] = new int[] { 3, 4 };
            graph[3] = new int[] { 4 };
            graph[4] = new int[] { };

            // interoping with quickgraph
            var g = GraphExtensions.ToDelegateVertexAndEdgeListGraph(
                Enumerable.Range(0, graph.Length),
                v => Array.ConvertAll(graph[v], w => new SEquatableEdge<int>(v, w))
                );

            // it's ready to use!
            foreach(var v in g.TopologicalSort())
                Console.WriteLine(v);
        }
    }
}
