#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(ICloneableEdge<>))]
    sealed class ICloneableEdgeContract<TVertex>
        : ICloneableEdge<TVertex>
    {
        ICloneableEdge<TVertex> ICloneableEdge<TVertex>.Clone(TVertex source, TVertex target)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Ensures(Contract.Result<ICloneableEdge<TVertex>>() != null);
            Contract.Ensures(Contract.Result<ICloneableEdge<TVertex>>().Source.Equals(source));
            Contract.Ensures(Contract.Result<ICloneableEdge<TVertex>>().Target.Equals(target));

            return default(ICloneableEdge<TVertex>);
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
#endif