using System;

namespace QuickGraph
{
    public static class GraphConsoleSerializer
    {
        public static void DisplayGraph<Vertex, Edge>(IVertexListGraph<Vertex, Edge> g)
            where Edge : IEdge<Vertex>
        {
            Console.WriteLine("{0} vertices", g.VertexCount);
            foreach(Vertex v in g.Vertices)
                foreach (Edge edge in g.OutEdges(v))
                    Console.WriteLine("\t{0}", edge);
        }
    }
}
