using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class SinkVertexPredicate<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IIncidenceGraph<Vertex, Edge> visitedGraph;

        public SinkVertexPredicate(IIncidenceGraph<Vertex, Edge> visitedGraph)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            this.visitedGraph = visitedGraph;
        }

        public bool Test(Vertex v)
        {
            return this.visitedGraph.IsOutEdgesEmpty(v);
        }
    }
}
