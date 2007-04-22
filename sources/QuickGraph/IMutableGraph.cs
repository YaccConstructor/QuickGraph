using System;

namespace QuickGraph
{
    public interface IMutableGraph<Vertex,Edge> : IGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        void Clear();
    }
}
