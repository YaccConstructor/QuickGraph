using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Glee
{
    public sealed class GleeToStringGraphPopulator<TVertex,TEdge> : GleeDefaultGraphPopulator<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IFormatProvider formatProvider;
        private readonly string format;

        public GleeToStringGraphPopulator(
            IEdgeListGraph<TVertex, TEdge> visitedGraph,
            IFormatProvider formatProvider,
            string format
            )
            :base(visitedGraph)
        {
            this.formatProvider = formatProvider;
            if (String.IsNullOrEmpty(format))
                this.format = "{0}";
            else
                this.format = format;
        }

        public IFormatProvider FormatProvider
        {
            get { return this.formatProvider; }
        }

        public string Format
        {
            get { return this.format; }
        }

        protected override string GetVertexId(TVertex v)
        {
            return String.Format(this.formatProvider, this.format, v);
        }
    }
}
