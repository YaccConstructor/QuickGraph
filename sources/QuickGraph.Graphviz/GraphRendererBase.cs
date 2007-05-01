using System;
using System.Drawing;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public abstract class GraphRendererBase<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private GraphvizAlgorithm<Vertex, Edge> graphviz;

        public GraphRendererBase(
            IDotEngine dot,
            IVertexAndEdgeListGraph<Vertex,Edge> visitedGraph)
        {
            this.graphviz = new GraphvizAlgorithm<Vertex, Edge>(dot, visitedGraph);
            this.Initialize();
        }

        protected virtual void Initialize()        
        {
            this.graphviz.CommonVertexFormat.Style = GraphvizVertexStyle.Filled;
            this.graphviz.CommonVertexFormat.FillColor = System.Drawing.Color.LightYellow;
            this.graphviz.CommonVertexFormat.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.graphviz.CommonVertexFormat.Shape = GraphvizVertexShape.Box;

            this.graphviz.CommonEdgeFormat.Font = new System.Drawing.Font("Tahoma", 8.25F);
        }

        public GraphvizAlgorithm<Vertex, Edge> Graphviz
        {
            get { return this.graphviz; }
        }

        public IVertexAndEdgeListGraph<Vertex, Edge> VisitedGraph
        {
            get { return this.graphviz.VisitedGraph; }
        }

        public string Generate(string fileName)
        {
            return this.graphviz.Generate(fileName);
        }
    }
}
