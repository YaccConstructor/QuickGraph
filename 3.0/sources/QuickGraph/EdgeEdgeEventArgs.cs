using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An event involving two edges.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
    [Serializable]
    public sealed class EdgeEdgeEventArgs<TVertex, TEdge> 
        : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge targetEdge;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeEdgeEventArgs&lt;TVertex, TEdge&gt;"/> class.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="targetEdge">The target edge.</param>
        public EdgeEdgeEventArgs(TEdge edge, TEdge targetEdge)
            :base(edge)
        {
            Contract.Requires(targetEdge != null);

            this.targetEdge = targetEdge;
        }

        /// <summary>
        /// Gets the target edge.
        /// </summary>
        /// <value>The target edge.</value>
        public TEdge TargetEdge
        {
            [Pure]
            get { return this.targetEdge;}
        }
    }

    /// <summary>
    /// The handler for events with <see cref="EdgeEdgeEventArgs&lt;TVertex, TEdge&gt;"/>
    /// </summary>
    public delegate void EdgeEdgeEventHandler<TVertex, TEdge>(
        Object sender,
        EdgeEdgeEventArgs<TVertex, TEdge> e)
        where TEdge : IEdge<TVertex>;
}
