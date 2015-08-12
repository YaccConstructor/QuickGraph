using System;
namespace QuickGraph.Algorithms.RandomWalks
{
    public interface IMarkovEdgeChain<TVertex,TEdge> 
        : IEdgeChain<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        Random Rand { get;set;}
    }
}
