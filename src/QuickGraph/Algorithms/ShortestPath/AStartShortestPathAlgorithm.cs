using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// A* single-source shortest path algorithm for directed graph
    /// with positive distance.
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     idref="lawler01combinatorial"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class AStarShortestPathAlgorithm<TVertex, TEdge> 
        : ShortestPathAlgorithmBase<TVertex, TEdge, IVertexListGraph<TVertex, TEdge>>
        , IVertexColorizerAlgorithm<TVertex, TEdge>
        , IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        , IDistanceRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private FibonacciQueue<TVertex, double> vertexQueue;
        private Dictionary<TVertex, double> costs;
        private readonly Func<TVertex, double> costHeuristic;

        public AStarShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            Func<TVertex, double> costHeuristic
            )
            : this(visitedGraph, weights, costHeuristic, DistanceRelaxers.ShortestDistance)
        { }

        public AStarShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            Func<TVertex, double> costHeuristic,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, visitedGraph, weights, costHeuristic, distanceRelaxer)
        { }

        public AStarShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            Func<TVertex, double> costHeuristic,
            IDistanceRelaxer distanceRelaxer
            )
            : base(host, visitedGraph, weights, distanceRelaxer)
        {
            Contract.Requires(costHeuristic != null);

            this.costHeuristic = costHeuristic;
        }

        public Func<TVertex, double> CostHeuristic
        {
            get { return this.costHeuristic; }
        }

        public event VertexAction<TVertex> InitializeVertex;
        public event VertexAction<TVertex> DiscoverVertex;
        public event VertexAction<TVertex> StartVertex;
        public event VertexAction<TVertex> ExamineVertex;
        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        public event VertexAction<TVertex> FinishVertex;

        public event EdgeAction<TVertex, TEdge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(TEdge e)
        {
            var eh = this.EdgeNotRelaxed;
            if (eh != null)
                eh(e);
        }

        private void InternalExamineEdge(TEdge args)
        {
            if (this.Weights(args) < 0)
                throw new NegativeWeightException();
        }

        private void InternalTreeEdge(TEdge args)
        {
            bool decreased = this.Relax(args);
            if (decreased)
            {
                this.OnTreeEdge(args);
            }
            else
                this.OnEdgeNotRelaxed(args);
        }

        private void InternalGrayTarget(TEdge e)
        {
            var target = e.Target;

            bool decreased = this.Relax(e);
            double distance = this.Distances[target];
            if (decreased)
            {
                this.costs[target] = this.DistanceRelaxer.Combine(distance, this.costHeuristic(target));
                this.vertexQueue.Update(target);
                this.OnTreeEdge(e);
            }
            else
            {
                this.OnEdgeNotRelaxed(e);
            }
        }

        private void InternalBlackTarget(TEdge e)
        {
            var target = e.Target;

            bool decreased = this.Relax(e);
            double distance = this.Distances[target];
            if (decreased)
            {
                this.OnTreeEdge(e);
                this.costs[target] = this.DistanceRelaxer.Combine(distance, this.costHeuristic(target));
                this.vertexQueue.Enqueue(target);
                this.VertexColors[target] = GraphColor.Gray;
            }
            else
            {
                this.OnEdgeNotRelaxed(e);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.VertexColors.Clear();
            this.costs = new Dictionary<TVertex, double>(this.VisitedGraph.VertexCount);
            // init color, distance
            var initialDistance = this.DistanceRelaxer.InitialDistance;
            foreach (var u in VisitedGraph.Vertices)
            {
                this.VertexColors.Add(u, GraphColor.White);
                this.Distances.Add(u, initialDistance);
                this.costs.Add(u, initialDistance);
            }
            this.vertexQueue = new FibonacciQueue<TVertex, double>(this.costs, this.DistanceRelaxer.Compare);
        }

        protected override void InternalCompute()
        {
            TVertex rootVertex;
            if (this.TryGetRootVertex(out rootVertex))
                this.ComputeFromRoot(rootVertex);
            else
            {
                foreach (var v in this.VisitedGraph.Vertices)
                    if (this.VertexColors[v] == GraphColor.White)
                        this.ComputeFromRoot(v);
            }
        }

        private void ComputeFromRoot(TVertex rootVertex)
        {
            Contract.Requires(rootVertex != null);
            Contract.Requires(this.VisitedGraph.ContainsVertex(rootVertex));
            Contract.Requires(this.VertexColors[rootVertex] == GraphColor.White);

            this.VertexColors[rootVertex] = GraphColor.Gray;
            this.Distances[rootVertex] = 0;
            this.ComputeNoInit(rootVertex);
        }

        public void ComputeNoInit(TVertex s)
        {
            BreadthFirstSearchAlgorithm<TVertex, TEdge> bfs = null;

            try
            {
                bfs = new BreadthFirstSearchAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    this.vertexQueue,
                    VertexColors
                    );

                bfs.InitializeVertex += this.InitializeVertex;
                bfs.DiscoverVertex += this.DiscoverVertex;
                bfs.StartVertex += this.StartVertex;
                bfs.ExamineEdge += this.ExamineEdge;
                bfs.ExamineVertex += this.ExamineVertex;
                bfs.FinishVertex += this.FinishVertex;

                bfs.ExamineEdge += new EdgeAction<TVertex, TEdge>(this.InternalExamineEdge);
                bfs.TreeEdge += new EdgeAction<TVertex, TEdge>(this.InternalTreeEdge);
                bfs.GrayTarget += new EdgeAction<TVertex, TEdge>(this.InternalGrayTarget);
                bfs.BlackTarget +=new EdgeAction<TVertex,TEdge>(this.InternalBlackTarget);

                bfs.Visit(s);
            }
            finally
            {
                if (bfs != null)
                {
                    bfs.InitializeVertex -= this.InitializeVertex;
                    bfs.DiscoverVertex -= this.DiscoverVertex;
                    bfs.StartVertex -= this.StartVertex;
                    bfs.ExamineEdge -= this.ExamineEdge;
                    bfs.ExamineVertex -= this.ExamineVertex;
                    bfs.FinishVertex -= this.FinishVertex;

                    bfs.ExamineEdge -= new EdgeAction<TVertex, TEdge>(this.InternalExamineEdge);
                    bfs.TreeEdge -= new EdgeAction<TVertex, TEdge>(this.InternalTreeEdge);
                    bfs.GrayTarget -= new EdgeAction<TVertex, TEdge>(this.InternalGrayTarget);
                    bfs.BlackTarget -= new EdgeAction<TVertex, TEdge>(this.InternalBlackTarget);
                }
            }
        }
    }
}
