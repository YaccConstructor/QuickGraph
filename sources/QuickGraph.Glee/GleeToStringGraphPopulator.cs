using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Glee
{
    public sealed class GleeToStringGraphPopulator<Vertex,Edge> : GleeDefaultGraphPopulator<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private readonly IFormatProvider formatProvider;
        private readonly string format;

        public GleeToStringGraphPopulator(
            IVertexAndEdgeSet<Vertex, Edge> visitedGraph,
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

        protected override string GetVertexId(Vertex v)
        {
            return String.Format(this.formatProvider, this.format, v);
        }
    }
}
