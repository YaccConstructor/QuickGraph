using System;
using System.Collections.Generic;
using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A implicit directed graph datastructure
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
   [ContractClass(typeof(IImplicitGraphContract<,>))]
   public interface IImplicitGraph<TVertex,TEdge> 
        : IGraph<TVertex,TEdge>
        , IImplicitVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Determines whether there are out-edges associated to <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>
        /// 	<c>true</c> if <paramref name="v"/> has no out-edges; otherwise, <c>false</c>.
        /// </returns>
       [Pure]
       bool IsOutEdgesEmpty(TVertex v);

        /// <summary>
        /// Gets the count of out-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>The count of out-edges of <paramref name="v"/></returns>
        [Pure]
        int OutDegree(TVertex v);

        /// <summary>
        /// Gets the out-edges of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>An enumeration of the out-edges of <paramref name="v"/>.</returns>
        [Pure]
        IEnumerable<TEdge> OutEdges(TVertex v);

        /// <summary>
        /// Tries to get the out-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="edges"></param>
        /// <returns></returns>
        [Pure]
        bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges);

        /// <summary>
        /// Gets the out-edge of <paramref name="v"/> at position <paramref name="index"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <param name="index">The index.</param>
        /// <returns>The out-edge at position <paramref name="index"/></returns>
        [Pure]
        TEdge OutEdge(TVertex v, int index);
    }
}
