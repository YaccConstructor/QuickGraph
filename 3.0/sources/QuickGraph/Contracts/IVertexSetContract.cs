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
            CodeContract.Invariant((this.VertexCount == 0) == this.IsVerticesEmpty);
            CodeContract.Invariant(this.VertexCount >= 0);
        }

        public bool IsVerticesEmpty
        {
            get 
            {
                return CodeContract.Result<bool>();
            }
        }

        public int VertexCount
        {
            get
            {
                return CodeContract.Result<int>();
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                CodeContract.Ensures(CodeContract.Result<IEnumerable<TVertex>>() != null);

                return CodeContract.Result<IEnumerable<TVertex>>();
            }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            CodeContract.Requires(vertex != null);
            CodeContract.Ensures(
                CodeContract.Result<bool>()
                == CodeContract.Exists(this.Vertices, v => v.Equals(vertex))
                );

            return CodeContract.Result<bool>();
        }
    }
#endif
}
