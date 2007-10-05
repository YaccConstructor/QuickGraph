namespace QuickGraph.Algorithms.RandomWalks
{
    public interface IEdgeChain<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        TEdge Successor(IImplicitGraph<TVertex, TEdge> g, TVertex u);
    }
}
