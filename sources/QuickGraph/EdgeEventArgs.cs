using System;

namespace QuickGraph
{
    [Serializable]
    public class EdgeEventArgs<V, E> : EventArgs
        where E : IEdge<V>
    {
        private E edge;
        public EdgeEventArgs(E edge)
        {
            if (edge == null)
                throw new ArgumentNullException("edge");
            this.edge = edge;
        }

        public E Edge
        {
            get { return this.edge; }
        }
    }

    public delegate void EdgeEventHandler<Vertex,Edge>(
        Object sender,
        EdgeEventArgs<Vertex,Edge> e)
        where Edge : IEdge<Vertex>;
}
