using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(ITermEdge<>))]
    abstract class ITermEdgeContract<TVertex>
        : ITermEdge<TVertex>
    {
        [ContractInvariantMethod]
        void ITermEdgeInvariant()
        {
            ITermEdge<TVertex> ithis = this;
            Contract.Invariant(ithis.SourceTerminal >= 0);
            Contract.Invariant(ithis.TargetTerminal >= 0);
        }

        int ITermEdge<TVertex>.SourceTerminal
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return -1;
            }
        }

        int ITermEdge<TVertex>.TargetTerminal
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return -1;
            }
        }

        #region IEdge<TVertex> Members

        TVertex IEdge<TVertex>.Source
        {
            get { throw new NotImplementedException(); }
        }

        TVertex IEdge<TVertex>.Target
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
