using System;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A cloneable edge
    /// </summary>
    /// <typeparam name="TVertex">type of the vertex</typeparam>
    /// <typeparam name="TEdge">type of the edge</typeparam>
    [ContractClass(typeof(ICloneableEdgeContract<,>))]
    public interface ICloneableEdge<TVertex, TEdge> 
        : IEdge<TVertex>
        where TEdge : IEdge<TVertex>
    {
        /// <summary>
        /// Clones the edge content to a different pair of <paramref name="source"/>
        /// and <paramref name="target"/> vertices
        /// </summary>
        /// <param name="source">The source vertex of the new edge</param>
        /// <param name="target">The target vertex of the new edge</param>
        /// <returns>A clone of the edge with new source and target vertices</returns>
        TEdge Clone(TVertex source, TVertex target);
    }
}
