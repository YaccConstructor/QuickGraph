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
    public sealed class ParallelBreadthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IVertexListGraph<TVertex, TEdge>>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>,
        IDistanceRecorderAlgorithm<TVertex,TEdge>,
        IVertexColorizerAlgorithm<TVertex, TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, GraphColor> vertexColors;
        private readonly IQueue<TVertex> vertexQueue;

        private IDictionary<TVertex, int> vertexIndices;
        private int[] vertexIndexedColors;

        public ParallelBreadthFirstSearchAlgorithm(IVertexListGraph<TVertex,TEdge> g)
            : this(g, new QuickGraph.Collections.ConcurrentQueue<TVertex>(), new Dictionary<TVertex, GraphColor>())
        {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitedGraph"></param>
        /// <param name="vertexQueue">thread-safe queue</param>
        /// <param name="vertexColors"></param>
        public ParallelBreadthFirstSearchAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IQueue<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            : this(null, visitedGraph, vertexQueue, vertexColors)
        { }

        public ParallelBreadthFirstSearchAlgorithm(
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

        public GraphColor GetVertexColor(TVertex vertex)
        {
            return (GraphColor)this.vertexIndexedColors[this.vertexIndices[vertex]];
        }

        public event VertexEventHandler<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            var eh = this.InitializeVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            var eh = this.StartVertex;
            if (eh!=null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event EventHandler NextLevel;
        private void OnNextLevel()
        {
            var eh = this.NextLevel;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        public event VertexEventHandler<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            var eh = this.ExamineVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            var eh = this.DiscoverVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex,TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            var eh = this.FinishVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public void Initialize()
        {
            var cancelManager = this.Services.CancelManager;

            // initialize indices and colors
            var vertexCount = this.VisitedGraph.VertexCount;
            this.vertexIndices = new Dictionary<TVertex, int>(vertexCount);
            this.vertexIndexedColors = new int[vertexCount];
            int i = 0;
            foreach(var vertex in this.VisitedGraph.Vertices)
            {
                this.vertexIndices.Add(vertex, i);
                // White = 0, so implicitely set by the runtime
                // this.vertexIndexedColors[i] = (int)GraphColor.White;
                i++;
            }

            // initialize vertex u
            Parallel.ForEach(this.VisitedGraph.Vertices, delegate(TVertex v)
            {
                if (cancelManager.IsCancelling) return;

                OnInitializeVertex(v);
            });

            // make sure queue is empty
            GraphContracts.Assert(this.vertexQueue.Count == 0);
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
                this.EnqueueRoot(rootVertex);
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

            Interlocked.Exchange(
                ref this.vertexIndexedColors[this.vertexIndices[s]], 
                (int)GraphColor.Gray);

            OnDiscoverVertex(s);
            this.vertexQueue.Enqueue(s);
        }

        private void FlushVisitQueue()
        {
            var cancelManager = this.Services.CancelManager;
            var visitedGraph = this.VisitedGraph;

            while (this.vertexQueue.Count != 0)
            {
                if (cancelManager.IsCancelling) return;

                Parallel.For(0, this.vertexQueue.Count, (i, tlocal) =>
                {
                    var u = this.vertexQueue.Dequeue();
                    this.OnExamineVertex(u);
                    Parallel.ForEach(visitedGraph.OutEdges(u), e =>
                    {
                        TVertex v = e.Target;
                        OnExamineEdge(e);

                        int vIndex = this.vertexIndices[v];
                        var vColor = (GraphColor)Interlocked.CompareExchange(
                            ref this.vertexIndexedColors[vIndex],
                            (int)GraphColor.Gray,
                            (int)GraphColor.White);
                        if (vColor == GraphColor.White)
                        {
                            this.OnTreeEdge(e);
                            this.OnDiscoverVertex(v);
                            this.vertexQueue.Enqueue(v);
                        }
                    });

                    this.OnFinishVertex(u);
                });

                this.OnNextLevel();
            }
        }
    }
}
