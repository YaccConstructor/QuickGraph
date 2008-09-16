using System;
using System.Collections.Generic;

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
            return this.InDegree(v) == 0;
        }

        public int InDegree(TVertex v)
        {
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

        public bool ContainsEdge(TEdge edge)
        {
            if (!TestEdge(edge))
                return false;
            return this.BaseGraph.ContainsEdge(edge);
        }

        public TEdge InEdge(TVertex v, int index)
        {
            throw new NotSupportedException();
        }
    }
}
