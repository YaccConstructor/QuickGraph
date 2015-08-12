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
    /// Dijkstra single-source shortest path algorithm for directed graph
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
    public sealed class DijkstraShortestPathAlgorithm<TVertex, TEdge> 
        : ShortestPathAlgorithmBase<TVertex,TEdge,IVertexListGraph<TVertex,TEdge>>
        , IVertexColorizerAlgorithm<TVertex,TEdge>
        , IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        , IDistanceRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private FibonacciQueue<TVertex,double> vertexQueue;        

        public DijkstraShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            : this(visitedGraph, weights, DistanceRelaxers.ShortestDistance)
        { }

        public DijkstraShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, visitedGraph, weights, distanceRelaxer)
        { }

        public DijkstraShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph,weights, distanceRelaxer)
        {}

        public event VertexAction<TVertex> InitializeVertex;
        public event VertexAction<TVertex> DiscoverVertex;
        public event VertexAction<TVertex> StartVertex;
        public event VertexAction<TVertex> ExamineVertex;
        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        public event VertexAction<TVertex> FinishVertex;

        public event EdgeAction<TVertex,TEdge> EdgeNotRelaxed;
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
                this.AssertHeap();
            }
            else
                this.OnEdgeNotRelaxed(args);
        }

        private void InternalGrayTarget(TEdge edge)
        {
            bool decreased = this.Relax(edge);
            if (decreased)
            {
                this.vertexQueue.Update(edge.Target);
                this.AssertHeap();
                this.OnTreeEdge(edge);
            }
            else
            {
                this.OnEdgeNotRelaxed(edge);
            }
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
            }
            this.vertexQueue = new FibonacciQueue<TVertex, double>(this.DistancesIndexGetter());
        }
        
        protected override void  InternalCompute()
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

        [Conditional("DEBUG")]
        private void AssertHeap()
        {
            if (this.vertexQueue.Count == 0) return;
            var top = this.vertexQueue.Peek();
            var vertices = this.vertexQueue.ToArray();
            for (int i = 1; i < vertices.Length; ++i)
                if (this.Distances[top] > this.Distances[vertices[i]])
                    Contract.Assert(false);
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
#if SUPERDEBUG
                bfs.ExamineEdge += e => this.AssertHeap();
#endif
                bfs.ExamineVertex += this.ExamineVertex;
                bfs.FinishVertex += this.FinishVertex;

                bfs.ExamineEdge += new EdgeAction<TVertex,TEdge>(this.InternalExamineEdge);
                bfs.TreeEdge += new EdgeAction<TVertex,TEdge>(this.InternalTreeEdge);
                bfs.GrayTarget += new EdgeAction<TVertex, TEdge>(this.InternalGrayTarget);

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
                }
            }
        }
    }
}
