#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IMutableVertexSet<>))]
    sealed class IMutableVertexSetContract<TVertex>
        : IMutableVertexSet<TVertex>
    {
        #region IMutableVertexSet<TVertex> Members

        event VertexAction<TVertex> IMutableVertexSet<TVertex>.VertexAdded
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        bool IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            IMutableVertexSet<TVertex> ithis = this;
            Contract.Requires(v != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(!ithis.ContainsVertex(v)));
            Contract.Ensures(ithis.ContainsVertex(v));
            Contract.Ensures(ithis.VertexCount == Contract.OldValue(ithis.VertexCount) + (Contract.Result<bool>() ? 1 : 0));

            return default(bool);
        }

        int IMutableVertexSet<TVertex>.AddVertexRange(IEnumerable<TVertex> vertices)
        {
            IMutableVertexSet<TVertex> ithis = this;
            Contract.Requires(vertices != null);
            Contract.Requires(Contract.ForAll(vertices, v => v != null));
            Contract.Ensures(Contract.ForAll(vertices, v => ithis.ContainsVertex(v)));
            Contract.Ensures(ithis.VertexCount == Contract.OldValue(ithis.VertexCount) + Contract.Result<int>());

            return default(int);
        }

        event VertexAction<TVertex> IMutableVertexSet<TVertex>.VertexRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        bool IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            IMutableVertexSet<TVertex> ithis = this;
            Contract.Requires(v != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(ithis.ContainsVertex(v)));
            Contract.Ensures(!ithis.ContainsVertex(v));
            Contract.Ensures(ithis.VertexCount == Contract.OldValue(ithis.VertexCount) - (Contract.Result<bool>() ? 1 : 0));

            return default(bool);
        }

        int IMutableVertexSet<TVertex>.RemoveVertexIf(VertexPredicate<TVertex> pred)
        {
            IMutableVertexSet<TVertex> ithis = this;
            Contract.Requires(pred != null);
            Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Enumerable.Count(ithis.Vertices, v => pred(v))));
            Contract.Ensures(Contract.ForAll(ithis.Vertices, v => !pred(v)));
            Contract.Ensures(ithis.VertexCount == Contract.OldValue(ithis.VertexCount) - Contract.Result<int>());

            return default(int);
        }

        #endregion

        #region IVertexSet<TVertex> Members

        bool IVertexSet<TVertex>.IsVerticesEmpty
        {
            get { throw new NotImplementedException(); }
        }

        int IVertexSet<TVertex>.VertexCount
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<TVertex> IVertexSet<TVertex>.Vertices
        {
            get { throw new NotImplementedException(); }
        }

        [Pure] // InterfacePureBug
        bool IImplicitVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
#endif