using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A set of edges
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
    [ContractClass(typeof(IEdgeSetContract<,>))]
    public interface IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Gets a value indicating whether there are no edges in this set.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this set is empty; otherwise, <c>false</c>.
        /// </value>
        bool IsEdgesEmpty { get; }
        /// <summary>
        /// Gets the edge count.
        /// </summary>
        /// <value>The edge count.</value>
        int EdgeCount { get; }
        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <value>The edges.</value>
        IEnumerable<TEdge> Edges { get; }
        /// <summary>
        /// Determines whether the specified edge contains edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <returns>
        /// 	<c>true</c> if the specified edge contains edge; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        bool ContainsEdge(TEdge edge);
    }


}
