using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIncidenceGraph<Vertex,Edge> : IImplicitGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool ContainsEdge(Vertex source, Vertex target);
    }
}
