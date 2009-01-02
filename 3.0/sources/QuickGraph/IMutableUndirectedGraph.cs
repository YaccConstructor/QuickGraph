using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IMutableUndirectedGraphContract<,>))]
#endif
    public interface IMutableUndirectedGraph<TVertex,TEdge> 
        : IMutableEdgeListGraph<TVertex,TEdge>
        , IMutableVertexSet<TVertex>
        , IUndirectedGraph<TVertex,TEdge>
        , IMutableVertexAndEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        int RemoveAdjacentEdgeIf(TVertex vertex, EdgePredicate<TVertex, TEdge> predicate);
        void ClearAdjacentEdges(TVertex vertex);
    }
}
