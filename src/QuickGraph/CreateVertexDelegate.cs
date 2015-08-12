using System;
namespace QuickGraph
{
    /// <summary>
    /// A vertex factory delegate.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public delegate TVertex CreateVertexDelegate<TVertex, TEdge>(
        IVertexListGraph<TVertex,TEdge> g) 
    where TEdge : IEdge<TVertex>;
}
