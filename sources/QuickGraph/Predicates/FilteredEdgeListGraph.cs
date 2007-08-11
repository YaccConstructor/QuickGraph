using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class FilteredEdgeListGraph<Vertex,Edge,Graph> :
        FilteredGraph<Vertex,Edge,Graph>,
        IEdgeListGraph<Vertex,Edge>
        where Graph : IEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        public FilteredEdgeListGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex,Edge> edgePredicate
            )
        :base(baseGraph, vertexPredicate, edgePredicate)
        {
        }
        public bool IsEdgesEmpty
        {
            get 
            { 
                foreach(Edge edge in this.Edges)
                    return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get 
            { 
                int count = 0;
                foreach(Edge edge in this.Edges)
                    count++;
                return count;
            }
        }

        public IEnumerable<Edge> Edges
        {
            get 
            { 
                foreach(Edge edge in this.BaseGraph.Edges)
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

        public bool ContainsEdge(Edge edge)
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
