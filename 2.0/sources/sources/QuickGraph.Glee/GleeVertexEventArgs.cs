using System;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public sealed class GleeVertexEventArgs<TVertex> : VertexEventArgs<TVertex>
    {
        private readonly Node node;

        public GleeVertexEventArgs(TVertex vertex, Node node)
            : base(vertex)
        {
            this.node = node;
        }

        public Node Node
        {
            get { return this.node; }
        }
    }

    public delegate void GleeVertexNodeEventHandler<Vertex>(
        object sender, 
        GleeVertexEventArgs<Vertex> args);
}
