using System;

namespace QuickGraph.Algorithms
{
    public interface IUndirectedVertexPredecessorRecorderAlgorithm<TVertex,TEdge> 
        : IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexAction<TVertex> StartVertex;
        event VertexAction<TVertex> FinishVertex;
    }
}
