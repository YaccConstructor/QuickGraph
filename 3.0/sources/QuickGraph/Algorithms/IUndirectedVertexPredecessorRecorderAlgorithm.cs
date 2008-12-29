using System;

namespace QuickGraph.Algorithms
{
    public interface IUndirectedVertexPredecessorRecorderAlgorithm<TVertex,TEdge> 
        : IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexEventHandler<TVertex> StartVertex;
        event VertexEventHandler<TVertex> FinishVertex;
    }
}
