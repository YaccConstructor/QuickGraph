using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class FilteredIncidenceGraph<TVertex, TEdge, TGraph> 
        : FilteredImplicitGraph<TVertex,TEdge,TGraph>
        , IIncidenceGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IIncidenceGraph<TVertex,TEdge>
    {
        public FilteredIncidenceGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex,TEdge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        {}

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            if (!this.VertexPredicate(source))
                return false;
            if (!this.VertexPredicate(target))
                return false;

            foreach (var edge in this.BaseGraph.OutEdges(source))
                if (edge.Target.Equals(target) && this.EdgePredicate(edge))
                    return true;
            return false;
        }

        public bool TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge)
        {
            IEnumerable<TEdge> unfilteredEdges;
            if (this.VertexPredicate(source) &&
                this.VertexPredicate(target) &&
                this.BaseGraph.TryGetEdges(source, target, out unfilteredEdges))
            {
                foreach (var ufe in unfilteredEdges)
                    if (this.EdgePredicate(ufe))
                    {
                        edge = ufe;
                        return true;
                    }
            }
            edge = default(TEdge);
            return false;
        }

        public bool TryGetEdges(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> edges)
        {
            edges = null;
            if (!this.VertexPredicate(source))
                return false;
            if (!this.VertexPredicate(target))
                return false;

            IEnumerable<TEdge> unfilteredEdges;
            if (this.BaseGraph.TryGetEdges(source, target, out unfilteredEdges))
            {
                List<TEdge> filtered = new List<TEdge>();
                foreach (var edge in unfilteredEdges)
                    if (this.EdgePredicate(edge))
                        filtered.Add(edge);
                edges = filtered;
                return true;
            }

            return false;
        }
    }
}
