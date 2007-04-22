using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIndexedImplicitGraph<Vertex,Edge> : IImplicitGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        IIndexedEnumerable<Vertex> Vertices { get;}
    }
}
