using System;
namespace QuickGraph.Algorithms.RandomWalks
{
    public interface IMarkovEdgeChain<Vertex,Edge> : IEdgeChain<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        Random Rand { get;set;}
    }
}
