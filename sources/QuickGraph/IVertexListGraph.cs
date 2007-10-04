using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IVertexListGraph<Vertex, Edge> : 
        IIncidenceGraph<Vertex, Edge>,
        IVertexSet<Vertex>
        where Edge : IEdge<Vertex>
    {
    }
}
