using System;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public sealed class GleeVertexEventArgs<Vertex> : VertexEventArgs<Vertex>
    {
        private readonly Node node;

        public GleeVertexEventArgs(Vertex vertex, Node node)
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
