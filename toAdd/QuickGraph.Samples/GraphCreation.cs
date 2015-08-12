using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;

namespace QuickGraph.Samples
{
    [TestClass]
    public partial class GraphCreation
    {
        [TestMethod]
        public void EdgeArrayToAdjacencyGraph()
        {
            var edges = new SEdge<int>[] { 
                new SEdge<int>(1, 2), 
                new SEdge<int>(0, 1) 
            };
            var graph = edges.ToAdjacencyGraph<int, SEdge<int>>();
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
            foreach (var v in g.TopologicalSort())
                Console.WriteLine(v);
        }
    }
}
