namespace QuickGraph
{
    public interface IMutableBidirectionalGraph<Vertex,Edge> :
        IMutableVertexAndEdgeListGraph<Vertex,Edge>,
        IBidirectionalGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        int RemoveInEdgeIf(Vertex v, EdgePredicate<Vertex, Edge> edgePredicate);
        void ClearInEdges(Vertex v);
        void ClearEdges(Vertex v);
    }
}
