#if CONTRACTS_FULL
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IIncidenceGraph<,>))]
    sealed class IIncidenceGraphContract<TVertex, TEdge>
        : IImplicitGraphContract<TVertex, TEdge>
        , IIncidenceGraph<TVertex, TEdge>
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
    }
}
#endif
