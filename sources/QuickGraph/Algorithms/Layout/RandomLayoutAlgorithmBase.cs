using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class RandomLayoutAlgorithmBase<TVertex, TEdge, TGraph> : LayoutAlgorithmBase<TVertex, TEdge, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        private Random rnd = new Random();
        private RectangleF boundingBox;

        public RandomLayoutAlgorithmBase(TGraph visitedGraph, Dictionary<TVertex, PointF> vertexPositions)
            : base(visitedGraph, vertexPositions)
        {
            this.boundingBox = new RectangleF(0, 0, VisitedGraph.VertexCount, VisitedGraph.VertexCount);
        }

        public Random Rnd
        {
            get { return this.rnd; }
            set { this.rnd = value; }
        }

        public RectangleF BoundingBox
        {
            get { return this.boundingBox; }
            set { this.boundingBox = value; }
        }
    }
}
