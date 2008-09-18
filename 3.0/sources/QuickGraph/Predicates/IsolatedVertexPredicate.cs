using System;

namespace QuickGraph.Predicates
{
    /// <summary>
    /// A vertex predicate that detects vertex with no in or out edges.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public sealed class IsolatedVertexPredicate<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IBidirectionalGraph<TVertex, TEdge> visitedGraph;

        public IsolatedVertexPredicate(IBidirectionalGraph<TVertex,TEdge> visitedGraph)
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            this.visitedGraph = visitedGraph;
        }

        public bool Test(TVertex v)
        {
            return this.visitedGraph.IsInEdgesEmpty(v)
                && this.visitedGraph.IsOutEdgesEmpty(v);
        }
    }
}
