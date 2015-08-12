using System;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public abstract class GraphRendererBase<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private GraphvizAlgorithm<TVertex, TEdge> graphviz;

        public GraphRendererBase(
            IEdgeListGraph<TVertex, TEdge> visitedGraph)
        {
            this.graphviz = new GraphvizAlgorithm<TVertex, TEdge>(visitedGraph);
            this.Initialize();
        }

        protected virtual void Initialize()        
        {
            this.graphviz.CommonVertexFormat.Style = GraphvizVertexStyle.Filled;
            this.graphviz.CommonVertexFormat.FillColor = GraphvizColor.LightYellow;
            this.graphviz.CommonVertexFormat.Font = new GraphvizFont("Tahoma", 8.25F);
            this.graphviz.CommonVertexFormat.Shape = GraphvizVertexShape.Box;

            this.graphviz.CommonEdgeFormat.Font = new GraphvizFont("Tahoma", 8.25F);
        }

        public GraphvizAlgorithm<TVertex, TEdge> Graphviz
        {
            get { return this.graphviz; }
        }

        public IEdgeListGraph<TVertex, TEdge> VisitedGraph
        {
            get { return this.graphviz.VisitedGraph; }
        }

        public string Generate(IDotEngine dot, string fileName)
        {
            return this.graphviz.Generate(dot, fileName);
        }
    }
}
