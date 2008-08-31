using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;

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
    public sealed class DagShortestPathAlgorithm<TVertex, TEdge> :
        ShortestPathAlgorithmBase<TVertex,TEdge,IVertexListGraph<TVertex,TEdge>>,
        IVertexColorizerAlgorithm<TVertex,TEdge>,
        ITreeBuilderAlgorithm<TVertex,TEdge>,
        IDistanceRecorderAlgorithm<TVertex,TEdge>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        public DagShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TEdge, double> weights
            )
            : this(g, weights, new ShortestDistanceRelaxer())
        { }

        public DagShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, g, weights, distanceRelaxer)
        { }

        public DagShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TEdge,double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, g,weights, distanceRelaxer)
        {}

        public event VertexEventHandler<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            VertexEventHandler<TVertex> eh = this.StartVertex;
            if (eh!=null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex,TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(TEdge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public void Initialize()
        {
            this.VertexColors.Clear();
            this.Distances.Clear();

            // init color, distance
            var initialDistance = this.DistanceRelaxer.InitialDistance;
            foreach (var u in VisitedGraph.Vertices)
            {
                this.VertexColors.Add(u, GraphColor.White);
                this.Distances.Add(u, initialDistance);
                this.OnInitializeVertex(u);
            }
        }
        
        protected override void  InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("RootVertex not initialized");

            this.Initialize();
            VertexColors[rootVertex] = GraphColor.Gray;
            Distances[rootVertex] = 0;
            ComputeNoInit(rootVertex);
        }

        public void ComputeNoInit(TVertex s)
        {
            ICollection<TVertex> orderedVertices = AlgoUtility.TopologicalSort<TVertex, TEdge>(this.VisitedGraph);

            OnDiscoverVertex(s);
            foreach (var v in orderedVertices)
            {
                OnExamineVertex(v);
                foreach (var e in VisitedGraph.OutEdges(v))
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

        private bool Relax(TEdge e)
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
