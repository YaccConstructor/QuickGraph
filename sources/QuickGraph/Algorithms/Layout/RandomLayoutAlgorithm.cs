using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public sealed class RandomLayoutAlgorithm<Vertex,Edge,Graph> : RandomLayoutAlgorithmBase<Vertex,Edge,Graph>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        public RandomLayoutAlgorithm(Graph visitedGraph, Dictionary<Vertex,PointF> vertexPositions)
            : base(visitedGraph, vertexPositions)
        {}

        protected override void InternalCompute()
        {
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    break;

                float x = this.BoundingBox.Left+ (float)this.Rnd.NextDouble() * this.BoundingBox.Width;
                float y = this.BoundingBox.Top + (float)this.Rnd.NextDouble() * this.BoundingBox.Height;
                this.VertexPositions[v] = new PointF(x, y);
            }

            this.OnIterationEnded(EventArgs.Empty);
        }
    }
}
