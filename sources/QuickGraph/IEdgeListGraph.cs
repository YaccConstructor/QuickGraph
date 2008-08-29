using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IsEdgesEmpty { get; }
        int EdgeCount { get; }
        IEnumerable<TEdge> Edges { get; }
        bool ContainsEdge(TEdge edge);
    }

    public interface IEdgeListGraph<TVertex, TEdge> : 
        IGraph<TVertex, TEdge>,
        IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {}
}
