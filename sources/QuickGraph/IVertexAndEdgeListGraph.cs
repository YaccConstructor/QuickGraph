using System;

namespace QuickGraph
{
    public interface IVertexAndEdgeListGraph<Vertex,Edge> :
        IVertexListGraph<Vertex,Edge>,
        IEdgeListGraph<Vertex,Edge>,
        IVertexAndEdgeSet<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {}
}
