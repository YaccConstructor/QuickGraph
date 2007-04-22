using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public abstract class GraphAugmentorAlgorithmBase<Vertex,Edge,Graph> :
        AlgorithmBase<Graph>
        where Edge : IEdge<Vertex>
        where Graph : IMutableVertexAndEdgeListGraph<Vertex,Edge>
    {
        private bool augmented = false;
        private List<Edge> augmentedEdges = new List<Edge>();
        private IVertexFactory<Vertex> vertexFactory;
        private IEdgeFactory<Vertex, Edge> edgeFactory;

        private Vertex superSource = default(Vertex);
        private Vertex superSink = default(Vertex);

        public GraphAugmentorAlgorithmBase(
            Graph visitedGraph,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory
            )
            :base(visitedGraph)
        {
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            this.vertexFactory = vertexFactory;
            this.edgeFactory = edgeFactory;
        }

        public IVertexFactory<Vertex> VertexFactory
        {
            get { return this.vertexFactory; }
        }

        public IEdgeFactory<Vertex, Edge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public Vertex SuperSource
        {
            get { return this.superSource; }
        }

        public Vertex SuperSink
        {
            get { return this.superSink; }
        }

        public bool Augmented
        {
            get { return this.augmented; }
        }

        public ICollection<Edge> AugmentedEdges
        {
            get { return this.augmentedEdges; }
        }

        public event VertexEventHandler<Vertex> SuperSourceAdded;
        private void OnSuperSourceAdded(Vertex v)
        {
            if (this.SuperSourceAdded != null)
                this.SuperSourceAdded(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> SuperSinkAdded;
        private void OnSuperSinkAdded(Vertex v)
        {
            if (this.SuperSinkAdded != null)
                this.SuperSinkAdded(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex, Edge> EdgeAdded;
        private void OnEdgeAdded(Edge e)
        {
            if (this.EdgeAdded != null)
                this.EdgeAdded(this, new EdgeEventArgs<Vertex, Edge>(e));
        }


        protected override void InternalCompute()
        {
            if (this.Augmented)
                throw new InvalidOperationException("Graph already augmented");

            this.superSource = this.VertexFactory.CreateVertex();
            this.VisitedGraph.AddVertex(this.superSource);
            this.OnSuperSourceAdded(this.SuperSource);

            this.superSink = this.VertexFactory.CreateVertex();
            this.VisitedGraph.AddVertex(this.superSink);
            this.OnSuperSinkAdded(this.SuperSink);

            this.AugmentGraph();
            this.augmented = true;
        }

        public virtual void Rollback()
        {
            if (!this.Augmented)
                return;

            this.VisitedGraph.RemoveVertex(this.SuperSource);
            this.VisitedGraph.RemoveVertex(this.SuperSink);
            this.superSource = default(Vertex);
            this.superSink = default(Vertex);
            this.augmentedEdges.Clear();
            this.augmented = false;
        }

        protected abstract void AugmentGraph();

        protected void AddAugmentedEdge(Vertex source, Vertex target)
        {
            Edge edge = this.EdgeFactory.CreateEdge(source, target);
            this.augmentedEdges.Add(edge);
            this.VisitedGraph.AddEdge(edge);
            this.OnEdgeAdded(edge);
        }
    }
}
