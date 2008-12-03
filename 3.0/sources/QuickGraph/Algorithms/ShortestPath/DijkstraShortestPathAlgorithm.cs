using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// A single-source shortest path algorithm for directed graph
    /// with positive distance.
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="lawler01combinatorial"
    ///     />
    [Serializable]
    public sealed class DijkstraShortestPathAlgorithm<TVertex, TEdge> :
        ShortestPathAlgorithmBase<TVertex,TEdge,IVertexListGraph<TVertex,TEdge>>,
        IVertexColorizerAlgorithm<TVertex,TEdge>,
        IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>,
        IDistanceRecorderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private FibonacciQueue<TVertex,double> vertexQueue;        

        public DijkstraShortestPathAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            : this(visitedGraph, weights, ShortestDistanceRelaxer.Instance)
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
        { }

        public event VertexEventHandler<TVertex> InitializeVertex;
        public event VertexEventHandler<TVertex> DiscoverVertex;
        public event VertexEventHandler<TVertex> StartVertex;
        public event VertexEventHandler<TVertex> ExamineVertex;
        public event EdgeEventHandler<TVertex, TEdge> ExamineEdge;
        public event VertexEventHandler<TVertex> FinishVertex;
        public event EdgeEventHandler<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(TEdge e)
        {
            var eh = this.EdgeNotRelaxed;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        private void InternalTreeEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            bool decreased = this.Relax(args.Edge);
            if (decreased)
            {
                this.OnTreeEdge(args.Edge);
            }
            else
                this.OnEdgeNotRelaxed(args.Edge);
        }

        private void InternalGrayTarget(Object sender, EdgeEventArgs<TVertex, TEdge> args)
        {
            bool decreased = this.Relax(args.Edge);
            if (decreased)
            {
                this.vertexQueue.Update(args.Edge.Target);
                this.OnTreeEdge(args.Edge);
            }
            else
            {
                this.OnEdgeNotRelaxed(args.Edge);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.VertexColors.Clear();
            // init color, distance
            var initialDistance = this.DistanceRelaxer.InitialDistance;
            foreach (var u in VisitedGraph.Vertices)
            {
                this.VertexColors.Add(u, GraphColor.White);
                this.Distances.Add(u, initialDistance);
            }
        }
        
        protected override void  InternalCompute()
        {
            TVertex rootVertex;
            
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("RootVertex not initialized");

            this.VertexColors[rootVertex] = GraphColor.Gray;
            this.Distances[rootVertex] = 0;
            ComputeNoInit(rootVertex);
        }

        public void ComputeNoInit(TVertex s)
        {
            this.vertexQueue = new FibonacciQueue<TVertex, double>(this.Distances);            
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
#if DEBUG
                bfs.ExamineEdge += (sender, e) => {
                    if (this.vertexQueue.Count == 0) return;
                    var top = this.vertexQueue.Peek();
                    var vertices = this.vertexQueue.ToArray();
                    for (int i = 1; i < vertices.Length; ++i)
                        if (this.Distances[top] > this.Distances[vertices[i]])
                            Contract.Assert(false);
                };
#endif
                bfs.ExamineVertex += this.ExamineVertex;
                bfs.FinishVertex += this.FinishVertex;

                bfs.TreeEdge += new EdgeEventHandler<TVertex,TEdge>(this.InternalTreeEdge);
                bfs.GrayTarget += new EdgeEventHandler<TVertex, TEdge>(this.InternalGrayTarget);

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

                    bfs.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(this.InternalTreeEdge);
                    bfs.GrayTarget -= new EdgeEventHandler<TVertex, TEdge>(this.InternalGrayTarget);
                }
            }
        }
    }
}
