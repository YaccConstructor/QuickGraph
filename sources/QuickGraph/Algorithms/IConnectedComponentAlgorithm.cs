using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms
{
    public interface IConnectedComponentAlgorithm<Vertex,Edge,Graph> : IAlgorithm<Graph>
        where Graph : IGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        int ComponentCount { get;}
        IDictionary<Vertex, int> Components { get;}
    }
}
