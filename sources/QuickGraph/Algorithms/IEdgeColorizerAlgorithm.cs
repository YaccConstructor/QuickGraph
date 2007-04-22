using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public interface IEdgeColorizerAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        IDictionary<Edge, GraphColor> EdgeColors { get;}
    }
}
