using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredVertexAndEdgeListGraph<Vertex, Edge, Graph> :
        FilteredVertexListGraph<Vertex, Edge, Graph>,
        IVertexAndEdgeListGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        public FilteredVertexAndEdgeListGraph(
            Graph baseGraph,
            IVertexPredicate<Vertex> vertexPredicate,
            IEdgePredicate<Vertex, Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsEdgesEmpty
        {
            get
            {
                return this.EdgeCount == 0;
            }
        }

        public int EdgeCount
        {
            get
            {
                int count = 0;
                foreach (Edge edge in this.BaseGraph.Edges)
                {
                    if (
                           this.VertexPredicate.Test(edge.Source)
                        && this.VertexPredicate.Test(edge.Target)
                        && this.EdgePredicate.Test(edge))
                        count++;
                }
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
                           this.VertexPredicate.Test(edge.Source)
                        && this.VertexPredicate.Test(edge.Target)
                        && this.EdgePredicate.Test(edge))
                        yield return edge;
                }
            }
        }

        public bool ContainsEdge(Edge edge)
        {
            foreach (Edge e in this.Edges)
                if (Comparison<Edge>.Equals(edge, e))
                    return true;
            return false;
        }
    }
}
