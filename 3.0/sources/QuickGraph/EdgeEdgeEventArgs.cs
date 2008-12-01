using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    public sealed class EdgeEdgeEventArgs<TVertex, TEdge> 
        : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge targetEdge;

        public EdgeEdgeEventArgs(TEdge edge, TEdge targetEdge)
            :base(edge)
        {
            Contract.Requires(targetEdge != null);

            this.targetEdge = targetEdge;
        }

        public TEdge TargetEdge
        {
            get { return this.targetEdge;}
        }
    }

    public delegate void EdgeEdgeEventHandler<TVertex, TEdge>(
        Object sender,
        EdgeEdgeEventArgs<TVertex, TEdge> e)
        where TEdge : IEdge<TVertex>;
}
