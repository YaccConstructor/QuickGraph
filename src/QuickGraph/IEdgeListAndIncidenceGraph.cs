namespace QuickGraph
{
    /// <summary>
    /// An incidence graph whose edges can be enumerated
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public interface IEdgeListAndIncidenceGraph<TVertex,TEdge> 
        : IEdgeListGraph<TVertex,TEdge>
        , IIncidenceGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
