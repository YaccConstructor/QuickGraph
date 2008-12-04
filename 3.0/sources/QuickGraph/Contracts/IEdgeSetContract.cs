#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IEdgeSet<,>))]
    sealed class IEdgeSetContract<TVertex, TEdge> 
        : IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [Pure]
            get 
            {
                IEdgeSet<TVertex, TEdge> ithis = this;
                Contract.Ensures(Contract.Result<bool>() == (ithis.EdgeCount == 0));

                return Contract.Result<bool>();
            }
        }

        int IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [Pure]
            get
            {
                IEdgeSet<TVertex, TEdge> ithis = this;
                Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.Edges));

                return Contract.Result<int>();
            }
        }

        IEnumerable<TEdge> IEdgeSet<TVertex, TEdge>.Edges
        {
            [Pure]
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);

                return Contract.Result<IEnumerable<TEdge>>();            
            }
        }

        [Pure]
        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            IEdgeSet<TVertex, TEdge> ithis = this;
            Contract.Requires(edge != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.Exists(ithis.Edges, e => e.Equals(edge)));

            return Contract.Result<bool>();
        }
    }
}
#endif
