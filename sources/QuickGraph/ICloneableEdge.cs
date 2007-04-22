using System;

namespace QuickGraph
{
    public interface ICloneableEdge<Vertex> : IEdge<Vertex>
    {
        ICloneableEdge<Vertex> Clone(Vertex source, Vertex target);
    }
}
