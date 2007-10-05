using System;
using System.Collections.Generic;
using System.Drawing;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using System.Diagnostics;

namespace QuickGraph.Algorithms.Layout
{
    public sealed class BreadthFirstSearchRandomLayoutAlgorithm<TVertex,TEdge,TGraph>: 
        RandomLayoutAlgorithmBase<TVertex,TEdge,TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {
        private BreadthFirstSearchAlgorithm<TVertex, TEdge> bfs;
        private VertexPredecessorRecorderObserver<TVertex, TEdge> observer;
        private float vertexDistance = 50;

        public BreadthFirstSearchRandomLayoutAlgorithm(TGraph visitedGraph, Dictionary<TVertex, PointF> vertexPositions)
            : base(visitedGraph, vertexPositions)
        { }

        public float VertexDistance
        {
            get { return this.vertexDistance; }
            set { this.vertexDistance = value; }
        }

        protected override void InternalCompute()
        {
            this.bfs = new BreadthFirstSearchAlgorithm<TVertex, TEdge>(this.VisitedGraph);
            this.observer = new VertexPredecessorRecorderObserver<TVertex, TEdge>();

            try
            {
                this.observer.Attach(this.bfs);
                this.bfs.DiscoverVertex += new VertexEventHandler<TVertex>(bfs_DiscoverVertex);
                bfs.Compute();
                this.OnIterationEnded(EventArgs.Empty);
            }
            finally
            {
                if (this.observer != null)
                {
                    this.observer.Detach(this.bfs);
                    this.observer = null;
                }
                if (this.bfs != null)
                {
                    this.bfs.DiscoverVertex -= new VertexEventHandler<TVertex>(bfs_DiscoverVertex);
                    this.bfs = null;
                }
            }
        }

        void bfs_DiscoverVertex(object sender, VertexEventArgs<TVertex> e)
        {
            TEdge parent;
            if (this.observer.VertexPredecessors.TryGetValue(e.Vertex, out parent))
            {
                Debug.Assert(parent != null);
                float angle = (float)(this.Rnd.NextDouble() * Math.PI * 2);
                PointF parentPosition = this.VertexPositions[parent.Source];
                PointF position = new PointF(
                    (float)(parentPosition.X + Math.Cos(angle) * this.VertexDistance),
                    (float)(parentPosition.Y + Math.Cos(angle) * this.VertexDistance)
                    );
                this.VertexPositions[e.Vertex] = position;
            }
            else
            {
                float x = this.BoundingBox.Left+ (float)this.Rnd.NextDouble() * this.BoundingBox.Width;
                float y = this.BoundingBox.Left + (float)this.Rnd.NextDouble() * this.BoundingBox.Width;
                this.VertexPositions[e.Vertex] = new PointF(x,y);
            }
        }
    }
}
