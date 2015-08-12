using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IVertexSet<>))]
    abstract class IVertexSetContract<TVertex>
        : IVertexSet<TVertex>
    {
        bool IVertexSet<TVertex>.IsVerticesEmpty
        {
            get 
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<bool>() == (ithis.VertexCount == 0));

                return default(bool);
            }
        }

        int IVertexSet<TVertex>.VertexCount
        {
            get
            {
                IVertexSet<TVertex> ithis = this;
                Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.Vertices));

                return default(int);
            }
        }

        IEnumerable<TVertex> IVertexSet<TVertex>.Vertices
        {
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TVertex>>() != null);

                return default(IEnumerable<TVertex>);
            }
        }

        #region IImplicitVertexSet<TVertex> Members

        public bool ContainsVertex(TVertex vertex) {
          throw new NotImplementedException();
        }

        #endregion
    }
}
