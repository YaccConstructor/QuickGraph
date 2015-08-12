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
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     id="boost"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
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
            Func<TEdge, double> weights
            )
            : this(g, weights, DistanceRelaxers.ShortestDistance)
        { }

        public DagShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, g, weights, distanceRelaxer)
        { }

        public DagShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> g,
            Func<TEdge,double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, g,weights, distanceRelaxer)
        {}

        public event VertexAction<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(v);
        }

        public event VertexAction<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            var eh = this.StartVertex;
            if (eh!=null)
                eh(v);
        }

        public event VertexAction<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(v);
        }

        public event VertexAction<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(v);
        }

        public event EdgeAction<TVertex,TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(e);
        }

        public event EdgeAction<TVertex,TEdge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(TEdge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(e);
        }

        public event VertexAction<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(v);
        }

        protected override void Initialize()
        {
            base.Initialize();
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

            VertexColors[rootVertex] = GraphColor.Gray;
            Distances[rootVertex] = 0;
            ComputeNoInit(rootVertex);
        }

        public void ComputeNoInit(TVertex s)
        {
            var orderedVertices = AlgorithmExtensions.TopologicalSort(this.VisitedGraph);

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
    }
}
