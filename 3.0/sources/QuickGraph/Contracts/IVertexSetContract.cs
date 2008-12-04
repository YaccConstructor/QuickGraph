using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
#if CONTRACTS_FULL
    [ContractClassFor(typeof(IEdge<>))]
    sealed class IVertexSetContract<TVertex>
        : IVertexSet<TVertex>
    {
        bool IVertexSet<TVertex>.IsVerticesEmpty
        {
            [Pure]
            get 
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<bool>() == (ithis.VertexCount == 0));

                return Contract.Result<bool>();
            }
        }

        int IVertexSet<TVertex>.VertexCount
        {
            [Pure]
            get
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.Vertices));

                return Contract.Result<int>();
            }
        }

        IEnumerable<TVertex> IVertexSet<TVertex>.Vertices
        {
            [Pure]
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TVertex>>() != null);

                return Contract.Result<IEnumerable<TVertex>>();
            }
        }

        [Pure]
        bool IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            IVertexSet<TVertex> ithis = this;
            Contract.Requires(vertex != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.Exists(ithis.Vertices, v => v.Equals(vertex)));

            return Contract.Result<bool>();
        }
    }
#endif
}
