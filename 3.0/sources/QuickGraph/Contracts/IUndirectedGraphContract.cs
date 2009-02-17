#if CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IUndirectedGraph<,>))]
    class IUndirectedGraphContract<TVertex, TEdge>
        : IEdgeListGraphContract<TVertex,TEdge>
        , IUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        #region IUndirectedGraph<TVertex,TEdge> Members
        [Pure]
        IEnumerable<TEdge> IUndirectedGraph<TVertex, TEdge>.AdjacentEdges(TVertex v)
        {
            IUndirectedGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);
            Contract.Ensures(
                Contract.ForAll(
                    Contract.Result<IEnumerable<TEdge>>(),
                    edge => edge != null && ithis.ContainsEdge(edge) && (edge.Source.Equals(v) || edge.Target.Equals(v))
                    )
                );

            return default(IEnumerable<TEdge>);
        }

        [Pure]
        int IUndirectedGraph<TVertex, TEdge>.AdjacentDegree(TVertex v)
        {
            IUndirectedGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.AdjacentEdges(v)));

            return default(int);
        }

        [Pure]
        bool IUndirectedGraph<TVertex, TEdge>.IsAdjacentEdgesEmpty(TVertex v)
        {
            IUndirectedGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<bool>() == (ithis.AdjacentDegree(v) == 0));

            return default(bool);
        }

        [Pure]
        TEdge IUndirectedGraph<TVertex, TEdge>.AdjacentEdge(TVertex v, int index)
        {
            IUndirectedGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(v != null);
            Contract.Requires(ithis.ContainsVertex(v));
            Contract.Ensures(Contract.Result<TEdge>() != null);
            Contract.Ensures(
                Contract.Result<TEdge>().Source.Equals(v) 
                || Contract.Result<TEdge>().Target.Equals(v));

            return default(TEdge);
        }

        [Pure]
        bool IUndirectedGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            IUndirectedGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Ensures(Contract.Result<bool>() == Enumerable.Any(ithis.AdjacentEdges(source), e => e.Target.Equals(target) || e.Source.Equals(target)));

            return default(bool);
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
    }
}
#endif