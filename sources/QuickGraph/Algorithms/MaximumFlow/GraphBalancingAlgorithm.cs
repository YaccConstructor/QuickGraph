using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [Serializable]
    public class GraphBalancerAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IMutableBidirectionalGraph<TVertex,TEdge> visitedGraph;
        private IVertexFactory<TVertex> vertexFactory;
        private IEdgeFactory<TVertex,TEdge> edgeFactory;

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
            TVertex sink
            )
            : this(visitedGraph,
                source,
                sink,
                FactoryCompiler.GetVertexFactory<TVertex>(),
                FactoryCompiler.GetEdgeFactory<TVertex, TEdge>()
                )
        { }

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            TVertex source,
            TVertex sink,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory
            )
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (vertexFactory==null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory==null)
                throw new ArgumentNullException("edgeFactory");
            if (source == null)
                throw new ArgumentNullException("source");
            if (!visitedGraph.ContainsVertex(source))
                throw new ArgumentException("source is not part of the graph");
            if (sink == null)
                throw new ArgumentNullException("sink");
            if (!visitedGraph.ContainsVertex(sink))
                throw new ArgumentException("sink is not part of the graph");

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
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory,
            TVertex source,
            TVertex sink,
            IDictionary<TEdge,double> capacities)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (vertexFactory==null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory==null)
                throw new ArgumentNullException("edgeFactory");
            if (source == null)
                throw new ArgumentNullException("source");
            if (!visitedGraph.ContainsVertex(source))
                throw new ArgumentException("source is not part of the graph");
            if (sink == null)
                throw new ArgumentNullException("sink");
            if (!visitedGraph.ContainsVertex(sink))
                throw new ArgumentException("sink is not part of the graph");
            if (capacities == null)
                throw new ArgumentNullException("capacities");

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

        public IVertexFactory<TVertex> VertexFactory
        {
            get { return this.vertexFactory;}
        }

        public IEdgeFactory<TVertex,TEdge> EdgeFactory
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

        public event VertexEventHandler<TVertex> BalancingSourceAdded;
        private void OnBalancingSourceAdded()
        {
            if (this.BalancingSourceAdded != null)
                this.BalancingSourceAdded(this, new VertexEventArgs<TVertex>(source));
        }
        public event VertexEventHandler<TVertex> BalancingSinkAdded;
        private void OnBalancingSinkAdded()
        {
            if (this.BalancingSinkAdded != null)
                this.BalancingSinkAdded(this, new VertexEventArgs<TVertex>(this.sink));
        }
        public event EdgeEventHandler<TVertex,TEdge> EdgeAdded;
        private void OnEdgeAdded(TEdge edge)
        {
            if (this.EdgeAdded != null)
                this.EdgeAdded(this, new EdgeEventArgs<TVertex,TEdge>(edge));
        }
        public event VertexEventHandler<TVertex> SurplusVertexAdded;
        private void OnSurplusVertexAdded(TVertex vertex)
        {
            if (this.SurplusVertexAdded != null)
                this.SurplusVertexAdded(this, new VertexEventArgs<TVertex>(vertex));
        }
        public event VertexEventHandler<TVertex> DeficientVertexAdded;
        private void OnDeficientVertexAdded(TVertex vertex)
        {
            if (this.DeficientVertexAdded != null)
                this.DeficientVertexAdded(this, new VertexEventArgs<TVertex>(vertex));
        }

        public int GetBalancingIndex(TVertex v)
        {
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
            this.balancingSource = this.VertexFactory.CreateVertex();
            this.visitedGraph.AddVertex(this.balancingSource);
            this.OnBalancingSourceAdded();

            this.balancingSink = this.VertexFactory.CreateVertex();
            this.visitedGraph.AddVertex(this.balancingSink);
            this.OnBalancingSinkAdded();

            // step 1
            this.balancingSourceEdge = this.EdgeFactory.CreateEdge(this.BalancingSource, this.Source);
            this.VisitedGraph.AddEdge(this.BalancingSourceEdge);
            this.capacities.Add(this.balancingSourceEdge, double.MaxValue);
            this.preFlow.Add(this.balancingSourceEdge, 0);
            OnEdgeAdded(balancingSourceEdge);

            this.balancingSinkEdge = this.EdgeFactory.CreateEdge(this.Sink, this.BalancingSink);
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
                    TEdge edge = this.EdgeFactory.CreateEdge(this.BalancingSource, v);
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
                    TEdge edge = this.EdgeFactory.CreateEdge(v, this.BalancingSink);
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
