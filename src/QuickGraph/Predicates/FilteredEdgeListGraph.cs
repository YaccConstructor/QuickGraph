using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class FilteredEdgeListGraph<TVertex, TEdge, TGraph>
        : FilteredImplicitVertexSet<TVertex, TEdge, TGraph>
        , IEdgeListGraph<TVertex, TEdge>
        where TGraph : IEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public FilteredEdgeListGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
            )
            : base(baseGraph, vertexPredicate, edgePredicate)
        {}

        public bool IsVerticesEmpty
        {
            get
            {
                foreach (var v in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(v))
                        return false;
                return true;
            }
        }

        public int VertexCount
        {
            get
            {
                int count = 0;
                foreach (var v in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(v))
                        count++;
                return count;
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get
            {
                foreach (var v in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(v))
                        yield return v;
            }
        }

        public bool IsEdgesEmpty
        {
            get
            {
                foreach (var edge in this.BaseGraph.Edges)
                    if (this.FilterEdge(edge))
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
                    if (this.FilterEdge(edge))
                        count++;
                return count;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var edge in this.BaseGraph.Edges)
                    if (this.FilterEdge(edge))
                        yield return edge;
            }
        }

        [Pure]
        private bool FilterEdge(TEdge edge)
        {
            return this.VertexPredicate(edge.Source)
                        && this.VertexPredicate(edge.Target)
                        && this.EdgePredicate(edge);
        }

        [Pure]
        public bool ContainsEdge(TEdge edge)
        {
            return
                this.FilterEdge(edge) &&
                this.BaseGraph.ContainsEdge(edge);
        }
    }
}