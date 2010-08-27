namespace QuickGraph
{
    public interface IEdge<TVertex>
    {
        TVertex Source { get;}
        TVertex Target { get;}
    }
}
