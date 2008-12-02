using System;
namespace QuickGraph
{
    /// <summary>
    /// A vertex factory delegate.
    /// </summary>
    [Serializable]
    public delegate TVertex CreateVertexDelegate<TVertex, TEdge>(
        IVertexListGraph<TVertex,TEdge> g) 
    where TEdge : IEdge<TVertex>;
}
