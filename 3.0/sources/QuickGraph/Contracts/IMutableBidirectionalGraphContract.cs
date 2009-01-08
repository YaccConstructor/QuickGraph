#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IMutableBidirectionalGraph<,>))]
    sealed class IMutableBidirectionalGraphContract<TVertex, TEdge>
        : IMutableBidirectionalGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        int IMutableBidirectionalGraph<TVertex, TEdge>.RemoveInEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            IMutableBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(predicate != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.ForAll(ithis.InEdges(v), e => predicate(e)));
            Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Enumerable.Count(ithis.InEdges(v), e => predicate(e))));
            Contract.Ensures(ithis.InDegree(v) == Contract.OldValue(ithis.InDegree(v)) - Contract.Result<int>());

            return Contract.Result<int>();
        }

        void IMutableBidirectionalGraph<TVertex, TEdge>.ClearInEdges(TVertex v)
        {
            IMutableBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) - Contract.OldValue(ithis.InDegree(v)));
            Contract.Ensures(ithis.InDegree(v) == 0);
        }

        void IMutableBidirectionalGraph<TVertex, TEdge>.ClearEdges(TVertex v)
        {
            IMutableBidirectionalGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(!ithis.ContainsVertex(v));
        }


        #region IMutableVertexAndEdgeListGraph<TVertex,TEdge> Members

        bool IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        int IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMutableIncidenceGraph<TVertex,TEdge> Members

        int IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> predicate)
        {
            throw new NotImplementedException();
        }

        void IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            throw new NotImplementedException();
        }

        void IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            throw new NotImplementedException();
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

        #region IMutableVertexSet<TVertex> Members

        event VertexEventHandler<TVertex> IMutableVertexSet<TVertex>.VertexAdded
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        bool IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            throw new NotImplementedException();
        }

        int IMutableVertexSet<TVertex>.AddVertexRange(IEnumerable<TVertex> vertices)
        {
            throw new NotImplementedException();
        }

        event VertexEventHandler<TVertex> IMutableVertexSet<TVertex>.VertexRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        bool IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            throw new NotImplementedException();
        }

        int IMutableVertexSet<TVertex>.RemoveVertexIf(VertexPredicate<TVertex> pred)
        {
            throw new NotImplementedException();
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
        bool IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMutableEdgeListGraph<TVertex,TEdge> Members

        bool IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        event EdgeEventHandler<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeAdded
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        int IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        bool IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        event EdgeEventHandler<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        int IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
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

        #region IBidirectionalGraph<TVertex,TEdge> Members

        [Pure] // InterfacePureBug
        bool IBidirectionalGraph<TVertex, TEdge>.IsInEdgesEmpty(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        int IBidirectionalGraph<TVertex, TEdge>.InDegree(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        IEnumerable<TEdge> IBidirectionalGraph<TVertex, TEdge>.InEdges(TVertex v)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        bool IBidirectionalGraph<TVertex, TEdge>.TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        TEdge IBidirectionalGraph<TVertex, TEdge>.InEdge(TVertex v, int index)
        {
            throw new NotImplementedException();
        }

        [Pure] // InterfacePureBug
        int IBidirectionalGraph<TVertex, TEdge>.Degree(TVertex v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
#endif