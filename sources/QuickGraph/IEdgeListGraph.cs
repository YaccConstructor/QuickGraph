using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IEdgeListGraph<TVertex, TEdge> : 
        IGraph<TVertex, TEdge>,
        IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {}
}
