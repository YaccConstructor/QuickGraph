using System;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    public sealed class FormatVertexEventArgs<V> : VertexEventArgs<V>
    {
        private readonly GraphvizVertex vertexFormatter;

        public FormatVertexEventArgs(GraphvizVertex vertexFormatter, V v)
			: base(v)
        {
            CodeContract.Requires(vertexFormatter != null);
            this.vertexFormatter = vertexFormatter;
        }

        public GraphvizVertex VertexFormatter
        {
            get
            {
                return vertexFormatter;
            }
        }
    }

    public delegate void FormatVertexEventHandler<TVertex>(
        Object sender,
        FormatVertexEventArgs<TVertex> e);
}
