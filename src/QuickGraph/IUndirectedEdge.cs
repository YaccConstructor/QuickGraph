using System;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An undirected edge. 
    /// </summary>
    /// <remarks>
    /// Invariant: source must be less or equal to target (using the default comparer)
    /// </remarks>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    [ContractClass(typeof(IUndirectedEdgeContract<>))]
    public interface IUndirectedEdge<TVertex>
        : IEdge<TVertex>
    {
    }
}
