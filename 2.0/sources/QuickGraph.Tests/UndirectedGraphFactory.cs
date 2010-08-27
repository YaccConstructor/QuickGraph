using System;
using QuickGraph.Unit;

namespace QuickGraph
{
    public sealed class UndirectedGraphFactory
    {
        private static UndirectedGraph<String, Edge<String>> CreateGraph()
        {
            return new UndirectedGraph<String, Edge<String>>(false);
        }

        [Factory]
        public UndirectedGraph<string, Edge<string>> Empty()
        {
            return CreateGraph();
        }

        [Factory]
        public UndirectedGraph<string, Edge<string>> NoEdges()
        {
            UndirectedGraph<string, Edge<string>> g = CreateGraph();
            g.AddVertex("x");
            g.AddVertex("y");
            g.AddVertex("z");
            return g;
        }

        [Factory]
        public UndirectedGraph<string, Edge<string>> Loop()
        {
            UndirectedGraph<string, Edge<string>> g = CreateGraph();
            g.AddVertex("x");
            g.AddVertex("y");
            g.AddVertex("z");
            g.AddEdge(new Edge<string>("x", "y"));
            g.AddEdge(new Edge<string>("y", "z"));
            g.AddEdge(new Edge<string>("z", "x"));
            return g;
        }

        [Factory]
        public UndirectedGraph<string, Edge<string>> LoopDouble()
        {
            UndirectedGraph<string, Edge<string>> g = CreateGraph();
            g.AddVertex("x");
            g.AddVertex("y");
            g.AddVertex("z");

            g.AddEdge(new Edge<string>("x", "y"));
            g.AddEdge(new Edge<string>("y", "z"));
            g.AddEdge(new Edge<string>("z", "x"));

            g.AddEdge(new Edge<string>("x", "y"));
            g.AddEdge(new Edge<string>("y", "z"));
            g.AddEdge(new Edge<string>("z", "x"));
            return g;
        }

        [Factory]
        public UndirectedGraph<string, Edge<string>> Simple()
        {
            UndirectedGraph<string, Edge<string>> g = CreateGraph();
            g.AddVertex("x");
            g.AddVertex("y");
            g.AddVertex("z");
            g.AddEdge(new Edge<string>("x", "y"));
            g.AddEdge(new Edge<string>("y", "z"));
            return g;
        }
    }
}
