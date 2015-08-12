using System;

namespace QuickGraph
{
    public static class GraphConsoleSerializer
    {
        public static void DisplayGraph<Vertex, Edge>(IEdgeListGraph<Vertex, Edge> g)
            where Edge : IEdge<Vertex>
        {
            TestConsole.WriteLine("{0} vertices, {1} edges", g.VertexCount, g.EdgeCount);
            foreach(var v in g.Vertices)
                TestConsole.WriteLine("\t{0}", v);
            foreach (var edge in g.Edges)
                TestConsole.WriteLine("\t{0}", edge);
        }

        public static void DisplayGraph<TVertex, TEdge>(IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            TestConsole.WriteLine("{0} vertices", g.VertexCount);
            foreach(var v in g.Vertices)
                foreach (var edge in g.OutEdges(v))
                    TestConsole.WriteLine("\t{0}", edge);
        }
    }
}
