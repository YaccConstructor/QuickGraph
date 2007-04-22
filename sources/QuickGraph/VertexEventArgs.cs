using System;

namespace QuickGraph
{
    [Serializable]
    public class VertexEventArgs<V> : EventArgs
    {
        private readonly V vertex;
        public VertexEventArgs(V vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException("v");
            this.vertex = vertex;
        }

        public V Vertex
        {
            get { return this.vertex; }
        }
    }

    public delegate void VertexEventHandler<Vertex>(
        object sender,
        VertexEventArgs<Vertex> e);
}
