using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
   [ContractClass(typeof(IMutableVertexListGraphContract<,>))]
#endif
    public interface IMutableVertexListGraph<TVertex, TEdge> : 
        IMutableIncidenceGraph<TVertex, TEdge>,
        IMutableVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {
    }
}
