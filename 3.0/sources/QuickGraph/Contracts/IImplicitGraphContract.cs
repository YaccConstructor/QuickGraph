#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IImplicitGraph<,>))]
    sealed class IImplicitGraphContract<TVertex, TEdge> 
        : IImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        [Pure]
        bool IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            IImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Ensures(Contract.Result<bool>() == (ithis.OutDegree(v) == 0));

            return Contract.Result<bool>();
        }

        [Pure]
        int IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            IImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Ensures(Contract.Result<int>() == Enumerable.Count<TEdge>(ithis.OutEdges(v)));

            return Contract.Result<int>();
        }

        [Pure]
        IEnumerable<TEdge> IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            IImplicitGraph<TVertex, TEdge> ithis = this;
            IEnumerable<TEdge> edges;
            Contract.Requires(v != null);
            Contract.Requires(ithis.TryGetOutEdges(v, out edges));
            Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);
            Contract.Ensures(Contract.ForAll(ithis.OutEdges(v),e => e.Source.Equals(v)));

            return Contract.Result<IEnumerable<TEdge>>();
        }

        [Pure]
        bool IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            IImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);

            Contract.ValueAtReturn(out edges);
            return Contract.Result<bool>();
        }

        TEdge IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            throw new NotImplementedException();
        }

        #region IGraph<TVertex,TEdge> Members
        bool IGraph<TVertex, TEdge>.IsDirected
        {
            get { throw new NotImplementedException(); }
        }

        bool IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}
#endif
