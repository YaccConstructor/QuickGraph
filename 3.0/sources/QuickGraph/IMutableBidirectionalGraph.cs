namespace QuickGraph
{
    public interface IMutableBidirectionalGraph<TVertex,TEdge> :
        IMutableVertexAndEdgeListGraph<TVertex,TEdge>,
        IBidirectionalGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        int RemoveInEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> edgePredicate);
        void ClearInEdges(TVertex v);
        void ClearEdges(TVertex v);
    }
}
