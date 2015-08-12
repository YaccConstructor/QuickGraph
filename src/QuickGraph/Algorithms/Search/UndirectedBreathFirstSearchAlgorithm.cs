using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A breath first search algorithm for undirected graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class UndirectedBreadthFirstSearchAlgorithm<TVertex, TEdge> 
        : RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex, TEdge>>
        , IUndirectedVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        , IDistanceRecorderAlgorithm<TVertex, TEdge>
        , IVertexColorizerAlgorithm<TVertex, TEdge>
        , IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> vertexColors;
        private IQueue<TVertex> vertexQueue;

        public UndirectedBreadthFirstSearchAlgorithm(IUndirectedGraph<TVertex, TEdge> g)
            : this(g, new QuickGraph.Collections.Queue<TVertex>(), new Dictionary<TVertex, GraphColor>())
        { }

        public UndirectedBreadthFirstSearchAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IQueue<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            : this(null, visitedGraph, vertexQueue, vertexColors)
        { }

        public UndirectedBreadthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IQueue<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            : base(host, visitedGraph)
        {
            Contract.Requires(vertexQueue != null);
            Contract.Requires(vertexColors != null);

            this.vertexColors = vertexColors;
            this.vertexQueue = vertexQueue;
        }

        public IDictionary<TVertex, GraphColor> VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        public GraphColor GetVertexColor(TVertex vertex)
        {
            return this.vertexColors[vertex];
        }

        public event VertexAction<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            var eh = this.InitializeVertex;
            if (eh != null)
                eh(v);
        }


        public event VertexAction<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            var eh = this.StartVertex;
            if (eh != null)
                eh(v);
        }

        public event VertexAction<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            var eh = this.ExamineVertex;
            if (eh != null)
                eh(v);
        }

        public event VertexAction<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            var eh = this.DiscoverVertex;
            if (eh != null)
                eh(v);
        }

        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(e);
        }

        public event UndirectedEdgeAction<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e, bool reversed)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> NonTreeEdge;
        private void OnNonTreeEdge(TEdge e, bool reversed)
        {
            var eh = this.NonTreeEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> GrayTarget;
        private void OnGrayTarget(TEdge e, bool reversed)
        {
            var eh = this.GrayTarget;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> BlackTarget;
        private void OnBlackTarget(TEdge e, bool reversed)
        {
            var eh = this.BlackTarget;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event VertexAction<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            var eh = this.FinishVertex;
            if (eh != null)
                eh(v);
        }

        protected override void Initialize()
        {
            base.Initialize();

            // initialize vertex u
            var cancelManager = this.Services.CancelManager;
            if (cancelManager.IsCancelling)
                return;
            foreach (var v in VisitedGraph.Vertices)
            {
                this.VertexColors[v] = GraphColor.White;
                this.OnInitializeVertex(v);
            }
        }

        protected override void InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("missing root vertex");
            this.EnqueueRoot(rootVertex);
            this.FlushVisitQueue();
        }

        public void Visit(TVertex s)
        {
            this.EnqueueRoot(s);
            this.FlushVisitQueue();
        }

        private void EnqueueRoot(TVertex s)
        {
            this.OnStartVertex(s);
            this.VertexColors[s] = GraphColor.Gray;
            this.OnDiscoverVertex(s);
            this.vertexQueue.Enqueue(s);
        }

        private void FlushVisitQueue()
        {
            var cancelManager = this.Services.CancelManager;

            while (this.vertexQueue.Count > 0)
            {
                if (cancelManager.IsCancelling) return;

                var u = this.vertexQueue.Dequeue();

                this.OnExamineVertex(u);
                foreach (var e in VisitedGraph.AdjacentEdges(u))
                {
                    var reversed = e.Target.Equals(u);
                    TVertex v = reversed ? e.Source : e.Target;
                    this.OnExamineEdge(e);

                    var vColor = this.VertexColors[v];
                    if (vColor == GraphColor.White)
                    {
                        this.OnTreeEdge(e, reversed);
                        this.VertexColors[v] = GraphColor.Gray;
                        this.OnDiscoverVertex(v);
                        this.vertexQueue.Enqueue(v);
                    }
                    else
                    {
                        this.OnNonTreeEdge(e, reversed);
                        if (vColor == GraphColor.Gray)
                            this.OnGrayTarget(e, reversed);
                        else
                            this.OnBlackTarget(e, reversed);
                    }
                }
                this.VertexColors[u] = GraphColor.Black;
                this.OnFinishVertex(u);
            }
        }
    }
}
