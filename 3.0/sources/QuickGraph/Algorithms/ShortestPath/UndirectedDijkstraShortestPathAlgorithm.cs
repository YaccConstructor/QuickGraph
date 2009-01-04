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
    /// A single-source shortest path algorithm for undirected graph
    /// with positive distance.
    /// </summary>
    /// <reference-ref
    ///     idref="lawler01combinatorial"
    ///     />
    [Serializable]
    public sealed class UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge> 
        : UndirectedShortestPathAlgorithmBase<TVertex, TEdge>
        , IVertexColorizerAlgorithm<TVertex, TEdge>
        , IUndirectedVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        , IDistanceRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IPriorityQueue<TVertex> vertexQueue;

        public UndirectedDijkstraShortestPathAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            : this(visitedGraph, weights, ShortestDistanceRelaxer.Instance)
        { }

        public UndirectedDijkstraShortestPathAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, visitedGraph, weights, distanceRelaxer)
        { }

        public UndirectedDijkstraShortestPathAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : base(host, visitedGraph, weights, distanceRelaxer)
        { }

        public event VertexEventHandler<TVertex> InitializeVertex;
        public event VertexEventHandler<TVertex> StartVertex;
        public event VertexEventHandler<TVertex> DiscoverVertex;
        public event VertexEventHandler<TVertex> ExamineVertex;
        public event EdgeEventHandler<TVertex, TEdge> ExamineEdge;
        public event VertexEventHandler<TVertex> FinishVertex;

        public event UndirectedEdgeEventHandler<TVertex, TEdge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(TEdge e, bool reversed)
        {
            var eh = EdgeNotRelaxed;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        private void InternalTreeEdge(Object sender, UndirectedEdgeEventArgs<TVertex, TEdge> args)
        {
            Contract.Requires(args != null);

            bool decreased = Relax(args.Edge, args.Source, args.Target);
            if (decreased)
                this.OnTreeEdge(args.Edge, args.Reversed);
            else
                this.OnEdgeNotRelaxed(args.Edge, args.Reversed);
        }

        private void InternalGrayTarget(Object sender, UndirectedEdgeEventArgs<TVertex, TEdge> args)
        {
            Contract.Requires(args != null);

            bool decreased = Relax(args.Edge, args.Source, args.Target);
            if (decreased)
            {
                this.vertexQueue.Update(args.Target);
                this.AssertHeap();
                OnTreeEdge(args.Edge, args.Reversed);
            }
            else
            {
                OnEdgeNotRelaxed(args.Edge, args.Reversed);
            }
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

        protected override void Initialize()
        {
            base.Initialize();

            this.VertexColors.Clear();
            var initialDistance = this.DistanceRelaxer.InitialDistance;
            // init color, distance
            foreach (var u in VisitedGraph.Vertices)
            {
                this.VertexColors.Add(u, GraphColor.White);
                this.Distances.Add(u, initialDistance);
            }
            this.vertexQueue = new FibonacciQueue<TVertex, double>(this.VisitedGraph.Vertices, v => this.Distances[v]);
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
            Contract.Requires(s != null);
            Contract.Requires(this.VisitedGraph.ContainsVertex(s));

            UndirectedBreadthFirstSearchAlgorithm<TVertex, TEdge> bfs = null;
            try
            {
                bfs = new UndirectedBreadthFirstSearchAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    this.vertexQueue,
                    VertexColors
                    );

                bfs.InitializeVertex += this.InitializeVertex;
                bfs.DiscoverVertex += this.DiscoverVertex;
                bfs.StartVertex += this.StartVertex;
                bfs.ExamineEdge += this.ExamineEdge;
#if DEBUG
                bfs.ExamineEdge += (sender, e) => this.AssertHeap();
#endif
                bfs.ExamineVertex += this.ExamineVertex;
                bfs.FinishVertex += this.FinishVertex;

                bfs.TreeEdge += new UndirectedEdgeEventHandler<TVertex, TEdge>(this.InternalTreeEdge);
                bfs.GrayTarget += new UndirectedEdgeEventHandler<TVertex, TEdge>(this.InternalGrayTarget);

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

                    bfs.TreeEdge -= new UndirectedEdgeEventHandler<TVertex, TEdge>(this.InternalTreeEdge);
                    bfs.GrayTarget -= new UndirectedEdgeEventHandler<TVertex, TEdge>(this.InternalGrayTarget);
                }
            }
        }
    }
}
