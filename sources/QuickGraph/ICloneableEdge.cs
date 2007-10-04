using System;

namespace QuickGraph
{
    public interface ICloneableEdge<TVertex> : IEdge<TVertex>
    {
        ICloneableEdge<TVertex> Clone(TVertex source, TVertex target);
    }
}
