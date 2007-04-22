namespace QuickGraph
{
    public interface IEdge<Vertex>
    {
        Vertex Source { get;}
        Vertex Target { get;}
    }
}
