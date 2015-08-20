using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A depth and height first search algorithm for directed graphs
    /// </summary>
    /// <remarks>
    /// This is a modified version of the classic DFS algorithm
    /// where the search is performed both in depth and height.
    /// </remarks>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class BidirectionalDepthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IBidirectionalGraph<TVertex, TEdge>>,
        IVertexColorizerAlgorithm<TVertex, TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> colors;
        private int maxDepth = int.MaxValue;

        public BidirectionalDepthFirstSearchAlgorithm(IBidirectionalGraph<TVertex, TEdge> g)
            : this(g, new Dictionary<TVertex, GraphColor>())
        { }

        public BidirectionalDepthFirstSearchAlgorithm(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            : this(null, visitedGraph, colors)
        { }

        public BidirectionalDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            : base(host, visitedGraph)
        {
            if (colors == null)
                throw new ArgumentNullException("VertexColors");

            this.colors = colors;
        }

        public IDictionary<TVertex, GraphColor> VertexColors
        {
            get
            {
                return this.colors;
            }
        }

        public int MaxDepth
        {
            get
            {
                return this.maxDepth;
            }
            set
            {
                this.maxDepth = value;
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
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<TVertex>(v));
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

        public event EdgeEventHandler<TVertex, TEdge> BackEdge;
        private void OnBackEdge(TEdge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event EdgeEventHandler<TVertex, TEdge> ForwardOrCrossEdge;
        private void OnForwardOrCrossEdge(TEdge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        protected override void InternalCompute()
        {
            // put all vertex to white
            Initialize();

            // if there is a starting vertex, start whith him:
            TVertex rootVertex;
            if (this.TryGetRootVertex(out rootVertex))
            {
                OnStartVertex(rootVertex);
                Visit(rootVertex, 0);
            }

            // process each vertex 
            var cancelManager = this.Services.CancelManager;
            foreach (var u in VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling) return;
                if (VertexColors[u] == GraphColor.White)
                {
                    OnStartVertex(u);
                    Visit(u, 0);
                }
            }
        }

        public void Initialize()
        {
            foreach (var u in VisitedGraph.Vertices)
            {
                VertexColors[u] = GraphColor.White;
                OnInitializeVertex(u);
            }
        }

        public void Visit(TVertex u, int depth)
        {
            GraphContracts.AssumeNotNull(u, "u");
            if (depth > this.maxDepth)
                return;

            VertexColors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            var cancelManager = this.Services.CancelManager;
            TVertex v = default(TVertex);
            foreach (var e in VisitedGraph.OutEdges(u))
            {
                if (cancelManager.IsCancelling) return;

                OnExamineEdge(e);
                v = e.Target;
                ProcessEdge(depth, v, e);
            }

            foreach (var e in VisitedGraph.InEdges(u))
            {
                if (cancelManager.IsCancelling) return;

                OnExamineEdge(e);
                v = e.Source;
                ProcessEdge(depth, v, e);
            }

            VertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }

        private void ProcessEdge(int depth, TVertex v, TEdge e)
        {
            GraphColor c = VertexColors[v];
            if (c == GraphColor.White)
            {
                OnTreeEdge(e);
                Visit(v, depth + 1);
            }
            else if (c == GraphColor.Gray)
            {
                OnBackEdge(e);
            }
            else
            {
                OnForwardOrCrossEdge(e);
            }
        }
    }
}
