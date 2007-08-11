using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIncidenceGraph<Vertex,Edge> : IImplicitGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool ContainsEdge(Vertex source, Vertex target);
        bool TryGetEdges(
            Vertex source,
            Vertex target,
            out IEnumerable<Edge> edges);
        bool TryGetEdge(
            Vertex source,
            Vertex target,
            out Edge edge);
    }
}
