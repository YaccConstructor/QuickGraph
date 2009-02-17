#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IGraph<,>))]
    class IGraphContract<TVertex, TEdge>
        : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IGraph<TVertex, TEdge>.IsDirected
        {
            get { return default(bool); }
        }

        bool IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            get { return default(bool); }
        }
    }
}
#endif
