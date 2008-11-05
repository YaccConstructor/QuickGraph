using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    public class VertexEventArgs<TVertex> : EventArgs
    {
        private readonly TVertex vertex;
        public VertexEventArgs(TVertex vertex)
        {
            CodeContract.Requires(vertex != null);
            this.vertex = vertex;
        }

        public TVertex Vertex
        {
            get { return this.vertex; }
        }
    }

    public delegate void VertexEventHandler<Vertex>(
        object sender,
        VertexEventArgs<Vertex> e);
}
