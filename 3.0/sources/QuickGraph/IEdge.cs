using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A directed edge
    /// </summary>
    /// <typeparam name="TVertex">The vertex type</typeparam>
#if CONTRACTS_FULL
    [ContractClass(typeof(IEdgeContract<>))]
#endif
    public interface IEdge<TVertex>
    {
        /// <summary>
        /// Gets the source vertex
        /// </summary>
        TVertex Source { get;}
        /// <summary>
        /// Gets the target vertex
        /// </summary>
        TVertex Target { get;}
    }
}
