using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IEdgeListGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IsEdgesEmpty { get;}
        int EdgeCount { get;}
        IEnumerable<TEdge> Edges { get;}
        bool ContainsEdge(TEdge edge);
    }
}
