using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class FilteredEdgeListGraph<TVertex,TEdge,TGraph> :
        FilteredGraph<TVertex,TEdge,TGraph>,
        IEdgeListGraph<TVertex,TEdge>
        where TGraph : IEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        public FilteredEdgeListGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex,TEdge> edgePredicate
            )
        :base(baseGraph, vertexPredicate, edgePredicate)
        {
        }
        public bool IsEdgesEmpty
        {
            get 
            { 
                foreach(TEdge edge in this.Edges)
                    return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get 
            { 
                int count = 0;
                foreach(TEdge edge in this.Edges)
                    count++;
                return count;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get 
            { 
                foreach(TEdge edge in this.BaseGraph.Edges)
                {
                    if (
                        this.VertexPredicate(edge.Source)
                        && this.VertexPredicate(edge.Target)
                        && this.EdgePredicate(edge)
                        )
                        yield return edge;
                }
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            if (
                this.VertexPredicate(edge.Source)
                && this.VertexPredicate(edge.Target)
                && this.EdgePredicate(edge)
                )
                return this.BaseGraph.ContainsEdge(edge);
            else
                return false;
        }
    }
}
