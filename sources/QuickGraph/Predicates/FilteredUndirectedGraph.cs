using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class FilteredUndirectedGraph<Vertex,Edge,Graph> :
        FilteredGraph<Vertex,Edge,Graph>,
        IUndirectedGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
        where Graph : IUndirectedGraph<Vertex,Edge>
    {
        public FilteredUndirectedGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex, Edge> edgePredicate
            )
            : base(baseGraph, vertexPredicate, edgePredicate)
        { }

        public IEnumerable<Edge> AdjacentEdges(Vertex v)
        {
            if (this.VertexPredicate(v))
            {
                foreach (Edge edge in this.BaseGraph.AdjacentEdges(v))
                {
                    if (TestEdge(edge))
                        yield return edge;
                }
            }
        }

        public int AdjacentDegree(Vertex v)
        {
            int count = 0;
            foreach (Edge edge in this.AdjacentEdges(v))
                count++;
            return count;
        }

        public bool IsAdjacentEdgesEmpty(Vertex v)
        {
            foreach (Edge edge in this.AdjacentEdges(v))
                return false;
            return true;
        }

        public Edge AdjacentEdge(Vertex v, int index)
        {
            if (this.VertexPredicate(v))
            {
                int count = 0;
                foreach (Edge edge in this.AdjacentEdges(v))
                {
                    if (count == index)
                        return edge;
                    count++;
                }
            }

            throw new IndexOutOfRangeException();
        }

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            if (!this.VertexPredicate(source))
                return false;
            if (!this.VertexPredicate(target))
                return false;
            if (!this.BaseGraph.ContainsEdge(source, target))
                return false;
            // we need to find the edge
            foreach (Edge edge in this.Edges)
            {
                if (edge.Source.Equals(source) && edge.Target.Equals(target)
                    && this.EdgePredicate(edge))
                    return true;
            }

            return false;
        }

        public bool IsEdgesEmpty
        {
            get 
            {
                foreach (Edge edge in this.Edges)
                    return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get 
            {
                int count = 0;
                foreach (Edge edge in this.Edges)
                    count++;
                return count;
            }
        }

        public IEnumerable<Edge> Edges
        {
            get 
            {
                foreach (Edge edge in this.BaseGraph.Edges)
                    if (this.TestEdge(edge))
                        yield return edge;
            }
        }

        public bool ContainsEdge(Edge edge)
        {
            if (!this.TestEdge(edge))
                return false;
            return this.BaseGraph.ContainsEdge(edge);
        }

        public bool IsVerticesEmpty
        {
            get 
            {
                foreach (Vertex vertex in this.Vertices)
                    return false;
                return true;
            }
        }

        public int VertexCount
        {
            get 
            {
                int count = 0;
                foreach (Vertex vertex in this.Vertices)
                    count++;
                return count;
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get 
            {
                foreach (Vertex vertex in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(vertex))
                        yield return vertex;
            }
        }

        public bool ContainsVertex(Vertex vertex)
        {
            if (!this.VertexPredicate(vertex))
                return false;
            else
                return this.BaseGraph.ContainsVertex(vertex);
        }
    }
}
