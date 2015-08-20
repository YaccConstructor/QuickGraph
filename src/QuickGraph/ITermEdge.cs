using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A directed edge with terminal indices
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    [ContractClass(typeof(ITermEdgeContract<>))]
    public interface ITermEdge<TVertex> : IEdge<TVertex>
    {
        /// <summary>
        /// Index of terminal on source vertex to which this edge is attached.
        /// </summary>
        int SourceTerminal { get; }

        /// <summary>
        /// Index of terminal on target vertex to which this edge is attached.
        /// </summary>
        int TargetTerminal { get; }
    }
}
