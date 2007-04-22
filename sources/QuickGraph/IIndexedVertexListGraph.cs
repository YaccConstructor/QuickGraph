using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIndexedVertexListGraph<Vertex, Edge> :
        IVertexListGraph<Vertex, Edge>,
        IIndexedImplicitGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
    }
}
