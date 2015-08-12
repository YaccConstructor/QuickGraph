using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
namespace QuickGraph
{
    /// <summary>
    /// A mutable bidirectional directed graph
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    [ContractClass(typeof(IMutableBidirectionalGraphContract<,>))]
    public interface IMutableBidirectionalGraph<TVertex,TEdge> 
        : IMutableVertexAndEdgeListGraph<TVertex,TEdge>
        , IBidirectionalGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Removes in-edges of <paramref name="v"/> that match
        /// predicate <paramref name="edgePredicate"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="edgePredicate"></param>
        /// <returns>Number of edges removed</returns>
        int RemoveInEdgeIf(TVertex v, EdgePredicate<TVertex, TEdge> edgePredicate);

        /// <summary>
        /// Clears in-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v"></param>
        void ClearInEdges(TVertex v);

        /// <summary>
        /// Clears in-edges and out-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v"></param>
        void ClearEdges(TVertex v);
    }
}
