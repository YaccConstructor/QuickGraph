using System;

namespace QuickGraph.Algorithms
{
    public interface IEdgePredecessorRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        event EdgeEdgeEventHandler<TVertex, TEdge> DiscoverTreeEdge;
        event EdgeEventHandler<TVertex,TEdge> FinishEdge;
    }
}
