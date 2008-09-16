using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIndexedVertexListGraph<TVertex, TEdge> :
        IVertexListGraph<TVertex, TEdge>,
        IIndexedImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
