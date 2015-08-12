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
    public sealed class DepthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IVertexListGraph<TVertex, TEdge>>,
        IDistanceRecorderAlgorithm<TVertex,TEdge>,
        IVertexColorizerAlgorithm<TVertex,TEdge>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>,
        IVertexTimeStamperAlgorithm<TVertex,TEdge>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
		private readonly IDictionary<TVertex,GraphColor> colors;
		private int maxDepth = int.MaxValue;
        private readonly Func<IEnumerable<TEdge>, IEnumerable<TEdge>> outEdgeEnumerator;

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="visitedGraph">visited graph</param>
        public DepthFirstSearchAlgorithm(IVertexListGraph<TVertex, TEdge> visitedGraph)
            :this(visitedGraph, new Dictionary<TVertex, GraphColor>())
        {}

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="visitedGraph">visited graph</param>
        /// <param name="colors">vertex color map</param>
        public DepthFirstSearchAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors
            )
            : this(null, visitedGraph, colors)
        { }

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="host">algorithm host</param>
        /// <param name="visitedGraph">visited graph</param>
        public DepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph
            )
            : this(host, visitedGraph, new Dictionary<TVertex, GraphColor>(), e => e)
        { }

        /// <summary>
        /// Initializes a new instance of the algorithm.
        /// </summary>
        /// <param name="host">algorithm host</param>
        /// <param name="visitedGraph">visited graph</param>
        /// <param name="colors">vertex color map</param>
        public DepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
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
        /// <param name="outEdgeEnumerator">
        /// Delegate that takes the enumeration of out-edges and reorders
        /// them. All vertices passed to the method should be enumerated once and only once.
        /// May be null.
        /// </param>
        public DepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, GraphColor> colors,
            Func<IEnumerable<TEdge>, IEnumerable<TEdge>> outEdgeEnumerator
            )
            :base(host, visitedGraph)
		{
            Contract.Requires(colors != null);
            Contract.Requires(outEdgeEnumerator != null);

			this.colors = colors;
            this.outEdgeEnumerator = outEdgeEnumerator;
		}

        public IDictionary<TVertex,GraphColor> VertexColors
		{
			get
			{
				return this.colors;
			}
		}

        public Func<IEnumerable<TEdge>, IEnumerable<TEdge>> OutEdgeEnumerator
        {
            get { return this.outEdgeEnumerator; }
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

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(this.MaxDepth > 0);
        }

		public event VertexAction<TVertex> InitializeVertex;
		private void OnInitializeVertex(TVertex v)
		{
            var eh = this.InitializeVertex;
			if (eh!=null)
				eh(v);
		}

		public event VertexAction<TVertex> StartVertex;
		private void OnStartVertex(TVertex v)
		{
            var eh = this.StartVertex;
			if (eh!=null)
				eh(v);
		}

		public event VertexAction<TVertex> DiscoverVertex;
		private void OnDiscoverVertex(TVertex v)
		{
            var eh = this.DiscoverVertex;
			if (eh!=null)
				eh(v);
		}

		public event EdgeAction<TVertex,TEdge> ExamineEdge;
		private void OnExamineEdge(TEdge e)
		{
            var eh = this.ExamineEdge;
			if (eh!=null)
				eh(e);
		}

		public event EdgeAction<TVertex,TEdge> TreeEdge;
		private void OnTreeEdge(TEdge e)
		{
            var eh = this.TreeEdge;
			if (eh!=null)
				eh(e);
		}

		public event EdgeAction<TVertex,TEdge> BackEdge;
		private void OnBackEdge(TEdge e)
		{
            var eh = this.BackEdge;
			if (eh!=null)
				eh(e);
		}

		public event EdgeAction<TVertex,TEdge> ForwardOrCrossEdge;
		private void OnForwardOrCrossEdge(TEdge e)
		{
            var eh = this.ForwardOrCrossEdge;
			if (eh!=null)
				eh(e);
		}

		public event VertexAction<TVertex> FinishVertex;
		private void OnFinishVertex(TVertex v)
		{
            var eh = this.FinishVertex;
			if (eh!=null)
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
            public SearchFrame(TVertex vertex, IEnumerator<TEdge> edges, int depth)
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
            var oee = this.OutEdgeEnumerator;
            this.VertexColors[root] = GraphColor.Gray;
            this.OnDiscoverVertex(root);

            var cancelManager = this.Services.CancelManager;
            var enumerable = oee(this.VisitedGraph.OutEdges(root));
            todo.Push(new SearchFrame(root, enumerable.GetEnumerator(), 0));
            while (todo.Count > 0)
            {
                if (cancelManager.IsCancelling) return;

                var frame = todo.Pop();
                var u = frame.Vertex;
                var depth = frame.Depth;
                var edges = frame.Edges;

                if (depth > this.MaxDepth)
                {
                    if (edges != null)
                        edges.Dispose();
                    this.VertexColors[u] = GraphColor.Black;
                    this.OnFinishVertex(u);
                    continue;
                }

                while(edges.MoveNext())
                {
                    TEdge e = edges.Current;
                    if (cancelManager.IsCancelling) return;

                    this.OnExamineEdge(e);
                    TVertex v = e.Target;
                    GraphColor c = this.VertexColors[v];
                    switch (c)
                    {
                        case GraphColor.White:
                            this.OnTreeEdge(e);
                            todo.Push(new SearchFrame(u, edges, depth));
                            u = v;
                            edges = oee(this.VisitedGraph.OutEdges(u)).GetEnumerator();
                            depth++;
                            this.VertexColors[u] = GraphColor.Gray;
                            this.OnDiscoverVertex(u);
                            break;
                        case GraphColor.Gray:
                            this.OnBackEdge(e); break;
                        case GraphColor.Black:
                            this.OnForwardOrCrossEdge(e); break;
                    }
                }
                if (edges != null)
                    edges.Dispose();

                this.VertexColors[u] = GraphColor.Black;
                this.OnFinishVertex(u);
            }
		}
    }
}
