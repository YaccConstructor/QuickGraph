namespace QuickGraph
{
    /// <summary>
    /// A graph edge
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    public interface IEdge<TVertex>
    {
        /// <summary>
        /// Gets the source vertex
        /// </summary>
        TVertex Source { get;}
        /// <summary>
        /// Gets the target vertex
        /// </summary>
        TVertex Target { get;}
    }
}
