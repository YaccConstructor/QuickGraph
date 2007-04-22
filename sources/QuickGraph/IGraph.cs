using System;

namespace QuickGraph
{
    public interface IGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool IsDirected { get;}
        bool AllowParallelEdges { get;}
    }
}
