using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IIncidenceGraph<,>))]
    abstract class IIncidenceGraphContract<TVertex, TEdge>
        : IIncidenceGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            IIncidenceGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(ithis.ContainsVertex(source));
            Contract.Requires(ithis.ContainsVertex(target));

            return default(bool);
        }

        bool IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source, 
            TVertex target, 
            out IEnumerable<TEdge> edges)
        {
            IIncidenceGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(ithis.ContainsVertex(source));
            Contract.Requires(ithis.ContainsVertex(target));

            edges = null;
            return default(bool);
        }

        bool IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source, 
            TVertex target, 
            out TEdge edge)
        {
            IIncidenceGraph<TVertex, TEdge> ithis = this;
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(ithis.ContainsVertex(source));
            Contract.Requires(ithis.ContainsVertex(target));

            edge = default(TEdge);
            return default(bool);
        }

        #region IImplicitGraph<TVertex,TEdge> Members

        public bool IsOutEdgesEmpty(TVertex v) {
          throw new NotImplementedException();
        }

        public int OutDegree(TVertex v) {
          throw new NotImplementedException();
        }

        public IEnumerable<TEdge> OutEdges(TVertex v) {
          throw new NotImplementedException();
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges) {
          throw new NotImplementedException();
        }

        public TEdge OutEdge(TVertex v, int index) {
          throw new NotImplementedException();
        }

        #endregion

        #region IGraph<TVertex,TEdge> Members

        public bool IsDirected {
          get { throw new NotImplementedException(); }
        }

        public bool AllowParallelEdges {
          get { throw new NotImplementedException(); }
        }

        #endregion

        #region IImplicitVertexSet<TVertex> Members

        public bool ContainsVertex(TVertex vertex) {
          throw new NotImplementedException();
        }

        #endregion
    }
}
