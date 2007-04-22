using System;

namespace QuickGraph.Algorithms
{
    public interface ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        event EdgeEventHandler<Vertex, Edge> TreeEdge;
    }
}
