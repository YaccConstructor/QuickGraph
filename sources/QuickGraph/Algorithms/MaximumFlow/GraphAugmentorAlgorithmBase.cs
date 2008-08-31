using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public abstract class GraphAugmentorAlgorithmBase<TVertex,TEdge,TGraph> :
        AlgorithmBase<TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IMutableVertexAndEdgeListGraph<TVertex,TEdge>
    {
        private bool augmented = false;
        private List<TEdge> augmentedEdges = new List<TEdge>();
        private IVertexFactory<TVertex> vertexFactory;
        private IEdgeFactory<TVertex, TEdge> edgeFactory;

        private TVertex superSource = default(TVertex);
        private TVertex superSink = default(TVertex);

        protected GraphAugmentorAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory
            )
            :base(host, visitedGraph)
        {
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            this.vertexFactory = vertexFactory;
            this.edgeFactory = edgeFactory;
        }

        public IVertexFactory<TVertex> VertexFactory
        {
            get { return this.vertexFactory; }
        }

        public IEdgeFactory<TVertex, TEdge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public TVertex SuperSource
        {
            get { return this.superSource; }
        }

        public TVertex SuperSink
        {
            get { return this.superSink; }
        }

        public bool Augmented
        {
            get { return this.augmented; }
        }

        public ICollection<TEdge> AugmentedEdges
        {
            get { return this.augmentedEdges; }
        }

        public event VertexEventHandler<TVertex> SuperSourceAdded;
        private void OnSuperSourceAdded(TVertex v)
        {
            if (this.SuperSourceAdded != null)
                this.SuperSourceAdded(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> SuperSinkAdded;
        private void OnSuperSinkAdded(TVertex v)
        {
            if (this.SuperSinkAdded != null)
                this.SuperSinkAdded(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex, TEdge> EdgeAdded;
        private void OnEdgeAdded(TEdge e)
        {
            if (this.EdgeAdded != null)
                this.EdgeAdded(this, new EdgeEventArgs<TVertex, TEdge>(e));
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
            this.superSource = default(TVertex);
            this.superSink = default(TVertex);
            this.augmentedEdges.Clear();
            this.augmented = false;
        }

        protected abstract void AugmentGraph();

        protected void AddAugmentedEdge(TVertex source, TVertex target)
        {
            TEdge edge = this.EdgeFactory.CreateEdge(source, target);
            this.augmentedEdges.Add(edge);
            this.VisitedGraph.AddEdge(edge);
            this.OnEdgeAdded(edge);
        }
    }
}
