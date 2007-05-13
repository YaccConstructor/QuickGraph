using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// A single-source shortest path algorithm for directed acyclic
    /// graph.
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     id="boost"
    ///     />
    [Serializable]
    public sealed class DagShortestPathAlgorithm<Vertex, Edge> :
        ShortestPathAlgorithmBase<Vertex,Edge,IVertexListGraph<Vertex,Edge>>,
        IVertexColorizerAlgorithm<Vertex,Edge>,
        ITreeBuilderAlgorithm<Vertex,Edge>,
        IDistanceRecorderAlgorithm<Vertex,Edge>,
        IVertexPredecessorRecorderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        public DagShortestPathAlgorithm(
            IVertexListGraph<Vertex, Edge> g,
            IDictionary<Edge, double> weights
            )
            : this(g, weights, new ShortestDistanceRelaxer())
        { }

        public DagShortestPathAlgorithm(
            IVertexListGraph<Vertex,Edge> g,
            IDictionary<Edge,double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(g,weights, distanceRelaxer)
        {}

        public event VertexEventHandler<Vertex> InitializeVertex;
        private void OnInitializeVertex(Vertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> StartVertex;
        private void OnStartVertex(Vertex v)
        {
            VertexEventHandler<Vertex> eh = this.StartVertex;
            if (eh!=null)
                eh(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> DiscoverVertex;
        private void OnDiscoverVertex(Vertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> ExamineVertex;
        private void OnExamineVertex(Vertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex,Edge> ExamineEdge;
        private void OnExamineEdge(Edge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event EdgeEventHandler<Vertex,Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event EdgeEventHandler<Vertex,Edge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(Edge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event VertexEventHandler<Vertex> FinishVertex;
        private void OnFinishVertex(Vertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public void Initialize()
        {
            this.VertexColors.Clear();
            this.Distances.Clear();

            // init color, distance
            foreach (Vertex u in VisitedGraph.Vertices)
            {
                this.VertexColors[u] = GraphColor.White;
                this.Distances[u] = double.MaxValue;
                this.OnInitializeVertex(u);
            }
        }
        
        protected override void  InternalCompute()
        {
            this.Initialize();
            double initialDistance = this.DistanceRelaxer.InitialDistance;
            VertexColors[this.RootVertex] = GraphColor.Gray;
            Distances[this.RootVertex] = initialDistance;
            ComputeNoInit(this.RootVertex);
        }

        public void ComputeNoInit(Vertex s)
        {
            ICollection<Vertex> orderedVertices = AlgoUtility.TopologicalSort<Vertex, Edge>(this.VisitedGraph);

            OnDiscoverVertex(s);
            foreach (Vertex v in orderedVertices)
            {
                OnExamineVertex(v);
                foreach (Edge e in VisitedGraph.OutEdges(v))
                {
                    OnDiscoverVertex(e.Target);
                    bool decreased = Relax(e);
                    if (decreased)
                        OnTreeEdge(e);
                    else
                        OnEdgeNotRelaxed(e);
                }
                OnFinishVertex(v);
            }
        }

        private bool Relax(Edge e)
        {
            double du = this.Distances[e.Source];
            double dv = this.Distances[e.Target];
            double we = this.Weights[e];

            if (Compare(Combine(du, we), dv))
            {
                Distances[e.Target] = Combine(du, we);
                return true;
            }
            else
                return false;
        }
    }
}
