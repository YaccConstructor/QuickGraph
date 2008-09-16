using System;

namespace QuickGraph.Predicates
{
    public sealed class IsolatedVertexPredicate<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IBidirectionalGraph<TVertex, TEdge> visitedGraph;

        public IsolatedVertexPredicate(IBidirectionalGraph<TVertex,TEdge> visitedGraph)
        {
            this.visitedGraph = visitedGraph;
        }

        public bool Test(TVertex v)
        {
            return this.visitedGraph.IsInEdgesEmpty(v)
                && this.visitedGraph.IsOutEdgesEmpty(v);
        }
    }
}
