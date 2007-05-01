using System;
using System.IO;
using QuickGraph.Algorithms.Condensation;

namespace QuickGraph.Graphviz
{
    public class EdgeMergeCondensatedGraphRenderer<Vertex, Edge> :
        GraphRendererBase<Vertex, MergedEdge<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        public EdgeMergeCondensatedGraphRenderer(
            IVertexAndEdgeListGraph<Vertex, MergedEdge<Vertex, Edge>> visitedGraph)
            :base(visitedGraph)
        { }

        protected override void Initialize()
        {
            base.Initialize();
            this.Graphviz.FormatVertex += new FormatVertexEventHandler<Vertex>(Graphviz_FormatVertex);
            this.Graphviz.FormatEdge += new FormatEdgeEventHandler<Vertex, MergedEdge<Vertex, Edge>>(Graphviz_FormatEdge);
        }

        void Graphviz_FormatEdge(object sender, FormatEdgeEventArgs<Vertex, MergedEdge<Vertex, Edge>> e)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("{0}", e.Edge.Edges.Count);
            foreach (Edge edge in e.Edge.Edges)
                sw.WriteLine("  {0}", edge);
            e.EdgeFormatter.Label.Value = this.Graphviz.Escape(sw.ToString());
        }

        void Graphviz_FormatVertex(Object sender, FormatVertexEventArgs<Vertex> e)
        {
            e.VertexFormatter.Label = e.Vertex.ToString();
        }
    }
}
