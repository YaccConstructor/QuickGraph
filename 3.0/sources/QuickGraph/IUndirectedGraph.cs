using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    [ContractClass(typeof(IUndirectedGraphContract<,>))]
    public interface IUndirectedGraph<TVertex,TEdge> 
        : IEdgeListGraph<TVertex,TEdge>
        , IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        [Pure]
        IEnumerable<TEdge> AdjacentEdges(TVertex v);

        [Pure]
        int AdjacentDegree(TVertex v);

        [Pure]
        bool IsAdjacentEdgesEmpty(TVertex v);

        [Pure]
        TEdge AdjacentEdge(TVertex v, int index);

        [Pure]
        bool ContainsEdge(TVertex source, TVertex target);
    }
}
