using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class LayoutAlgorithmBase<Vertex,Edge,Graph> : AlgorithmBase<Graph>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex,Edge>
    {
        private Dictionary<Vertex, PointF> vertexPositions;

        public LayoutAlgorithmBase(Graph visitedGraph, Dictionary<Vertex,PointF> vertexPositions)
            : base(visitedGraph)
        {
            this.vertexPositions = vertexPositions;
        }

        public LayoutAlgorithmBase(Graph visitedGraph)
            :this(visitedGraph, new Dictionary<Vertex, PointF>(visitedGraph.VertexCount))
        {
        }

        public Dictionary<Vertex, PointF> VertexPositions
        {
            get { return this.vertexPositions; }
        }

        public event EventHandler IterationEnded;

        protected virtual void OnIterationEnded(EventArgs args)
        {
            EventHandler eh = this.IterationEnded;
            if (eh != null)
                eh(this, args);
        }
    }
}
