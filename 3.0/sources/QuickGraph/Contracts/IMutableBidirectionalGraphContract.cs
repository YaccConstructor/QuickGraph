using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IMutableBidirectionalGraph<,>))]
    sealed class IMutableBidirectionalGraphContract<TVertex, TEdge>
        : IBidirectionalGraphContract<TVertex, TEdge>
        , IMutableBidirectionalGraph<TVertex, TEdge>
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

            return default(int);
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

        #region IMutableVertexSet<TVertex> Members

        event VertexAction<TVertex> IMutableVertexSet<TVertex>.VertexAdded
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

        event VertexAction<TVertex> IMutableVertexSet<TVertex>.VertexRemoved
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

        event EdgeAction<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeAdded
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

        event EdgeAction<TVertex, TEdge> IMutableEdgeListGraph<TVertex, TEdge>.EdgeRemoved
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        int IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(EdgePredicate<TVertex, TEdge> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
