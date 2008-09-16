using System;

namespace QuickGraph
{
    [Serializable]
    public sealed class EdgeEdgeEventArgs<TVertex, TEdge> : EventArgs
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge edge;
        private readonly TEdge targetEdge;
        public EdgeEdgeEventArgs(TEdge edge, TEdge targetEdge)
        {
            GraphContracts.AssumeNotNull(edge, "edge");
            GraphContracts.AssumeNotNull(targetEdge, "targetEdge");
            this.edge = edge;
            this.targetEdge = targetEdge;
        }

        public TEdge Edge
        {
            get { return this.edge; }
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
