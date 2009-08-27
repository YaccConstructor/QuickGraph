using System;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    public sealed class FormatVertexEventArgs<TVertex> 
        : VertexEventArgs<TVertex>
    {
        private readonly GraphvizVertex vertexFormatter;

        public FormatVertexEventArgs(GraphvizVertex vertexFormatter, TVertex v)
			: base(v)
        {
            Contract.Requires(vertexFormatter != null);

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
