using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;
using System.Threading;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A <b>parallel</b> breath first search algorithm for directed graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    /// <reference-ref
    ///     href="http://www.cc.gatech.edu/~bader/papers/MultithreadedBFS-ICPP2006.pdf"
    ///     />
    [Serializable]
    public sealed class ParralelBreadthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IVertexListGraph<TVertex, TEdge>>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>,
        IDistanceRecorderAlgorithm<TVertex,TEdge>,
        IVertexColorizerAlgorithm<TVertex,TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> vertexColors;
        private IQueue<TVertex> vertexQueue;

        public ParralelBreadthFirstSearchAlgorithm(IVertexListGraph<TVertex,TEdge> g)
            : this(g, new QuickGraph.Collections.Queue<TVertex>(), new Dictionary<TVertex, GraphColor>())
        {}

        public ParralelBreadthFirstSearchAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IQueue<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            : this(null, visitedGraph, vertexQueue, vertexColors)
        { }

        public ParralelBreadthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IQueue<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            :base(host, visitedGraph)
        {
            if (vertexQueue == null)
                throw new ArgumentNullException("vertexQueue");
            if (vertexColors == null)
                throw new ArgumentNullException("vertexColors");

            this.vertexColors = vertexColors;
            this.vertexQueue = vertexQueue;
        }

        public IDictionary<TVertex,GraphColor> VertexColors
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
            if (eh!=null)
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

        public event EdgeEventHandler<TVertex,TEdge> NonTreeEdge;
        private void OnNonTreeEdge(TEdge e)
        {
            if (NonTreeEdge != null)
                NonTreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> GrayTarget;
        private void OnGrayTarget(TEdge e)
        {
            if (GrayTarget != null)
                GrayTarget(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> BlackTarget;
        private void OnBlackTarget(TEdge e)
        {
            if (BlackTarget != null)
                BlackTarget(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public void Initialize()
        {
            var cancelManager = this.Services.CancelManager;
            // initialize vertex u
            Parallel.ForEach(this.VisitedGraph.Vertices, delegate(TVertex v)
            {
                if (cancelManager.IsCancelling) return;
                VertexColors[v] = GraphColor.White;
                OnInitializeVertex(v);
            });
        }

        protected override void InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;

            this.Initialize();

            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
            {
                // enqueue roots
                foreach (var root in this.VisitedGraph.Roots())
                    this.EnqueueRoot(root);
            }
            else // enqueue select root only
            {
                this.Visit(rootVertex);
            }
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

                var queued = this.vertexQueue.ToArray();
                Parallel.ForEach(queued, delegate(TVertex u)
                {
                    OnExamineVertex(u);
                    Parallel.ForEach(this.VisitedGraph.OutEdges(u), delegate(TEdge e)
                    {
                        TVertex v = e.Target;
                        OnExamineEdge(e);

                        GraphColor vColor;
                        lock (this.SyncRoot)
                            vColor = VertexColors[v];
                        if (vColor == GraphColor.White)
                        {
                            OnTreeEdge(e);
                            lock (SyncRoot)
                                VertexColors[v] = GraphColor.Gray;
                            OnDiscoverVertex(v);
                            this.vertexQueue.Enqueue(v);
                        }
                        else
                        {
                            OnNonTreeEdge(e);
                            if (vColor == GraphColor.Gray)
                                OnGrayTarget(e);
                            else
                                OnBlackTarget(e);
                        }
                    });
                    lock (this.SyncRoot)
                        VertexColors[u] = GraphColor.Black;

                    OnFinishVertex(u);
                });
            }
        }
    }
}
