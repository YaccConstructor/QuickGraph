using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public static class TraversalHelper
    {
        public static Vertex GetFirstVertex<Vertex,Edge>(IVertexListGraph<Vertex, Edge> g)
            where Edge : IEdge<Vertex>
        {
            foreach (Vertex v in g.Vertices)
                return v;
            return default(Vertex);
        }

        public static Vertex GetFirstVertex<Vertex, Edge>(IUndirectedGraph<Vertex, Edge> g)
            where Edge : IEdge<Vertex>
        {
            foreach (Vertex v in g.Vertices)
                return v;
            return default(Vertex);
        }
    }
}
