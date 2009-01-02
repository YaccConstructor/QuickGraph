#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IMutableEdgeListGraph<,>))]
    sealed class IMutableEdgeListGraphContract<TVertex, TEdge>
        : IMutableEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        #region IMutableEdgeListGraph<TVertex,TEdge> Members
        bool IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge e)
        {
            IMutableEdgeListGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(e != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(!ithis.ContainsEdge(e)));
            Contract.Ensures(ithis.ContainsEdge(e));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) + (Contract.Result<bool>() ? 1 : 0));

            return Contract.Result<bool>();
        }

        event EdgeEventHandler<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeAdded
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        int IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(IEnumerable<TEdge> edges)
        {
            IMutableEdgeListGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(edges != null);
            Contract.Requires(Contract.ForAll(edges, e => e != null));
            Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Enumerable.Count(edges, e => !ithis.ContainsEdge(e))));
            Contract.Ensures(Contract.ForAll(edges, e => ithis.ContainsEdge(e)));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) + Contract.Result<int>());

            return Contract.Result<int>();
        }

        bool IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge e)
        {
            IMutableEdgeListGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(e != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(ithis.ContainsEdge(e)));
            Contract.Ensures(!ithis.ContainsEdge(e));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) - (Contract.Result<bool>() ? 1 : 0));

            return Contract.Result<bool>();
        }

        event EdgeEventHandler<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        int IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
        {
            IMutableEdgeListGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(predicate != null);
            Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Enumerable.Count(ithis.Edges, e => predicate(e))));
            Contract.Ensures(Contract.ForAll(ithis.Edges, e => !predicate(e)));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) - Contract.Result<int>());

            return Contract.Result<int>();
        }

        #endregion

        #region IMutableGraph<TVertex,TEdge> Members
        void IMutableGraph<TVertex, TEdge>.Clear()
        {
            throw new NotImplementedException();
        }
        #endregion

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

        #region IEdgeSet<TVertex,TEdge> Members

        bool IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            get { throw new NotImplementedException(); }
        }

        int IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<TEdge> IEdgeSet<TVertex, TEdge>.Edges
        {
            get { throw new NotImplementedException(); }
        }

        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
#endif