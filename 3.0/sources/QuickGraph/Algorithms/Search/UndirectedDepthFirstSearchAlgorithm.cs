using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A depth first search algorithm for undirected graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex, TEdge>>,
        IDistanceRecorderAlgorithm<TVertex, TEdge>,
        IVertexColorizerAlgorithm<TVertex, TEdge>,
        IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>,
        IVertexTimeStamperAlgorithm<TVertex, TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> colors;
        private int maxDepth = int.MaxValue;

        public UndirectedDepthFirstSearchAlgorithm(IUndirectedGraph<TVertex, TEdge> g)
            :this(g, new Dictionary<TVertex, GraphColor>())
        {
        }

        public UndirectedDepthFirstSearchAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            :this(null, visitedGraph, colors)
        {}

        public UndirectedDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            :base(host, visitedGraph)
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

        public GraphColor GetVertexColor(TVertex vertex)
        {
            return this.colors[vertex];
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

        public event VertexAction<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(v);
        }

        public event VertexAction<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            if (StartVertex != null)
                StartVertex(v);
        }

        public event VertexAction<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(v);
        }

        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, e);
        }

        public event EdgeAction<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, e);
        }

        public event EdgeAction<TVertex, TEdge> BackEdge;
        private void OnBackEdge(TEdge e)
        {
            if (BackEdge != null)
                BackEdge(this, e);
        }

        public event EdgeAction<TVertex, TEdge> ForwardOrCrossEdge;
        private void OnForwardOrCrossEdge(TEdge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, e);
        }

        public event VertexAction<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(v);
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

            var cancelManager = this.Services.CancelManager;
            // process each vertex 
            foreach (var u in VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling)
                    return;
                if (VertexColors[u] == GraphColor.White)
                {
                    OnStartVertex(u);
                    Visit(u, 0);
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            var cancelManager = this.Services.CancelManager;
            foreach (var u in VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling)
                    return;
                VertexColors[u] = GraphColor.White;
                OnInitializeVertex(u);
            }
        }

        public void Visit(TVertex u, int depth)
        {
            if (depth > this.maxDepth)
                return;
            if (u == null)
                throw new ArgumentNullException("u");

            var cancelManager = this.Services.CancelManager;
            if (cancelManager.IsCancelling)
                return;

            VertexColors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            TVertex v = default(TVertex);
            foreach (var e in VisitedGraph.AdjacentEdges(u))
            {
                if (cancelManager.IsCancelling)
                    return;

                OnExamineEdge(e);
                if (u.Equals(e.Source))
                    v = e.Target;
                else
                    v = e.Source;

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

            VertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }
    }
}
