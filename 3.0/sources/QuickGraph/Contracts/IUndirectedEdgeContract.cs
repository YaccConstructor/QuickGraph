using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IUndirectedEdge<>))]
    class IUndirectedEdgeContract<TVertex>
        : IEdgeContract<TVertex>
        , IUndirectedEdge<TVertex>
    {
        [ContractInvariantMethod]
        protected void IUndirectedEdgeInvariant()
        {
            IUndirectedEdge<TVertex> ithis = this;
            Contract.Invariant(Comparer<TVertex>.Default.Compare(ithis.Source, ithis.Target) <= 0);
        }
    }
}
