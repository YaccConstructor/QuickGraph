using System;

namespace QuickGraph
{
    public interface IEdgeFactory<Vertex, Edge> where Edge : IEdge<Vertex>
    {
        Edge CreateEdge(Vertex source, Vertex target);
    }
}
