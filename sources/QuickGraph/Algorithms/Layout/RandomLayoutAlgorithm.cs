using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public sealed class RandomLayoutAlgorithm<TVertex,TEdge,TGraph> : RandomLayoutAlgorithmBase<TVertex,TEdge,TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        public RandomLayoutAlgorithm(TGraph visitedGraph, Dictionary<TVertex,PointF> vertexPositions)
            : base(visitedGraph, vertexPositions)
        {}

        protected override void InternalCompute()
        {
            foreach (var v in this.VisitedGraph.Vertices)
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
