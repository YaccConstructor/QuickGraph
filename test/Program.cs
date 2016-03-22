using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.MinimumSpanningTree;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new UndirectedGraph<int, TaggedEdge<int, int>>();
            //var r = new Random();
            //for (int i = 0; i < 20; i++)
            //{
            //    graph.AddVertex(i);
            //    //graph.AddEdge(new TaggedEdge<int, int>(r.Next(20), r.Next(20), r.Next(50)));
            //}
            //for (int i = 0; i < 40; i++)
            //{
            //    //graph.AddVertex(i);
            //    graph.AddEdge(new TaggedEdge<int, int>(r.Next(20), r.Next(20), r.Next(50)));
            //}
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddEdge(new TaggedEdge<int, int>(1, 2, 10));
            graph.AddEdge(new TaggedEdge<int, int>(1, 4, 5));
            graph.AddEdge(new TaggedEdge<int, int>(1, 3, 12));
            graph.AddEdge(new TaggedEdge<int, int>(2, 3, 7));
            graph.AddEdge(new TaggedEdge<int, int>(4, 3, 7));
            graph.AddEdge(new TaggedEdge<int, int>(3, 4, 8));

            var t = new KruskalMinimumSpanningTreeAlgorithm<int, TaggedEdge<int, int>>(graph, y=>y.Tag);
            t.Compute();
        }
    }
}
