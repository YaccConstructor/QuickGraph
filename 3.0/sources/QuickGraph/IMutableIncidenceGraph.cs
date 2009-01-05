using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IMutableIncidenceGraphContract<TVertex,TEdge>))]
#endif
    public interface IMutableIncidenceGraph<TVertex,TEdge> :
        IMutableGraph<TVertex,TEdge>,
        IIncidenceGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        int RemoveOutEdgeIf(
            TVertex v,
            EdgePredicate<TVertex, TEdge> predicate);
        void ClearOutEdges(TVertex v);

        void TrimEdgeExcess();
    }
}
