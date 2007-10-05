using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class LayoutAlgorithmBase<TVertex,TEdge,TGraph> : AlgorithmBase<TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex,TEdge>
    {
        private Dictionary<TVertex, PointF> vertexPositions;

        public LayoutAlgorithmBase(TGraph visitedGraph, Dictionary<TVertex,PointF> vertexPositions)
            : base(visitedGraph)
        {
            this.vertexPositions = vertexPositions;
        }

        public LayoutAlgorithmBase(TGraph visitedGraph)
            :this(visitedGraph, new Dictionary<TVertex, PointF>(visitedGraph.VertexCount))
        {
        }

        public Dictionary<TVertex, PointF> VertexPositions
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
