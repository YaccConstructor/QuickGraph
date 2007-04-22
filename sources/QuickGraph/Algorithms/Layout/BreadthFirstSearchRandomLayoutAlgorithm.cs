using System;
using System.Collections.Generic;
using System.Drawing;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.Layout
{
    public sealed class BreadthFirstSearchRandomLayoutAlgorithm<Vertex,Edge,Graph>: 
        RandomLayoutAlgorithmBase<Vertex,Edge,Graph>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        private BreadthFirstSearchAlgorithm<Vertex, Edge> bfs;
        private VertexPredecessorRecorderObserver<Vertex, Edge> observer;
        private float vertexDistance = 50;

        public BreadthFirstSearchRandomLayoutAlgorithm(Graph visitedGraph, Dictionary<Vertex, PointF> vertexPositions)
            : base(visitedGraph, vertexPositions)
        { }

        public float VertexDistance
        {
            get { return this.vertexDistance; }
            set { this.vertexDistance = value; }
        }

        protected override void InternalCompute()
        {
            this.bfs = new BreadthFirstSearchAlgorithm<Vertex, Edge>(this.VisitedGraph);
            this.observer = new VertexPredecessorRecorderObserver<Vertex, Edge>();

            try
            {
                this.observer.Attach(this.bfs);
                this.bfs.DiscoverVertex += new VertexEventHandler<Vertex>(bfs_DiscoverVertex);
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
                    this.bfs.DiscoverVertex -= new VertexEventHandler<Vertex>(bfs_DiscoverVertex);
                    this.bfs = null;
                }
            }
        }

        void bfs_DiscoverVertex(object sender, VertexEventArgs<Vertex> e)
        {
            Edge parent;
            if (this.observer.VertexPredecessors.TryGetValue(e.Vertex, out parent))
            {
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
