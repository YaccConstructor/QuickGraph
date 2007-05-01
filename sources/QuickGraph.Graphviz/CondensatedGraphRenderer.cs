using System;
using System.IO;
using QuickGraph.Algorithms.Condensation;

namespace QuickGraph.Graphviz
{
    public class CondensatedGraphRenderer<Vertex,Edge,Graph> :
        GraphRendererBase<Graph, CondensatedEdge<Vertex, Edge, Graph>>
        where Edge : IEdge<Vertex>
        where Graph : IMutableVertexAndEdgeListGraph<Vertex, Edge>, new()
    {
        public CondensatedGraphRenderer(
            IVertexAndEdgeListGraph<Graph, CondensatedEdge<Vertex, Edge, Graph>> visitedGraph)
            :base(visitedGraph)
        {}

        protected override void Initialize()
        {
            base.Initialize();
            this.Graphviz.FormatVertex+=new FormatVertexEventHandler<Graph>(Graphviz_FormatVertex);
            this.Graphviz.FormatEdge += new FormatEdgeEventHandler<Graph, CondensatedEdge<Vertex, Edge, Graph>>(Graphviz_FormatEdge);
        }


        void Graphviz_FormatVertex(Object sender, FormatVertexEventArgs<Graph> e)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("{0}-{1}", e.Vertex.VertexCount, e.Vertex.EdgeCount);
            foreach (Vertex v in e.Vertex.Vertices)
                sw.WriteLine("  {0}", v);
            foreach(Edge edge in e.Vertex.Edges)
                sw.WriteLine("  {0}", edge);
            e.VertexFormatter.Label = this.Graphviz.Escape(sw.ToString());
        }

        void Graphviz_FormatEdge(object sender, FormatEdgeEventArgs<Graph, CondensatedEdge<Vertex, Edge, Graph>> e)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("{0}", e.Edge.Edges.Count);
            foreach (Edge edge in e.Edge.Edges)
                sw.WriteLine("  {0}", edge);
            e.EdgeFormatter.Label.Value = this.Graphviz.Escape(sw.ToString());
        }
    }
}
