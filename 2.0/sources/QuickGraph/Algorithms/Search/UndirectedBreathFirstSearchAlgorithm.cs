using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A breath first search algorithm for undirected graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class UndirectedBreadthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex, TEdge>>,
        IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>,
        IDistanceRecorderAlgorithm<TVertex, TEdge>,
        IVertexColorizerAlgorithm<TVertex, TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
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
            if (vertexQueue == null)
                throw new ArgumentNullException("vertexQueue");
            if (vertexColors == null)
                throw new ArgumentNullException("vertexColors");

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
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex, TEdge> NonTreeEdge;
        private void OnNonTreeEdge(TEdge e)
        {
            if (NonTreeEdge != null)
                NonTreeEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex, TEdge> GrayTarget;
        private void OnGrayTarget(TEdge e)
        {
            if (GrayTarget != null)
                GrayTarget(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex, TEdge> BlackTarget;
        private void OnBlackTarget(TEdge e)
        {
            if (BlackTarget != null)
                BlackTarget(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public void Initialize()
        {
            // initialize vertex u
            var cancelManager = this.Services.CancelManager;
            foreach (var v in VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling)
                    return;
                VertexColors[v] = GraphColor.White;
                OnInitializeVertex(v);
            }
        }

        protected override void InternalCompute()
        {
            this.Initialize();

            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
            {
                // enqueue all roots
                foreach (var root in AlgoUtility.Roots(this.VisitedGraph))
                    this.EnqueueRoot(root);
            }
            else
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
            OnDiscoverVertex(s);
            this.vertexQueue.Enqueue(s);
        }

        private void FlushVisitQueue()
        {
            var cancelManager = this.Services.CancelManager;

            while (this.vertexQueue.Count != 0)
            {
                if (cancelManager.IsCancelling) return;

                TVertex u = this.vertexQueue.Dequeue();

                OnExamineVertex(u);
                foreach (var e in VisitedGraph.AdjacentEdges(u))
                {
                    TVertex v = (e.Source.Equals(u)) ? e.Target : e.Source;
                    OnExamineEdge(e);

                    GraphColor vColor = VertexColors[v];
                    if (vColor == GraphColor.White)
                    {
                        OnTreeEdge(e);
                        VertexColors[v] = GraphColor.Gray;
                        OnDiscoverVertex(v);
                        this.vertexQueue.Enqueue(v);
                    }
                    else
                    {
                        OnNonTreeEdge(e);
                        if (vColor == GraphColor.Gray)
                        {
                            OnGrayTarget(e);
                        }
                        else
                        {
                            OnBlackTarget(e);
                        }
                    }
                }
                VertexColors[u] = GraphColor.Black;

                OnFinishVertex(u);
            }
        }
    }
}
