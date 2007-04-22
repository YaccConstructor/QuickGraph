using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredIncidenceGraph<Vertex, Edge, Graph> :
        FilteredImplicitGraph<Vertex,Edge,Graph>,
        IIncidenceGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
        where Graph : IIncidenceGraph<Vertex,Edge>
    {
        public FilteredIncidenceGraph(
            Graph baseGraph,
            IVertexPredicate<Vertex> vertexPredicate,
            IEdgePredicate<Vertex,Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        {}

        public bool ContainsEdge(Vertex source, Vertex target)
        {
            if (!this.VertexPredicate.Test(source))
                return false;
            if (!this.VertexPredicate.Test(target))
                return false;

            foreach (Edge edge in this.BaseGraph.OutEdges(source))
                if (edge.Target.Equals(target) && this.EdgePredicate.Test(edge))
                    return true;
            return false;
        }
    }
}
