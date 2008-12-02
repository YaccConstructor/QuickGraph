using System;
using System.Collections.Generic;

namespace QuickGraph
{
    /// <summary>
    /// A directed graph datastructure that is efficient
    /// to traverse both in and out edges.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
    public interface IBidirectionalGraph<TVertex,TEdge> : 
        IVertexAndEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Determines whether <paramref name="v"/> has no in-edges.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>
        /// 	<c>true</c> if <paramref name="v"/> has no in-edges; otherwise, <c>false</c>.
        /// </returns>
        bool IsInEdgesEmpty(TVertex v);

        /// <summary>
        /// Gets the number of in-edges of <paramref name="v"/>
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <returns>The number of in-edges pointing towards <paramref name="v"/></returns>
        int InDegree(TVertex v);

        /// <summary>
        /// Gets the collection of in-edges of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>The collection of in-edges of <paramref name="v"/></returns>
        IEnumerable<TEdge> InEdges(TVertex v);

        /// <summary>
        /// Gets the in-edge at location <paramref name="index"/>.
        /// </summary>
        /// <param name="v">The vertex.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        TEdge InEdge(TVertex v, int index);

        /// <summary>
        /// Gets the degree of <paramref name="v"/>, i.e.
        /// the sum of the out-degree and in-degree of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">The vertex</param>
        /// <returns>The sum of <see cref="OutDegree"/> and <see cref="InDegree"/> of <paramref name="v"/></returns>
        int Degree(TVertex v);
    }
}
