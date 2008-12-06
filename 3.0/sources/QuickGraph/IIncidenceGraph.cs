using System;
using System.Collections.Generic;
using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IIncidenceGraphContract<,>))]
#endif
    public interface IIncidenceGraph<TVertex, TEdge> 
        : IImplicitGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool ContainsEdge(TVertex source, TVertex target);
        bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges);
        bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge);
    }
}
