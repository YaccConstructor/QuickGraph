using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// A single-source shortest path algorithm for undirected graph
    /// with positive distance.
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="lawler01combinatorial"
    ///     />
    [Serializable]
    public sealed class UndirectedDijkstraShortestPathAlgorithm<Vertex, Edge> :
        ShortestPathAlgorithmBase<Vertex, Edge, IUndirectedGraph<Vertex, Edge>>,
        IVertexColorizerAlgorithm<Vertex, Edge>,
        IVertexPredecessorRecorderAlgorithm<Vertex, Edge>,
        IDistanceRecorderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private PriorithizedVertexBuffer<Vertex, double> vertexQueue;

        public UndirectedDijkstraShortestPathAlgorithm(
            IUndirectedGraph<Vertex, Edge> visitedGraph,
            IDictionary<Edge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : base(visitedGraph, weights, distanceRelaxer)
        { }

        public event VertexEventHandler<Vertex> InitializeVertex;
        public event VertexEventHandler<Vertex> StartVertex;
        public event VertexEventHandler<Vertex> DiscoverVertex;
        public event VertexEventHandler<Vertex> ExamineVertex;
        public event EdgeEventHandler<Vertex, Edge> ExamineEdge;
        public event VertexEventHandler<Vertex> FinishVertex;

        public event EdgeEventHandler<Vertex, Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }
        public event EdgeEventHandler<Vertex, Edge> EdgeNotRelaxed;
        private void OnEdgeNotRelaxed(Edge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        private void InternalTreeEdge(Object sender, EdgeEventArgs<Vertex, Edge> args)
        {
            bool decreased = Relax(args.Edge);
            if (decreased)
                OnTreeEdge(args.Edge);
            else
                OnEdgeNotRelaxed(args.Edge);
        }

        private void InternalGrayTarget(Object sender, EdgeEventArgs<Vertex, Edge> args)
        {
            bool decreased = Relax(args.Edge);
            if (decreased)
            {
                this.vertexQueue.Update(args.Edge.Target);
                OnTreeEdge(args.Edge);
            }
            else
            {
                OnEdgeNotRelaxed(args.Edge);
            }
        }

        public void Initialize()
        {
            this.VertexColors.Clear();
            this.Distances.Clear();
            // init color, distance
            foreach (Vertex u in VisitedGraph.Vertices)
            {
                this.VertexColors.Add(u, GraphColor.White);
                this.Distances.Add(u, double.MaxValue);
            }
        }

        protected override void InternalCompute()
        {
            if (this.RootVertex == null)
                throw new InvalidOperationException("RootVertex not initialized");

            this.Initialize();
            this.VertexColors[this.RootVertex] = GraphColor.Gray;
            this.Distances[this.RootVertex] = 0;
            ComputeNoInit(this.RootVertex);
        }

        public void ComputeNoInit(Vertex s)
        {
            this.vertexQueue = new PriorithizedVertexBuffer<Vertex, double>(this.Distances);
            UndirectedBreadthFirstSearchAlgorithm<Vertex, Edge> bfs = new UndirectedBreadthFirstSearchAlgorithm<Vertex, Edge>(
                this.VisitedGraph,
                this.vertexQueue,
                VertexColors
                );

            try
            {
                bfs.InitializeVertex += this.InitializeVertex;
                bfs.DiscoverVertex += this.DiscoverVertex;
                bfs.StartVertex += this.StartVertex;
                bfs.ExamineEdge += this.ExamineEdge;
                bfs.ExamineVertex += this.ExamineVertex;
                bfs.FinishVertex += this.FinishVertex;

                bfs.TreeEdge += new EdgeEventHandler<Vertex, Edge>(this.InternalTreeEdge);
                bfs.GrayTarget += new EdgeEventHandler<Vertex, Edge>(this.InternalGrayTarget);

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

                    bfs.TreeEdge -= new EdgeEventHandler<Vertex, Edge>(this.InternalTreeEdge);
                    bfs.GrayTarget -= new EdgeEventHandler<Vertex, Edge>(this.InternalGrayTarget);
                }
            }
        }

        private bool Relax(Edge e)
        {
            double du = this.Distances[e.Source];
            double dv = this.Distances[e.Target];
            double we = this.Weights[e];

            if (Compare(Combine(du, we), dv))
            {
                this.Distances[e.Target] = Combine(du, we);
                return true;
            }
            else
                return false;
        }
    }
}
