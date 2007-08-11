using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredBidirectionalGraph<Vertex, Edge, Graph> :
        FilteredVertexListGraph<Vertex, Edge, Graph>,
        IBidirectionalGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        public FilteredBidirectionalGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex, Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsInEdgesEmpty(Vertex v)
        {
            return this.InDegree(v) == 0;
        }

        public int InDegree(Vertex v)
        {
            int count = 0;
            foreach (Edge edge in this.InEdges(v))
                if (TestEdge(edge))
                    count++;
            return count;
        }

        public IEnumerable<Edge> InEdges(Vertex v)
        {
            foreach (Edge edge in this.InEdges(v))
                if (TestEdge(edge))
                    yield return edge;
        }

        public int Degree(Vertex v)
        {
            return this.OutDegree(v) - this.InDegree(v);
        }

        public bool IsEdgesEmpty
        {
            get
            {
                foreach (Edge edge in this.BaseGraph.Edges)
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
                foreach (Edge edge in this.BaseGraph.Edges)
                    if (TestEdge(edge))
                        count++;
                return count;
            }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                foreach (Edge edge in this.BaseGraph.Edges)
                    if (TestEdge(edge))
                        yield return edge;
            }
        }

        public bool ContainsEdge(Edge edge)
        {
            if (!TestEdge(edge))
                return false;
            return this.BaseGraph.ContainsEdge(edge);
        }

        public Edge InEdge(Vertex v, int index)
        {
            throw new NotSupportedException();
        }
    }
}
