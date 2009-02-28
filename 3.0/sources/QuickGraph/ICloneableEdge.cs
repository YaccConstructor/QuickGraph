using System;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A cloneable edge
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    [ContractClass(typeof(ICloneableEdgeContract<>))]
    public interface ICloneableEdge<TVertex> 
        : IEdge<TVertex>
    {
        /// <summary>
        /// Clones the edge content to a different pair of <paramref name="source"/>
        /// and <paramref name="target"/> vertices
        /// </summary>
        /// <param name="source">The source vertex of the new edge</param>
        /// <param name="target">The target vertex of the new edge</param>
        /// <returns>A clone of the edge with new source and target vertices</returns>
        ICloneableEdge<TVertex> Clone(TVertex source, TVertex target);
    }
}
