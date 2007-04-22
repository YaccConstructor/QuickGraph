using System;

namespace QuickGraph
{
    public interface IMutableIncidenceGraph<Vertex,Edge> :
        IMutableGraph<Vertex,Edge>,
        IIncidenceGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        int RemoveOutEdgeIf(
            Vertex v,
            IEdgePredicate<Vertex, Edge> predicate);
        void ClearOutEdges(Vertex v);
    }
}
