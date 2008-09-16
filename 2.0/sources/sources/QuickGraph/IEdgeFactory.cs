using System;

namespace QuickGraph
{
    public interface IEdgeFactory<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        TEdge CreateEdge(TVertex source, TVertex target);
    }
}
