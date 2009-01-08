#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IMutableVertexAndEdgeSet<,>))]
    sealed class IMutableVertexAndEdgeSetContract<TVertex, TEdge>
        : IMutableVertexAndEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            IMutableVertexAndEdgeSet<TVertex, TEdge> ithis = this;
            Contract.Requires(edge != null);
            Contract.Ensures(ithis.ContainsEdge(edge));
            Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(ithis.ContainsEdge(edge)));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) + (Contract.Result<bool>() ? 1 : 0));

            return Contract.Result<bool>();
        }

        int IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(IEnumerable<TEdge> edges)
        {
            IMutableVertexAndEdgeSet<TVertex, TEdge> ithis = this;
            Contract.Requires(edges != null);
            Contract.Requires(Contract.ForAll(edges, edge => edge != null));
            Contract.Ensures(Contract.ForAll(edges, edge => ithis.ContainsEdge(edge)));
            Contract.Ensures(Contract.Result<int>() == Enumerable.Count(edges, edge => Contract.OldValue(!ithis.ContainsEdge(edge))));
            Contract.Ensures(ithis.EdgeCount == Contract.OldValue(ithis.EdgeCount) + Contract.Result<int>());

            return Contract.Result<int>();
        }

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

        [Pure] // InterfacePureBug
        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
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

        #region IMutableGraph<TVertex,TEdge> Members

        void IMutableGraph<TVertex, TEdge>.Clear()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
#endif