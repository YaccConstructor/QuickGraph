using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredBidirectionalGraph<TVertex, TEdge, TGraph> :
        FilteredVertexListGraph<TVertex, TEdge, TGraph>,
        IBidirectionalGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        public FilteredBidirectionalGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsInEdgesEmpty(TVertex v)
        {
            Contract.Requires(v != null);

            return this.InDegree(v) == 0;
        }

        public int InDegree(TVertex v)
        {
            Contract.Requires(v != null);

            int count = 0;
            foreach (var edge in this.InEdges(v))
                if (TestEdge(edge))
                    count++;
            return count;
        }

        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            foreach (var edge in this.InEdges(v))
                if (TestEdge(edge))
                    yield return edge;
        }

        [Pure]
        public bool TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            Contract.Requires(v != null);

            if (this.ContainsVertex(v))
            {
                edges = this.InEdges(v);
                return true;
            }
            else
            {
                edges = null;
                return false;
            }
        }

        public int Degree(TVertex v)
        {
            return this.OutDegree(v) + this.InDegree(v);
        }

        public bool IsEdgesEmpty
        {
            get
            {
                foreach (var edge in this.BaseGraph.Edges)
                    if (TestEdge(edge))
                        return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get
            {
                int count = 0;
                foreach (var edge in this.BaseGraph.Edges)
                    if (TestEdge(edge))
                        count++;
                return count;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var edge in this.BaseGraph.Edges)
                    if (TestEdge(edge))
                        yield return edge;
            }
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            if (!TestEdge(edge))
                return false;
            return this.BaseGraph.ContainsEdge(edge);
        }

        [Pure]
        public TEdge InEdge(TVertex v, int index)
        {
            throw new NotSupportedException();
        }
    }
}
