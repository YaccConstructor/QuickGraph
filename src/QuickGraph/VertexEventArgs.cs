using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class VertexEventArgs<TVertex> : EventArgs
    {
        private readonly TVertex vertex;
        protected VertexEventArgs(TVertex vertex)
        {
            Contract.Requires(vertex != null);
            this.vertex = vertex;
        }

        public TVertex Vertex
        {
            get { return this.vertex; }
        }
    }

    public delegate void VertexAction<TVertex>(TVertex vertex);
}
