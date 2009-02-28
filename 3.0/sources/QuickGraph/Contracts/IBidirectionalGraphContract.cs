using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IBidirectionalGraph<,>))]
    class IBidirectionalGraphContract<TVertex, TEdge>
        : IBidirectionalGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        #region IBidirectionalGraph<TVertex,TEdge> Members
        [Pure]
        bool IBidirectionalGraph<TVertex, TEdge>.IsInEdgesEmpty(TVertex v)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<bool>() == (ithis.InDegree(v) == 0));

            return default(bool);
        }

        [Pure]
        int IBidirectionalGraph<TVertex, TEdge>.InDegree(TVertex v)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.InEdges(v)));

            return default(int);
        }

        [Pure]
        IEnumerable<TEdge> IBidirectionalGraph<TVertex, TEdge>.InEdges(TVertex v)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);
            Contract.Ensures(Contract.ForAll(
                Contract.Result<IEnumerable<TEdge>>(), 
                edge => edge != null && ithis.ContainsEdge(edge) && edge.Target.Equals(v)
                )
            );

            return default(IEnumerable<TEdge>);
        }

        [Pure]
        bool IBidirectionalGraph<TVertex, TEdge>.TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<bool>() == ithis.ContainsVertex(v));
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out edges) != null);
            Contract.Ensures(!Contract.Result<bool>() || 
                Contract.ForAll(
                Contract.ValueAtReturn<IEnumerable<TEdge>>(out edges),
                edge => edge != null && ithis.ContainsEdge(edge) && edge.Target.Equals(v)
                )
            );

            edges = null;
            return default(bool);
        }

        [Pure]
        TEdge IBidirectionalGraph<TVertex, TEdge>.InEdge(TVertex v, int index)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<TEdge>().Equals(Enumerable.ElementAt(ithis.InEdges(v), index)));

            return default(TEdge);
        }

        [Pure]
        int IBidirectionalGraph<TVertex, TEdge>.Degree(TVertex v)
        {
            IBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<int>() == ithis.InDegree(v) + ithis.OutDegree(v));

            return default(int);
        }

        #endregion

        #region IIncidenceGraph<TVertex,TEdge> Members

        bool IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            throw new NotImplementedException();
        }

        bool IIncidenceGraph<TVertex, TEdge>.TryGetEdges(TVertex source, TVertex target, out IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        bool IIncidenceGraph<TVertex, TEdge>.TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            throw new NotImplementedException();
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

        [Pure] // InterfacePureBug
        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
