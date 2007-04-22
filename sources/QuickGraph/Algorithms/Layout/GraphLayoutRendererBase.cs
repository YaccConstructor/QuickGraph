using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public abstract class GraphLayoutRendererBase<Vertex,Edge,Graph,LayoutAlgorithm>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
        where LayoutAlgorithm : LayoutAlgorithmBase<Vertex,Edge,Graph>
    {
        private readonly object syncRoot = new object();
        private LayoutAlgorithm algorithm;
        private bool enabled = true;
        private bool renderIntermediateIterations = true;

        public GraphLayoutRendererBase(LayoutAlgorithm algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            this.algorithm = algorithm;
        }

        public LayoutAlgorithm Algorithm
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
            Dictionary<Vertex, PointF> vertexPositions;
            lock (this.Algorithm.SyncRoot)
            {
                vertexPositions = new Dictionary<Vertex, PointF>(this.Algorithm.VertexPositions);
            }

            this.PreRender();

            // paint vertices
            foreach (Vertex v in this.Algorithm.VisitedGraph.Vertices)
            {
                    // get position
                    PointF position = vertexPositions[v];
                    // paint vertex
                    DrawVertex(v,position);
            }

            foreach (Vertex v in this.Algorithm.VisitedGraph.Vertices)
            {
                // get position
                PointF source = vertexPositions[v];
                // paint vertex
                foreach (Edge edge in this.Algorithm.VisitedGraph.OutEdges(v))
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
        protected abstract void DrawVertex(Vertex vertex, PointF position);
        protected abstract void DrawEdge(Edge edge, PointF sourcePosition, PointF targetPosition);
    }
}
