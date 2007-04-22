using System;

namespace QuickGraph
{
    public interface IMutableVertexAndEdgeListGraph<Vertex,Edge> :
        IMutableVertexListGraph<Vertex,Edge>,
        IMutableEdgeListGraph<Vertex,Edge>,
        IVertexAndEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {}
}
