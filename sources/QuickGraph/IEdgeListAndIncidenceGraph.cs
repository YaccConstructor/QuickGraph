namespace QuickGraph
{
    public interface IEdgeListAndIncidenceGraph<Vertex,Edge> :
        IEdgeListGraph<Vertex,Edge>, IIncidenceGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
    }
}
