using System;

namespace QuickGraph
{
    [Serializable]
    public class VertexEventArgs<TVertex> : EventArgs
    {
        private readonly TVertex vertex;
        public VertexEventArgs(TVertex vertex)
        {
            GraphContracts.AssumeNotNull(vertex, "vertex");
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
