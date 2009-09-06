using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IBidirectionalImplicitGraph<,>))]
    class IBidirectionalImplicitGraphContract<TVertex, TEdge>
        : IBidirectionalImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        #region IBidirectionalImplicitGraph<TVertex,TEdge> Members
        [Pure]
        bool IBidirectionalImplicitGraph<TVertex, TEdge>.IsInEdgesEmpty(TVertex v)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<bool>() == (ithis.InDegree(v) == 0));

            return default(bool);
        }

        [Pure]
        int IBidirectionalImplicitGraph<TVertex, TEdge>.InDegree(TVertex v)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.InEdges(v)));

            return default(int);
        }

        [Pure]
        IEnumerable<TEdge> IBidirectionalImplicitGraph<TVertex, TEdge>.InEdges(TVertex v)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);
            Contract.Ensures(Contract.ForAll(
                Contract.Result<IEnumerable<TEdge>>(), 
                edge => edge != null && edge.Target.Equals(v)
                )
            );

            return default(IEnumerable<TEdge>);
        }

        [Pure]
        bool IBidirectionalImplicitGraph<TVertex, TEdge>.TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<bool>() == ithis.ContainsVertex(v));
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out edges) != null);
            Contract.Ensures(!Contract.Result<bool>() || 
                Contract.ForAll(
                Contract.ValueAtReturn<IEnumerable<TEdge>>(out edges),
                edge => edge != null && edge.Target.Equals(v)
                )
            );

            edges = null;
            return default(bool);
        }

        [Pure]
        TEdge IBidirectionalImplicitGraph<TVertex, TEdge>.InEdge(TVertex v, int index)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<TEdge>().Equals(Enumerable.ElementAt(ithis.InEdges(v), index)));

            return default(TEdge);
        }

        [Pure]
        int IBidirectionalImplicitGraph<TVertex, TEdge>.Degree(TVertex v)
        {
            IBidirectionalImplicitGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<int>() == ithis.InDegree(v) + ithis.OutDegree(v));

            return default(int);
        }

        #endregion

        #region IImplicitGraph<TVertex,TEdge> Members

        [Pure] // InterfacePureBug
        bool IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        int IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        IEnumerable<TEdge> IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        bool IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        TEdge IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
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

        #region IImplicitVertexSet<TVertex> Members

        bool IImplicitVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
