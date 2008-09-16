using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class FilteredUndirectedGraph<TVertex,TEdge,TGraph> :
        FilteredGraph<TVertex,TEdge,TGraph>,
        IUndirectedGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IUndirectedGraph<TVertex,TEdge>
    {
        public FilteredUndirectedGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
            )
            : base(baseGraph, vertexPredicate, edgePredicate)
        { }

        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            if (this.VertexPredicate(v))
            {
                foreach (var edge in this.BaseGraph.AdjacentEdges(v))
                {
                    if (TestEdge(edge))
                        yield return edge;
                }
            }
        }

        public int AdjacentDegree(TVertex v)
        {
            int count = 0;
            foreach (var edge in this.AdjacentEdges(v))
                count++;
            return count;
        }

        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            foreach (var edge in this.AdjacentEdges(v))
                return false;
            return true;
        }

        public TEdge AdjacentEdge(TVertex v, int index)
        {
            if (this.VertexPredicate(v))
            {
                int count = 0;
                foreach (var edge in this.AdjacentEdges(v))
                {
                    if (count == index)
                        return edge;
                    count++;
                }
            }

            throw new IndexOutOfRangeException();
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            if (!this.VertexPredicate(source))
                return false;
            if (!this.VertexPredicate(target))
                return false;
            if (!this.BaseGraph.ContainsEdge(source, target))
                return false;
            // we need to find the edge
            foreach (var edge in this.Edges)
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
                foreach (var edge in this.Edges)
                    return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get 
            {
                int count = 0;
                foreach (var edge in this.Edges)
                    count++;
                return count;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get 
            {
                foreach (var edge in this.BaseGraph.Edges)
                    if (this.TestEdge(edge))
                        yield return edge;
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            if (!this.TestEdge(edge))
                return false;
            return this.BaseGraph.ContainsEdge(edge);
        }

        public bool IsVerticesEmpty
        {
            get 
            {
                foreach (var vertex in this.Vertices)
                    return false;
                return true;
            }
        }

        public int VertexCount
        {
            get 
            {
                int count = 0;
                foreach (var vertex in this.Vertices)
                    count++;
                return count;
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get 
            {
                foreach (var vertex in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(vertex))
                        yield return vertex;
            }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            if (!this.VertexPredicate(vertex))
                return false;
            else
                return this.BaseGraph.ContainsVertex(vertex);
        }
    }
}
