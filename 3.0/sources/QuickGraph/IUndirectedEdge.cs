using System;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An undirected edge. Invariant: source must be less or equal to target (using the default comparer)
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    [ContractClass(typeof(IUndirectedEdgeContract<>))]
    public interface IUndirectedEdge<TVertex>
        : IEdge<TVertex>
    {
    }
}
