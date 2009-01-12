using System;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    public sealed class FormatEdgeEventArgs<V,E> : EdgeEventArgs<V,E>
        where E : IEdge<V>
    {
        private readonly GraphvizEdge edgeFormatter;

        public FormatEdgeEventArgs(GraphvizEdge edgeFormatter, E e)
			: base(e)
        {
            Contract.Requires(edgeFormatter != null);
            this.edgeFormatter = edgeFormatter;
        }

        /// <summary>
        /// Edge formatter
        /// </summary>
        public GraphvizEdge EdgeFormatter
        {
            get
            {
                return edgeFormatter;
            }
        }
    }

    public delegate void FormatEdgeAction<TVertex, TEdge>(
        object sender, 
        FormatEdgeEventArgs<TVertex,TEdge> e)
        where TEdge : IEdge<TVertex>;
}
