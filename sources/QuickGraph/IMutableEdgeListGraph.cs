using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableEdgeListGraph<TVertex, TEdge> : 
        IMutableGraph<TVertex, TEdge>,
        IEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool AddEdge(TEdge edge);
        event EdgeEventHandler<TVertex, TEdge> EdgeAdded;

        bool RemoveEdge(TEdge edge);
        event EdgeEventHandler<TVertex, TEdge> EdgeRemoved;

        int RemoveEdgeIf(EdgePredicate<TVertex,TEdge> predicate);
    }
}
