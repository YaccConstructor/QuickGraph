using System;

namespace QuickGraph.Algorithms
{
    public interface IVertexTimeStamperAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexEventHandler<TVertex> DiscoverVertex;
        event VertexEventHandler<TVertex> FinishVertex;
    }
}
