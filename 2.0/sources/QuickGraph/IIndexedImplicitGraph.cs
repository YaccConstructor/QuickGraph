using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IIndexedImplicitGraph<TVertex,TEdge> : IImplicitGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        IIndexable<TVertex> Vertices { get;}
    }
}
