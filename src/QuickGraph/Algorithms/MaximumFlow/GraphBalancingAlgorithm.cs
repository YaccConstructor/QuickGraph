using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.MaximumFlow
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class GraphBalancerAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IMutableBidirectionalGraph<TVertex,TEdge> visitedGraph;
        private VertexFactory<TVertex> vertexFactory;
        private EdgeFactory<TVertex,TEdge> edgeFactory;

        private TVertex source;
        private TVertex sink;

        private TVertex balancingSource;
        private TEdge balancingSourceEdge;

        private TVertex balancingSink;
        private TEdge balancingSinkEdge;

        private IDictionary<TEdge,double> capacities = new Dictionary<TEdge,double>();
        private Dictionary<TEdge,int> preFlow = new Dictionary<TEdge,int>();
        private List<TVertex> surplusVertices = new List<TVertex>();
        private List<TEdge> surplusEdges = new List<TEdge>();
        private List<TVertex> deficientVertices = new List<TVertex>();
        private List<TEdge> deficientEdges = new List<TEdge>();
        private bool balanced = false;

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            TVertex source,
            TVertex sink,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory
            )
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);
            Contract.Requires(source != null);
            Contract.Requires(visitedGraph.ContainsVertex(source));
            Contract.Requires(sink != null);
            Contract.Requires(visitedGraph.ContainsVertex(sink));

            this.visitedGraph = visitedGraph;
            this.vertexFactory = vertexFactory;
            this.edgeFactory = edgeFactory;
            this.source = source;
            this.sink = sink;

            // setting capacities = u(e) = +infty
            foreach (var edge in this.VisitedGraph.Edges)
                this.capacities.Add(edge, double.MaxValue);

            // setting preflow = l(e) = 1
            foreach (var edge in this.VisitedGraph.Edges)
                this.preFlow.Add(edge, 1);
        }

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory,
            TVertex source,
            TVertex sink,
            IDictionary<TEdge,double> capacities)
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);
            Contract.Requires(source != null);
            Contract.Requires(visitedGraph.ContainsVertex(source));
            Contract.Requires(sink != null);
            Contract.Requires(visitedGraph.ContainsVertex(sink));
            Contract.Requires(capacities != null);

            this.visitedGraph = visitedGraph;
            this.source = source;
            this.sink = sink;
            this.capacities = capacities;

            // setting preflow = l(e) = 1
            foreach (var edge in this.VisitedGraph.Edges)
                this.preFlow.Add(edge, 1);
        }

        public IMutableBidirectionalGraph<TVertex, TEdge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public VertexFactory<TVertex> VertexFactory
        {
            get { return this.vertexFactory;}
        }

        public EdgeFactory<TVertex,TEdge> EdgeFactory
        {
            get { return this.edgeFactory;}
        }

        public bool Balanced
        {
            get
            {
                return this.balanced;
            }
        }

        public TVertex Source
        {
            get
            {
                return this.source;
            }
        }
        public TVertex Sink
        {
            get
            {
                return this.sink;
            }
        }
        public TVertex BalancingSource
        {
            get
            {
                return this.balancingSource;
            }
        }
        public TEdge BalancingSourceEdge
        {
            get
            {
                return this.balancingSourceEdge;
            }
        }
        public TVertex BalancingSink
        {
            get
            {
                return this.balancingSink;
            }
        }
        public TEdge BalancingSinkEdge
        {
            get
            {
                return this.balancingSinkEdge;
            }
        }
        public ICollection<TVertex> SurplusVertices
        {
            get
            {
                return this.surplusVertices;
            }
        }
        public ICollection<TEdge> SurplusEdges
        {
            get
            {
                return this.surplusEdges;
            }
        }
        public ICollection<TVertex> DeficientVertices
        {
            get
            {
                return this.deficientVertices;
            }
        }
        public ICollection<TEdge> DeficientEdges
        {
            get
            {
                return this.deficientEdges;
            }
        }
        public IDictionary<TEdge,double> Capacities
        {
            get
            {
                return this.capacities;
            }
        }

        public event VertexAction<TVertex> BalancingSourceAdded;
        private void OnBalancingSourceAdded()
        {
            var eh = this.BalancingSourceAdded;
            if (eh != null)
                eh(this.source);
        }
        public event VertexAction<TVertex> BalancingSinkAdded;
        private void OnBalancingSinkAdded()
        {
            var eh = this.BalancingSinkAdded;
            if (eh != null)
                eh(this.sink);
        }
        public event EdgeAction<TVertex,TEdge> EdgeAdded;
        private void OnEdgeAdded(TEdge edge)
        {
            Contract.Requires(edge != null);

            var eh = this.EdgeAdded;
            if (eh != null)
                eh(edge);
        }
        public event VertexAction<TVertex> SurplusVertexAdded;
        private void OnSurplusVertexAdded(TVertex vertex)
        {
            Contract.Requires(vertex != null);
            var eh = this.SurplusVertexAdded;
            if (eh != null)
                eh(vertex);
        }
        public event VertexAction<TVertex> DeficientVertexAdded;
        private void OnDeficientVertexAdded(TVertex vertex)
        {
            Contract.Requires(vertex != null);

            var eh = this.DeficientVertexAdded;
            if (eh != null)
                eh(vertex);
        }

        public int GetBalancingIndex(TVertex v)
        {
            Contract.Requires(v != null);

            int bi = 0;
            foreach (var edge in this.VisitedGraph.OutEdges(v))
            {
                int pf = this.preFlow[edge];
                bi += pf;
            }
            foreach (var edge in this.VisitedGraph.InEdges(v))
            {
                int pf = this.preFlow[edge];
                bi -= pf;
            }
            return bi;
        }

        public void Balance()
        {
            if (this.Balanced)
                throw new InvalidOperationException("Graph already balanced");

            // step 0
            // create new source, new sink
            this.balancingSource = this.VertexFactory();
            this.visitedGraph.AddVertex(this.balancingSource);
            this.OnBalancingSourceAdded();

            this.balancingSink = this.VertexFactory();
            this.visitedGraph.AddVertex(this.balancingSink);
            this.OnBalancingSinkAdded();

            // step 1
            this.balancingSourceEdge = this.EdgeFactory(this.BalancingSource, this.Source);
            this.VisitedGraph.AddEdge(this.BalancingSourceEdge);
            this.capacities.Add(this.balancingSourceEdge, double.MaxValue);
            this.preFlow.Add(this.balancingSourceEdge, 0);
            OnEdgeAdded(balancingSourceEdge);

            this.balancingSinkEdge = this.EdgeFactory(this.Sink, this.BalancingSink);
            this.VisitedGraph.AddEdge(this.balancingSinkEdge);
            this.capacities.Add(this.balancingSinkEdge, double.MaxValue);
            this.preFlow.Add(this.balancingSinkEdge, 0);
            OnEdgeAdded(balancingSinkEdge);

            // step 2
            // for each surplus vertex v, add (source -> v)
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (v.Equals(this.balancingSource))
                    continue;
                if (v.Equals(this.balancingSink))
                    continue;
                if (v.Equals(this.source))
                    continue;
                if (v.Equals(this.sink))
                    continue;

                int balacingIndex = this.GetBalancingIndex(v);
                if (balacingIndex == 0)
                    continue;

                if (balacingIndex < 0)
                {
                    // surplus vertex
                    TEdge edge = this.EdgeFactory(this.BalancingSource, v);
                    this.VisitedGraph.AddEdge(edge);
                    this.surplusEdges.Add(edge);
                    this.surplusVertices.Add(v);
                    this.preFlow.Add(edge, 0);
                    this.capacities.Add(edge, -balacingIndex);
                    OnSurplusVertexAdded(v);
                    OnEdgeAdded(edge);
                }
                else
                {
                    // deficient vertex
                    TEdge edge = this.EdgeFactory(v, this.BalancingSink);
                    this.deficientEdges.Add(edge);
                    this.deficientVertices.Add(v);
                    this.preFlow.Add(edge, 0);
                    this.capacities.Add(edge, balacingIndex);
                    OnDeficientVertexAdded(v);
                    OnEdgeAdded(edge);
                }
            }

            this.balanced = true;
        }

        public void UnBalance()
        {
            if (!this.Balanced)
                throw new InvalidOperationException("Graph is not balanced");
            foreach (var edge in this.surplusEdges)
            {
                this.VisitedGraph.RemoveEdge(edge);
                this.capacities.Remove(edge);
                this.preFlow.Remove(edge);
            }
            foreach (var edge in this.deficientEdges)
            {
                this.VisitedGraph.RemoveEdge(edge);
                this.capacities.Remove(edge);
                this.preFlow.Remove(edge);
            }

            this.capacities.Remove(this.BalancingSinkEdge);
            this.capacities.Remove(this.BalancingSourceEdge);
            this.preFlow.Remove(this.BalancingSinkEdge);
            this.preFlow.Remove(this.BalancingSourceEdge);
            this.VisitedGraph.RemoveEdge(this.BalancingSourceEdge);
            this.VisitedGraph.RemoveEdge(this.BalancingSinkEdge);
            this.VisitedGraph.RemoveVertex(this.BalancingSource);
            this.VisitedGraph.RemoveVertex(this.BalancingSink);

            this.balancingSource = default(TVertex);
            this.balancingSink = default(TVertex);
            this.balancingSourceEdge = default(TEdge);
            this.balancingSinkEdge = default(TEdge);

            this.surplusEdges.Clear();
            this.deficientEdges.Clear();
            this.surplusVertices.Clear();
            this.deficientVertices.Clear();

            this.balanced = false;
        }
    }
}
