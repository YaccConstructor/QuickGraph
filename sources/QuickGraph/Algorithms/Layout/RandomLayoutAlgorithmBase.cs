using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class RandomLayoutAlgorithmBase<Vertex, Edge, Graph> : LayoutAlgorithmBase<Vertex, Edge, Graph>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        private Random rnd = new Random();
        private RectangleF boundingBox;

        public RandomLayoutAlgorithmBase(Graph visitedGraph, Dictionary<Vertex, PointF> vertexPositions)
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
