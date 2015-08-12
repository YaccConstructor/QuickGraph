using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public abstract class GraphAugmentorAlgorithmBase<TVertex,TEdge,TGraph> 
        : AlgorithmBase<TGraph>
        , IDisposable
        where TEdge : IEdge<TVertex>
        where TGraph : IMutableVertexAndEdgeSet<TVertex, TEdge>
    {
        private bool augmented = false;
        private List<TEdge> augmentedEdges = new List<TEdge>();
        private readonly VertexFactory<TVertex> vertexFactory;
        private readonly EdgeFactory<TVertex, TEdge> edgeFactory;

        private TVertex superSource = default(TVertex);
        private TVertex superSink = default(TVertex);

        protected GraphAugmentorAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory
            )
            :base(host, visitedGraph)
        {
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            this.vertexFactory = vertexFactory;
            this.edgeFactory = edgeFactory;
        }

        public VertexFactory<TVertex> VertexFactory
        {
            get { return this.vertexFactory; }
        }

        public EdgeFactory<TVertex, TEdge> EdgeFactory
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

        public event VertexAction<TVertex> SuperSourceAdded;
        private void OnSuperSourceAdded(TVertex v)
        {
            Contract.Requires(v != null);
            var eh = this.SuperSourceAdded;
            if (eh != null)
                eh(v);
        }

        public event VertexAction<TVertex> SuperSinkAdded;
        private void OnSuperSinkAdded(TVertex v)
        {
            Contract.Requires(v != null);
            var eh = this.SuperSinkAdded;
            if (eh != null)
                eh(v);
        }

        public event EdgeAction<TVertex, TEdge> EdgeAdded;
        private void OnEdgeAdded(TEdge e)
        {
            Contract.Requires(e != null);
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(e);
        }


        protected override void InternalCompute()
        {
            if (this.Augmented)
                throw new InvalidOperationException("Graph already augmented");

            this.superSource = this.VertexFactory();
            this.VisitedGraph.AddVertex(this.superSource);
            this.OnSuperSourceAdded(this.SuperSource);

            this.superSink = this.VertexFactory();
            this.VisitedGraph.AddVertex(this.superSink);
            this.OnSuperSinkAdded(this.SuperSink);

            this.AugmentGraph();
            this.augmented = true;
        }

        public virtual void Rollback()
        {
            if (!this.Augmented)
                return;

            this.augmented = false;
            this.VisitedGraph.RemoveVertex(this.SuperSource);
            this.VisitedGraph.RemoveVertex(this.SuperSink);
            this.superSource = default(TVertex);
            this.superSink = default(TVertex);
            this.augmentedEdges.Clear();
        }

        public void Dispose()
        {
            this.Rollback();
        }

        protected abstract void AugmentGraph();

        protected void AddAugmentedEdge(TVertex source, TVertex target)
        {
            TEdge edge = this.EdgeFactory(source, target);
            this.augmentedEdges.Add(edge);
            this.VisitedGraph.AddEdge(edge);
            this.OnEdgeAdded(edge);
        }
    }
}
