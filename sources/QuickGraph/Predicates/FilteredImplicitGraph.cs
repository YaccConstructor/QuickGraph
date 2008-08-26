using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredImplicitGraph<TVertex, TEdge, TGraph> :
        FilteredGraph<TVertex, TEdge, TGraph>,
        IImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IImplicitGraph<TVertex, TEdge>
    {
        public FilteredImplicitGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(TVertex v)
        {
            int count =0;
            foreach (var edge in this.BaseGraph.OutEdges(v))
                if (this.TestEdge(edge))
                    count++;
            return count;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            foreach (var edge in this.BaseGraph.OutEdges(v))
                if (this.TestEdge(edge))
                    yield return edge;
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            throw new NotSupportedException();
        }
    }
}
