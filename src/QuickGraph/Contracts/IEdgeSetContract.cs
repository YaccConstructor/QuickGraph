using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Contracts
{
    [ContractClassFor(typeof(IEdgeSet<,>))]
    abstract class IEdgeSetContract<TVertex, TEdge> 
        : IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            get 
            {
                IEdgeSet<TVertex, TEdge> ithis = this;
                Contract.Ensures(Contract.Result<bool>() == (ithis.EdgeCount == 0));

                return default(bool);
            }
        }

        int IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            get
            {
                IEdgeSet<TVertex, TEdge> ithis = this;
                Contract.Ensures(Contract.Result<int>() == Enumerable.Count(ithis.Edges));

                return default(int);
            }
        }

        IEnumerable<TEdge> IEdgeSet<TVertex, TEdge>.Edges
        {
            get 
            {
                Contract.Ensures(Contract.Result<IEnumerable<TEdge>>() != null);
                Contract.Ensures(Enumerable.All<TEdge>(Contract.Result<IEnumerable<TEdge>>(), e => e != null));

                return default(IEnumerable<TEdge>);            
            }
        }

        [Pure]
        bool IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            IEdgeSet<TVertex, TEdge> ithis = this;
            Contract.Requires(edge != null);
            Contract.Ensures(Contract.Result<bool>() == Contract.Exists(ithis.Edges, e => e.Equals(edge)));

            return default(bool);
        }
    }
}
