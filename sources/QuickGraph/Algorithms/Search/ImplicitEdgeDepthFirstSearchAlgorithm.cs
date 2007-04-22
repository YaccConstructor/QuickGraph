using System;
using System.Collections.Generic;

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
    public sealed class ImplicitEdgeDepthFirstSearchAlgorithm<Vertex,Edge> :
        RootedAlgorithmBase<Vertex,IIncidenceGraph<Vertex,Edge>>,
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private int maxDepth = int.MaxValue;
        private IDictionary<Edge,GraphColor> edgeColors = new Dictionary<Edge,GraphColor>();

        public ImplicitEdgeDepthFirstSearchAlgorithm(IIncidenceGraph<Vertex,Edge> visitedGraph)
            :base(visitedGraph)
        {}

        /// <summary>
        /// <summary>
        /// Gets the vertex color map
        /// </summary>
        /// <value>
        /// Vertex color (<see cref="GraphColor"/>) dictionary
        /// </value>
        public IDictionary<Edge, GraphColor> EdgeColors
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
        public event VertexEventHandler<Vertex> StartVertex;

        /// <summary>
        /// Triggers the StartVertex event.
        /// </summary>
        /// <param name="v"></param>
        private void OnStartVertex(Vertex v)
        {
            if (this.StartVertex != null)
                StartVertex(this, new VertexEventArgs<Vertex>(v));
        }

        /// <summary>
        /// Invoked on the first edge of a test case
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> StartEdge;

        /// <summary>
        /// Triggers the StartEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnStartEdge(Edge e)
        {
            if (this.StartEdge != null)
                StartEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EdgeEdgeEventHandler<Vertex, Edge> DiscoverTreeEdge;

        /// <summary>
        /// Triggers DiscoverEdge event
        /// </summary>
        /// <param name="se"></param>
        /// <param name="e"></param>
        private void OnDiscoverTreeEdge(Edge se, Edge e)
        {
            if (DiscoverTreeEdge != null)
                DiscoverTreeEdge(this, new EdgeEdgeEventArgs<Vertex, Edge>(se, e));
        }

        /// <summary>
        /// Invoked on each edge as it becomes a member of the edges that form 
        /// the search tree. If you wish to record predecessors, do so at this 
        /// event point. 
        /// </summary>
        public event EdgeEventHandler<Vertex, Edge> TreeEdge;

        /// <summary>
        /// Triggers the TreeEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// Invoked on the back edges in the graph. 
        /// </summary>
        public event EdgeEventHandler<Vertex, Edge> BackEdge;

        /// <summary>
        /// Triggers the BackEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnBackEdge(Edge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// Invoked on forward or cross edges in the graph. 
        /// (In an undirected graph this method is never called.) 
        /// </summary>
        public event EdgeEventHandler<Vertex, Edge> ForwardOrCrossEdge;

        /// <summary>
        /// Triggers the ForwardOrCrossEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnForwardOrCrossEdge(Edge e)
        {
            if (this.ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// Invoked on a edge after all of its out edges have been added to 
        /// the search tree and all of the adjacent vertices have been 
        /// discovered (but before their out-edges have been examined). 
        /// </summary>
        public event EdgeEventHandler<Vertex, Edge> FinishEdge;

        /// <summary>
        /// Triggers the ForwardOrCrossEdge event.
        /// </summary>
        /// <param name="e"></param>
        private void OnFinishEdge(Edge e)
        {
            if (this.FinishEdge != null)
                FinishEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        

        protected override void  InternalCompute()
        {
            if (this.RootVertex == null)
                throw new RootVertexNotSpecifiedException();

            // initialize algorithm
            this.Initialize();

            // start whith him:
            OnStartVertex(this.RootVertex);

            // process each out edge of v
            foreach (Edge e in this.VisitedGraph.OutEdges(this.RootVertex))
            {
                if (this.IsAborting)
                    return;
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
        private void Visit(Edge se, int depth)
        {
            if (depth > this.maxDepth)
                return;
            if (se == null)
                throw new ArgumentNullException("se");
            if (this.IsAborting)
                return;

            // mark edge as gray
            this.EdgeColors[se] = GraphColor.Gray;
            // add edge to the search tree
            OnTreeEdge(se);

            // iterate over out-edges
            foreach (Edge e in this.VisitedGraph.OutEdges(se.Target))
            {
                if (this.IsAborting)
                    return;
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