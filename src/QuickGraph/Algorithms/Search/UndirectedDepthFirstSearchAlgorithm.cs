using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A depth first search algorithm for directed graph
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex, TEdge>>,
        IDistanceRecorderAlgorithm<TVertex, TEdge>,
        IVertexColorizerAlgorithm<TVertex, TEdge>,
        IUndirectedVertexPredecessorRecorderAlgorithm<TVertex, TEdge>,
        IVertexTimeStamperAlgorithm<TVertex, TEdge>,
        IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, GraphColor> colors;
        private int maxDepth = int.MaxValue;
        private readonly Func<IEnumerable<TEdge>, IEnumerable<TEdge>> adjacentEdgeEnumerator;

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="visitedGraph">visited graph</param>
        public UndirectedDepthFirstSearchAlgorithm(IUndirectedGraph<TVertex, TEdge> visitedGraph)
            : this(visitedGraph, new Dictionary<TVertex, GraphColor>())
        { }

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="visitedGraph">visited graph</param>
        /// <param name="colors">vertex color map</param>
        public UndirectedDepthFirstSearchAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            : this(null, visitedGraph, colors)
        { }

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="host">algorithm host</param>
        /// <param name="visitedGraph">visited graph</param>
        /// <param name="colors">vertex color map</param>
        public UndirectedDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            : this(host, visitedGraph, colors, e => e)
        { }

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="host">algorithm host</param>
        /// <param name="visitedGraph">visited graph</param>
        /// <param name="colors">vertex color map</param>
        /// <param name="adjacentEdgeEnumerator">
        /// Delegate that takes the enumeration of out-edges and reorders
        /// them. All vertices passed to the method should be enumerated once and only once.
        /// May be null.
        /// </param>
        public UndirectedDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors,
            Func<IEnumerable<TEdge>, IEnumerable<TEdge>> adjacentEdgeEnumerator
            )
            : base(host, visitedGraph)
        {
            Contract.Requires(colors != null);
            Contract.Requires(adjacentEdgeEnumerator != null);

            this.colors = colors;
            this.adjacentEdgeEnumerator = adjacentEdgeEnumerator;
        }

        public IDictionary<TVertex, GraphColor> VertexColors
        {
            get
            {
                return this.colors;
            }
        }

        public Func<IEnumerable<TEdge>, IEnumerable<TEdge>> AdjacentEdgeEnumerator
        {
            get { return this.adjacentEdgeEnumerator; }
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
                Contract.Requires(value > 0);
                this.maxDepth = value;
            }
        }

        public event VertexAction<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            Contract.Requires(v != null);

            var eh = this.InitializeVertex;
            if (eh != null)
                eh(v);
        }

        public event VertexAction<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            Contract.Requires(v != null);

            var eh = this.StartVertex;
            if (eh != null)
                eh(v);
        }

        public event VertexAction<TVertex> VertexMaxDepthReached;
        private void OnVertexMaxDepthReached(TVertex v)
        {
            Contract.Requires(v != null);

            var eh = this.VertexMaxDepthReached;
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

        public event UndirectedEdgeAction<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e, bool reversed)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex,TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e, bool reversed)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> BackEdge;
        private void OnBackEdge(TEdge e, bool reversed)
        {
            var eh = this.BackEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        public event UndirectedEdgeAction<TVertex, TEdge> ForwardOrCrossEdge;
        private void OnForwardOrCrossEdge(TEdge e, bool reversed)
        {
            var eh = this.ForwardOrCrossEdge;
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

        protected override void InternalCompute()
        {
            // if there is a starting vertex, start whith him:
            TVertex rootVertex;
            if (this.TryGetRootVertex(out rootVertex))
            {
                this.OnStartVertex(rootVertex);
                this.Visit(rootVertex);
            }
            else
            {
                var cancelManager = this.Services.CancelManager;
                // process each vertex 
                foreach (var u in this.VisitedGraph.Vertices)
                {
                    if (cancelManager.IsCancelling)
                        return;
                    if (this.VertexColors[u] == GraphColor.White)
                    {
                        this.OnStartVertex(u);
                        this.Visit(u);
                    }
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.VertexColors.Clear();
            foreach (var u in this.VisitedGraph.Vertices)
            {
                this.VertexColors[u] = GraphColor.White;
                this.OnInitializeVertex(u);
            }
        }

        struct SearchFrame
        {
            public readonly TVertex Vertex;
            public readonly IEnumerator<TEdge> Edges;
            public readonly int Depth;
            public SearchFrame(
                TVertex vertex, 
                IEnumerator<TEdge> edges, 
                int depth)
            {
                Contract.Requires(vertex != null);
                Contract.Requires(edges != null);
                Contract.Requires(depth >= 0);

                this.Vertex = vertex;
                this.Edges = edges;
                this.Depth = depth;
            }
        }

        public void Visit(TVertex root)
        {
            Contract.Requires(root != null);

            var todo = new Stack<SearchFrame>();
            var oee = this.AdjacentEdgeEnumerator;
            var visitedEdges = new Dictionary<TEdge, int>(this.VisitedGraph.EdgeCount);

            this.VertexColors[root] = GraphColor.Gray;
            this.OnDiscoverVertex(root);

            var cancelManager = this.Services.CancelManager;
            var enumerable = oee(this.VisitedGraph.AdjacentEdges(root));
            todo.Push(new SearchFrame(root, enumerable.GetEnumerator(), 0));
            while (todo.Count > 0)
            {
                if (cancelManager.IsCancelling) return;

                var frame = todo.Pop();
                var u = frame.Vertex;
                var depth = frame.Depth;

                if (depth > this.MaxDepth)
                {
                    this.OnVertexMaxDepthReached(u);
                    this.VertexColors[u] = GraphColor.Black;
                    this.OnFinishVertex(u);
                    continue;
                }

                var edges = frame.Edges;
                while (edges.MoveNext())
                {
                    TEdge e = edges.Current;
                    if (cancelManager.IsCancelling) return;
                    if (visitedEdges.ContainsKey(e)) continue; // edge already visited

                    visitedEdges.Add(e, 0);
                    bool reversed = e.Target.Equals(u);
                    this.OnExamineEdge(e, reversed);
                    TVertex v = reversed ? e.Source : e.Target;
                    var c = this.VertexColors[v];
                    switch (c)
                    {
                        case GraphColor.White:
                            this.OnTreeEdge(e, reversed);
                            todo.Push(new SearchFrame(u, edges, frame.Depth + 1));
                            u = v;
                            edges = oee(this.VisitedGraph.AdjacentEdges(u)).GetEnumerator();
                            this.VertexColors[u] = GraphColor.Gray;
                            this.OnDiscoverVertex(u);
                            break;
                        case GraphColor.Gray:
                            this.OnBackEdge(e, reversed); break;
                        case GraphColor.Black:
                            this.OnForwardOrCrossEdge(e, reversed); break;
                    }
                }

                this.VertexColors[u] = GraphColor.Black;
                this.OnFinishVertex(u);
            }
        }
    }
}
