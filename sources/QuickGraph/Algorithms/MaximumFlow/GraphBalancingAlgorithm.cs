using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [Serializable]
    public class GraphBalancerAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IMutableBidirectionalGraph<Vertex,Edge> visitedGraph;
        private IVertexFactory<Vertex> vertexFactory;
        private IEdgeFactory<Vertex,Edge> edgeFactory;

        private Vertex source;
        private Vertex sink;

        private Vertex balancingSource;
        private Edge balancingSourceEdge;

        private Vertex balancingSink;
        private Edge balancingSinkEdge;

        private IDictionary<Edge,double> capacities = new Dictionary<Edge,double>();
        private Dictionary<Edge,int> preFlow = new Dictionary<Edge,int>();
        private List<Vertex> surplusVertices = new List<Vertex>();
        private List<Edge> surplusEdges = new List<Edge>();
        private List<Vertex> deficientVertices = new List<Vertex>();
        private List<Edge> deficientEdges = new List<Edge>();
        private bool balanced = false;

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<Vertex, Edge> visitedGraph,
            Vertex source,
            Vertex sink
            )
            : this(visitedGraph,
                source,
                sink,
                FactoryCompiler.GetVertexFactory<Vertex>(),
                FactoryCompiler.GetEdgeFactory<Vertex, Edge>()
                )
        { }

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<Vertex, Edge> visitedGraph,
            Vertex source,
            Vertex sink,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory
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
            foreach (Edge edge in this.VisitedGraph.Edges)
                this.capacities.Add(edge, double.MaxValue);

            // setting preflow = l(e) = 1
            foreach (Edge edge in this.VisitedGraph.Edges)
                this.preFlow.Add(edge, 1);
        }

        public GraphBalancerAlgorithm(
            IMutableBidirectionalGraph<Vertex, Edge> visitedGraph,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory,
            Vertex source,
            Vertex sink,
            IDictionary<Edge,double> capacities)
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
            foreach (Edge edge in this.VisitedGraph.Edges)
                this.preFlow.Add(edge, 1);
        }

        public IMutableBidirectionalGraph<Vertex, Edge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public IVertexFactory<Vertex> VertexFactory
        {
            get { return this.vertexFactory;}
        }

        public IEdgeFactory<Vertex,Edge> EdgeFactory
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

        public Vertex Source
        {
            get
            {
                return this.source;
            }
        }
        public Vertex Sink
        {
            get
            {
                return this.sink;
            }
        }
        public Vertex BalancingSource
        {
            get
            {
                return this.balancingSource;
            }
        }
        public Edge BalancingSourceEdge
        {
            get
            {
                return this.balancingSourceEdge;
            }
        }
        public Vertex BalancingSink
        {
            get
            {
                return this.balancingSink;
            }
        }
        public Edge BalancingSinkEdge
        {
            get
            {
                return this.balancingSinkEdge;
            }
        }
        public ICollection<Vertex> SurplusVertices
        {
            get
            {
                return this.surplusVertices;
            }
        }
        public ICollection<Edge> SurplusEdges
        {
            get
            {
                return this.surplusEdges;
            }
        }
        public ICollection<Vertex> DeficientVertices
        {
            get
            {
                return this.deficientVertices;
            }
        }
        public ICollection<Edge> DeficientEdges
        {
            get
            {
                return this.deficientEdges;
            }
        }
        public IDictionary<Edge,double> Capacities
        {
            get
            {
                return this.capacities;
            }
        }

        public event VertexEventHandler<Vertex> BalancingSourceAdded;
        private void OnBalancingSourceAdded()
        {
            if (this.BalancingSourceAdded != null)
                this.BalancingSourceAdded(this, new VertexEventArgs<Vertex>(source));
        }
        public event VertexEventHandler<Vertex> BalancingSinkAdded;
        private void OnBalancingSinkAdded()
        {
            if (this.BalancingSinkAdded != null)
                this.BalancingSinkAdded(this, new VertexEventArgs<Vertex>(this.sink));
        }
        public event EdgeEventHandler<Vertex,Edge> EdgeAdded;
        private void OnEdgeAdded(Edge edge)
        {
            if (this.EdgeAdded != null)
                this.EdgeAdded(this, new EdgeEventArgs<Vertex,Edge>(edge));
        }
        public event VertexEventHandler<Vertex> SurplusVertexAdded;
        private void OnSurplusVertexAdded(Vertex vertex)
        {
            if (this.SurplusVertexAdded != null)
                this.SurplusVertexAdded(this, new VertexEventArgs<Vertex>(vertex));
        }
        public event VertexEventHandler<Vertex> DeficientVertexAdded;
        private void OnDeficientVertexAdded(Vertex vertex)
        {
            if (this.DeficientVertexAdded != null)
                this.DeficientVertexAdded(this, new VertexEventArgs<Vertex>(vertex));
        }

        public int GetBalancingIndex(Vertex v)
        {
            int bi = 0;
            foreach (Edge edge in this.VisitedGraph.OutEdges(v))
            {
                int pf = this.preFlow[edge];
                bi += pf;
            }
            foreach (Edge edge in this.VisitedGraph.InEdges(v))
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
            foreach (Vertex v in this.VisitedGraph.Vertices)
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
                    Edge edge = this.EdgeFactory.CreateEdge(this.BalancingSource, v);
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
                    Edge edge = this.EdgeFactory.CreateEdge(v, this.BalancingSink);
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
            foreach (Edge edge in this.surplusEdges)
            {
                this.VisitedGraph.RemoveEdge(edge);
                this.capacities.Remove(edge);
                this.preFlow.Remove(edge);
            }
            foreach (Edge edge in this.deficientEdges)
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

            this.balancingSource = default(Vertex);
            this.balancingSink = default(Vertex);
            this.balancingSourceEdge = default(Edge);
            this.balancingSinkEdge = default(Edge);

            this.surplusEdges.Clear();
            this.deficientEdges.Clear();
            this.surplusVertices.Clear();
            this.deficientVertices.Clear();

            this.balanced = false;
        }
    }
}
