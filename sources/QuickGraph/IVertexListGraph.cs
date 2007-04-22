using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IVertexListGraph<Vertex, Edge> : 
        IIncidenceGraph<Vertex, Edge>,
        IVertexSet<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
    }
}
