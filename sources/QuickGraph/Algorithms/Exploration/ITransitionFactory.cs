using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Exploration
{
    public interface ITransitionFactory<Vertex,Edge>
        where Vertex : ICloneable
        where Edge : IEdge<Vertex>
    {
        bool IsValid(Vertex v);
        IEnumerable<Edge> Apply(Vertex source);
    }
}
