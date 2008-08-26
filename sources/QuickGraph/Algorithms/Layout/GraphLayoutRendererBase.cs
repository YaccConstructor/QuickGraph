using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class GraphLayoutRendererBase<TVertex,TEdge,TGraph,TLayoutAlgorithm>
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
        where TLayoutAlgorithm : LayoutAlgorithmBase<TVertex,TEdge,TGraph>
    {
        private readonly object syncRoot = new object();
        private TLayoutAlgorithm algorithm;
        private bool enabled = true;
        private bool renderIntermediateIterations = true;

        public GraphLayoutRendererBase(TLayoutAlgorithm algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            this.algorithm = algorithm;
        }

        public TLayoutAlgorithm Algorithm
        {
            get { return this.algorithm; }
        }

        public bool Enabled
        {
            get 
            { 
                lock(syncRoot)
                {
                    return this.enabled; 
                }
            }
        }

        public bool RenderIntermediateIterations
        {
            get 
            {
                lock (syncRoot)
                {
                    return this.renderIntermediateIterations;
                }
            }
            set 
            {
                lock (syncRoot)
                {
                    this.renderIntermediateIterations = value;
                }
            }
        }

        public void Start()
        {
            lock (syncRoot)
            {
                if (!this.enabled)
                {
                    this.algorithm.IterationEnded += new EventHandler(algorithm_IterationEnded);
                    this.enabled = true;
                }
            }
        }

        public void Stop()
        {
            lock (syncRoot)
            {
                if (this.enabled)
                {
                    this.algorithm.IterationEnded -= new EventHandler(algorithm_IterationEnded);
                    this.enabled = false;
                }
            }
        }

        void algorithm_IterationEnded(object sender, EventArgs e)
        {
            this.Render();
        }

        public virtual void Render()
        {
            // we have to lock the vertex position dictionary
            // and copy it to a list
            Dictionary<TVertex, PointF> vertexPositions;
            lock (this.Algorithm.SyncRoot)
            {
                vertexPositions = new Dictionary<TVertex, PointF>(this.Algorithm.VertexPositions);
            }

            this.PreRender();

            // paint vertices
            foreach (var v in this.Algorithm.VisitedGraph.Vertices)
            {
                    // get position
                    PointF position = vertexPositions[v];
                    // paint vertex
                    DrawVertex(v,position);
            }

            foreach (var v in this.Algorithm.VisitedGraph.Vertices)
            {
                // get position
                PointF source = vertexPositions[v];
                // paint vertex
                foreach (var edge in this.Algorithm.VisitedGraph.OutEdges(v))
                {
                    PointF target = vertexPositions[edge.Target];
                    // draw edge
                    DrawEdge(edge, source, target);
                }
            }
            this.PostRender();
        }

        protected abstract void PreRender();
        protected abstract void PostRender();
        protected abstract void DrawVertex(TVertex vertex, PointF position);
        protected abstract void DrawEdge(TEdge edge, PointF sourcePosition, PointF targetPosition);
    }
}
