using System;

namespace QuickGraph.Algorithms
{
    public interface IEdgePredecessorRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        event EdgeEdgeAction<TVertex, TEdge> DiscoverTreeEdge;
        event EdgeAction<TVertex,TEdge> FinishEdge;
    }
}
