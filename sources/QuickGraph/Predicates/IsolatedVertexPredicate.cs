using System;

namespace QuickGraph.Predicates
{
    public sealed class IsolatedVertexPredicate<Vertex,Edge> :
        IVertexPredicate<Vertex>
        where Edge : IEdge<Vertex>
    {
        private IBidirectionalGraph<Vertex, Edge> visitedGraph;

        public IsolatedVertexPredicate(IBidirectionalGraph<Vertex,Edge> visitedGraph)
        {
            this.visitedGraph = visitedGraph;
        }

        public bool Test(Vertex v)
        {
            return this.visitedGraph.IsInEdgesEmpty(v)
                && this.visitedGraph.IsOutEdgesEmpty(v);
        }
    }
}
