using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A edge depth first search algorithm for implicit directed graphs
    /// </summary>
    /// <remarks>
    /// This is a variant of the classic DFS where the edges are color
    /// marked.
    /// </remarks>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class ImplicitEdgeDepthFirstSearchAlgorithm<TVertex,TEdge> :
        RootedAlgorithmBase<TVertex,IIncidenceGraph<TVertex,TEdge>>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private int maxDepth = int.MaxValue;
        private IDictionary<TEdge,GraphColor> edgeColors = new Dictionary<TEdge,GraphColor>();

        public ImplicitEdgeDepthFirstSearchAlgorithm(IIncidenceGraph<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

        public ImplicitEdgeDepthFirstSearchAlgorithm(
            IAlgorithmComponent host,
            IIncidenceGraph<TVertex,TEdge> visitedGraph
            )
            :base(host, visitedGraph)
        {}

        /// <summary>
        /// <summary>
        /// Gets the vertex color map
        /// </summary>
        /// <value>
        /// Vertex color (<see cref="GraphColor"/>) dictionary
        /// </value>
        public IDictionary<TEdge, GraphColor> EdgeColors
        {
            get
            {
                return this.edgeColors;
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
        public event VertexEventHandler<TVertex> StartVertex;

        /// <summary>
        /// Triggers the StartVertex event.
        /// </summary>
        /// <param name="v"></param>
        private void OnStartVertex(TVertex v)
        {
            if (this.StartVertex != null)
                StartVertex(this, new VertexEventArgs<TVertex>(v));
        }

        /// <summary>
        /// Invoked on the first edge of a test case
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> StartEdge;

        /// <summary>
        /// Triggers the StartEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnStartEdge(TEdge e)
        {
            if (this.StartEdge != null)
                StartEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EdgeEdgeEventHandler<TVertex, TEdge> DiscoverTreeEdge;

        /// <summary>
        /// Triggers DiscoverEdge event
        /// </summary>
        /// <param name="se"></param>
        /// <param name="e"></param>
        private void OnDiscoverTreeEdge(TEdge se, TEdge e)
        {
            if (DiscoverTreeEdge != null)
                DiscoverTreeEdge(this, new EdgeEdgeEventArgs<TVertex, TEdge>(se, e));
        }

        /// <summary>
        /// Invoked on each edge as it becomes a member of the edges that form 
        /// the search tree. If you wish to record predecessors, do so at this 
        /// event point. 
        /// </summary>
        public event EdgeEventHandler<TVertex, TEdge> TreeEdge;

        /// <summary>
        /// Triggers the TreeEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        /// <summary>
        /// Invoked on the back edges in the graph. 
        /// </summary>
        public event EdgeEventHandler<TVertex, TEdge> BackEdge;

        /// <summary>
        /// Triggers the BackEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnBackEdge(TEdge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        /// <summary>
        /// Invoked on forward or cross edges in the graph. 
        /// (In an undirected graph this method is never called.) 
        /// </summary>
        public event EdgeEventHandler<TVertex, TEdge> ForwardOrCrossEdge;

        /// <summary>
        /// Triggers the ForwardOrCrossEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnForwardOrCrossEdge(TEdge e)
        {
            if (this.ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        /// <summary>
        /// Invoked on a edge after all of its out edges have been added to 
        /// the search tree and all of the adjacent vertices have been 
        /// discovered (but before their out-edges have been examined). 
        /// </summary>
        public event EdgeEventHandler<TVertex, TEdge> FinishEdge;

        /// <summary>
        /// Triggers the ForwardOrCrossEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnFinishEdge(TEdge e)
        {
            if (this.FinishEdge != null)
                FinishEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        

        protected override void  InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new RootVertexNotSpecifiedException();

            // initialize algorithm
            this.Initialize();

            // start whith him:
            OnStartVertex(rootVertex);

            var cancelManager = this.Services.CancelManager;
            // process each out edge of v
            foreach (var e in this.VisitedGraph.OutEdges(rootVertex))
            {
                if (cancelManager.IsCancelling) return;

                if (!this.EdgeColors.ContainsKey(e))
                {
                    OnStartEdge(e);
                    Visit(e, 0);
                }
            }
        }

        /// <summary>
        /// Does a depth first search on the vertex u
        /// </summary>
        /// <param name="se">edge to explore</param>
        /// <param name="depth">current exploration depth</param>
        /// <exception cref="ArgumentNullException">se cannot be null</exception>
        private void Visit(TEdge se, int depth)
        {
            GraphContracts.AssumeNotNull(se, "se");
            if (depth > this.maxDepth)
                return;

            // mark edge as gray
            this.EdgeColors[se] = GraphColor.Gray;
            // add edge to the search tree
            OnTreeEdge(se);

            var cancelManager = this.Services.CancelManager;
            // iterate over out-edges
            foreach (var e in this.VisitedGraph.OutEdges(se.Target))
            {
                if (cancelManager.IsCancelling) return;

                // check edge is not explored yet,
                // if not, explore it.
                if (!this.EdgeColors.ContainsKey(e))
                {
                    OnDiscoverTreeEdge(se, e);
                    Visit(e, depth + 1);
                }
                else
                {
                    GraphColor c = this.EdgeColors[e];
                    if (EdgeColors[e] == GraphColor.Gray)
                        OnBackEdge(e);
                    else
                        OnForwardOrCrossEdge(e);
                }
            }

            // all out-edges have been explored
            this.EdgeColors[se] = GraphColor.Black;
            OnFinishEdge(se);
        }

        /// <summary>
        /// Initializes the algorithm before computation.
        /// </summary>
        private void Initialize()
        {
            this.EdgeColors.Clear();
        }
   }
}