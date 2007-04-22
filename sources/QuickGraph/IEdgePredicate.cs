using System;

namespace QuickGraph
{
    public interface IEdgePredicate<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool Test(Edge edge);
    }
}
