using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Search
{   
    /// <summary>
    /// A depth first search algorithm for implicit directed graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class ImplicitDepthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IIncidenceGraph<TVertex,TEdge>>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>,
        IVertexTimeStamperAlgorithm<TVertex,TEdge>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private int maxDepth = int.MaxValue;
        private IDictionary<TVertex, GraphColor> vertexColors = new Dictionary<TVertex, GraphColor>();

        public ImplicitDepthFirstSearchAlgorithm(
            IIncidenceGraph<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

        public ImplicitDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IIncidenceGraph<TVertex,TEdge> visitedGraph)
            :base(host, visitedGraph)
        {}

        /// <summary>
        /// Gets the vertex color map
        /// </summary>
        /// <value>
        /// Vertex color (<see cref="GraphColor"/>) dictionary
        /// </value>
        public IDictionary<TVertex,GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        /// <summary>
        /// Gets or sets the maximum exploration depth, from
        /// the start vertex.
        /// </summary>
        /// <remarks>
        /// Defaulted at <c>int.MaxValue</c>.
        /// </remarks>
        /// <value>
        /// Maximum exploration depth.
        /// </value>
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

        /// <summary>
        /// Invoked on the source vertex once before the start of the search. 
        /// </summary>
        public event VertexAction<TVertex> StartVertex;

        /// <summary>
        /// Raises the <see cref="StartVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnStartVertex(TVertex v)
        {
            if (StartVertex != null)
                StartVertex(v);
        }

        /// <summary>
        /// Invoked when a vertex is encountered for the first time. 
        /// </summary>
        public event VertexAction<TVertex> DiscoverVertex;


        /// <summary>
        /// Raises the <see cref="DiscoverVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(v);
        }

        /// <summary>
        /// Invoked on every out-edge of each vertex after it is discovered. 
        /// </summary>
        public event EdgeAction<TVertex,TEdge> ExamineEdge;


        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(e);
        }

        /// <summary>
        /// Invoked on each edge as it becomes a member of the edges that form 
        /// the search tree. If you wish to record predecessors, do so at this 
        /// event point. 
        /// </summary>
        public event EdgeAction<TVertex, TEdge> TreeEdge;


        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(e);
        }

        /// <summary>
        /// Invoked on the back edges in the graph. 
        /// </summary>
        public event EdgeAction<TVertex, TEdge> BackEdge;


        /// <summary>
        /// Raises the <see cref="BackEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnBackEdge(TEdge e)
        {
            if (BackEdge != null)
                BackEdge(e);
        }

        /// <summary>
        /// Invoked on forward or cross edges in the graph. 
        /// (In an undirected graph this method is never called.) 
        /// </summary>
        public event EdgeAction<TVertex, TEdge> ForwardOrCrossEdge;


        /// <summary>
        /// Raises the <see cref="ForwardOrCrossEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnForwardOrCrossEdge(TEdge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(e);
        }

        /// <summary>
        /// Invoked on a vertex after all of its out edges have been added to 
        /// the search tree and all of the adjacent vertices have been 
        /// discovered (but before their out-edges have been examined). 
        /// </summary>
        public event VertexAction<TVertex> FinishVertex;

        /// <summary>
        /// Raises the <see cref="FinishVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(v);
        }

        protected override void InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("root vertex not set");

            this.Initialize();
            this.Visit(rootVertex, 0);
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.VertexColors.Clear();
        }

        private void Visit(TVertex u, int depth)
        {
            if (depth > this.MaxDepth)
                return;

            VertexColors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            var cancelManager = this.Services.CancelManager;
            foreach (var e in VisitedGraph.OutEdges(u))
            {
                if (cancelManager.IsCancelling) return;

                OnExamineEdge(e);
                TVertex v = e.Target;

                if (!this.VertexColors.ContainsKey(v))
                {
                    OnTreeEdge(e);
                    Visit(v, depth + 1);
                }
                else
                {
                    GraphColor c = VertexColors[v];
                    if (c == GraphColor.Gray)
                    {
                        OnBackEdge(e);
                    }
                    else
                    {
                        OnForwardOrCrossEdge(e);
                    }
                }
            }

            VertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }
   }
}