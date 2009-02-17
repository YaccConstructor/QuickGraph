using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IEdgeListGraph<,>))]
#endif
    public interface IEdgeListGraph<TVertex, TEdge> : 
        IGraph<TVertex, TEdge>,
        IEdgeSet<TVertex, TEdge>,
        IVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {}
}
