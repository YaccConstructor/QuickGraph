using System;

namespace QuickGraph.Algorithms
{
    public interface IVertexPredecessorRecorderAlgorithm<Vertex,Edge> :
        ITreeBuilderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        event VertexEventHandler<Vertex> StartVertex;
        event VertexEventHandler<Vertex> FinishVertex;
    }
}
