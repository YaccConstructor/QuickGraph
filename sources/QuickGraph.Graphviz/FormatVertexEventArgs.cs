using System;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public sealed class FormatVertexEventArgs<V> : VertexEventArgs<V>
    {
        private GraphvizVertex vertexFormatter;

        public FormatVertexEventArgs(GraphvizVertex vertexFormatter, V v)
			: base(v)
        {
            if (vertexFormatter == null)
                throw new ArgumentNullException("vertexFormatter");
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
