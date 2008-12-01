using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
#if CONTRACTS_FULL
    [ContractClassFor(typeof(IEdge<>))]
    sealed class IVertexSetContract<TVertex>
        : IVertexSet<TVertex>
    {
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant((this.VertexCount == 0) == this.IsVerticesEmpty);
            Contract.Invariant(this.VertexCount >= 0);
        }

        public bool IsVerticesEmpty
        {
            get 
            {
                return Contract.Result<bool>();
            }
        }

        public int VertexCount
        {
            get
            {
                return Contract.Result<int>();
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TVertex>>() != null);

                return Contract.Result<IEnumerable<TVertex>>();
            }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            Contract.Requires(vertex != null);
            Contract.Ensures(
                Contract.Result<bool>()
                == Contract.Exists(this.Vertices, v => v.Equals(vertex))
                );

            return Contract.Result<bool>();
        }
    }
#endif
}
