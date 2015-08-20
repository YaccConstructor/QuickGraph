using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    public class VertexEventArgs<TVertex> : EventArgs
    {
        private readonly TVertex vertex;
        public VertexEventArgs(TVertex vertex)
        {
            Contract.Requires(vertex != null);
            this.vertex = vertex;
        }

        public TVertex Vertex
        {
            get { return this.vertex; }
        }
    }

    public delegate void VertexAction<in TVertex>(TVertex vertex);

    public delegate void VertexEventHandler<TVertex>(
        Object sender,
        VertexEventArgs<TVertex> e);
}
