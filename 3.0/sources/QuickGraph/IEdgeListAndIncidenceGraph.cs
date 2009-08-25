namespace QuickGraph
{
    /// <summary>
    /// An incidence graph whose edges can be enumerated
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public interface IEdgeListAndIncidenceGraph<TVertex,TEdge> 
        : IEdgeListGraph<TVertex,TEdge>
        , IIncidenceGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
