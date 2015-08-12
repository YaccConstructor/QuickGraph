using System.Collections.Generic;
namespace QuickGraph.Algorithms.RandomWalks
{    
    public interface IEdgeChain<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool TryGetSuccessor(IImplicitGraph<TVertex, TEdge> g, TVertex u, out TEdge successor);
        bool TryGetSuccessor(IEnumerable<TEdge> edges, TVertex u, out TEdge successor);
    }
}
