#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IEdge<>))]
    sealed class IEdgeContract<TVertex>
        : IEdge<TVertex>
    {
        [ContractInvariantMethod]
        public void ObjectInvariant()
        {
            IEdge<TVertex> me = this;
            Contract.Invariant(me.Source != null);
            Contract.Invariant(me.Target != null);
        }

        TVertex IEdge<TVertex>.Source
        {
            get
            {
                Contract.Ensures(Contract.Result<TVertex>() != null);
                return default(TVertex);
            }
        }

        TVertex IEdge<TVertex>.Target
        {
            get
            {
                Contract.Ensures(Contract.Result<TVertex>() != null);
                return default(TVertex);
            }
        }
    }
}
#endif
