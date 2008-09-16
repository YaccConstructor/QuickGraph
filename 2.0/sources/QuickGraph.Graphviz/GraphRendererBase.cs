using System;
using System.Drawing;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public abstract class GraphRendererBase<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private GraphvizAlgorithm<TVertex, TEdge> graphviz;

        public GraphRendererBase(
            IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
        {
            this.graphviz = new GraphvizAlgorithm<TVertex, TEdge>(visitedGraph);
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

        public GraphvizAlgorithm<TVertex, TEdge> Graphviz
        {
            get { return this.graphviz; }
        }

        public IVertexAndEdgeSet<TVertex, TEdge> VisitedGraph
        {
            get { return this.graphviz.VisitedGraph; }
        }

        public string Generate(IDotEngine dot, string fileName)
        {
            return this.graphviz.Generate(dot, fileName);
        }
    }
}
