using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class SinkVertexPredicate<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IIncidenceGraph<TVertex, TEdge> visitedGraph;

        public SinkVertexPredicate(IIncidenceGraph<TVertex, TEdge> visitedGraph)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            this.visitedGraph = visitedGraph;
        }

        public bool Test(TVertex v)
        {
            return this.visitedGraph.IsOutEdgesEmpty(v);
        }
    }
}
