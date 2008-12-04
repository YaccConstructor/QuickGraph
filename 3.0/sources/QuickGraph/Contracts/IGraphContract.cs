using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
#if CONTRACTS_FULL
    [ContractClassFor(typeof(IGraph<,>))]
    sealed class IGraphContract<TVertex, TEdge>
        : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IGraph<TVertex, TEdge>.IsDirected
        {
            [Pure]
            get { return Contract.Result<bool>(); }
        }

        bool IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [Pure]
            get { return Contract.Result<bool>(); }
        }
    }
#endif
}
