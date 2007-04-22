using System;

namespace QuickGraph
{
    [Serializable]
    public sealed class EdgeEdgeEventArgs<V, E> : EventArgs
        where E : IEdge<V>
    {
        private E edge;
        private E targetEdge;
        public EdgeEdgeEventArgs(E edge, E targetEdge)
        {
            if (edge == null)
                throw new ArgumentNullException("edge");
            if (targetEdge==null)
                throw new ArgumentNullException("targetEdge");
            this.edge = edge;
            this.targetEdge = targetEdge;
        }

        public E Edge
        {
            get { return this.edge; }
        }

        public E TargetEdge
        {
            get { return this.targetEdge;}
        }
    }

    public delegate void EdgeEdgeEventHandler<Vertex, Edge>(
        Object sender,
        EdgeEdgeEventArgs<Vertex, Edge> e)
        where Edge : IEdge<Vertex>;
}
