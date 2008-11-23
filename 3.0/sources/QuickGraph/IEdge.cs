using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A graph edge
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
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
