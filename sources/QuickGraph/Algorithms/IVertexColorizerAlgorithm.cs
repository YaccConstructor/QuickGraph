using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public interface IVertexColorizerAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        IDictionary<Vertex, GraphColor> VertexColors { get;}
    }
}
