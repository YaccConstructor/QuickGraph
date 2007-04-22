namespace QuickGraph.Algorithms.RandomWalks
{
    public interface IEdgeChain<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        Edge Successor(IImplicitGraph<Vertex, Edge> g, Vertex u);
    }
}
