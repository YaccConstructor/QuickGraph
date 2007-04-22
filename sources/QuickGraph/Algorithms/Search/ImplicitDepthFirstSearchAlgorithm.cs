using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Search
{   
    /// <summary>
    /// A depth first search algorithm for implicit directed graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class ImplicitDepthFirstSearchAlgorithm<Vertex,Edge> :
        RootedAlgorithmBase<Vertex,IIncidenceGraph<Vertex,Edge>>,
        IVertexPredecessorRecorderAlgorithm<Vertex,Edge>,
        IVertexTimeStamperAlgorithm<Vertex,Edge>,
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private int maxDepth = int.MaxValue;
        private IDictionary<Vertex, GraphColor> vertexColors = new Dictionary<Vertex, GraphColor>();

        public ImplicitDepthFirstSearchAlgorithm(IIncidenceGraph<Vertex,Edge> visitedGraph)
            :base(visitedGraph)
        {}

        /// <summary>
        /// Gets the vertex color map
        /// </summary>
        /// <value>
        /// Vertex color (<see cref="GraphColor"/>) dictionary
        /// </value>
        public IDictionary<Vertex,GraphColor> VertexColors
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
        public event VertexEventHandler<Vertex> StartVertex;

        /// <summary>
        /// Raises the <see cref="StartVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnStartVertex(Vertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<Vertex>(v));
        }

        /// <summary>
        /// Invoked when a vertex is encountered for the first time. 
        /// </summary>
        public event VertexEventHandler<Vertex> DiscoverVertex;


        /// <summary>
        /// Raises the <see cref="DiscoverVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnDiscoverVertex(Vertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<Vertex>(v));
        }

        /// <summary>
        /// Invoked on every out-edge of each vertex after it is discovered. 
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> ExamineEdge;


        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnExamineEdge(Edge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// Invoked on each edge as it becomes a member of the edges that form 
        /// the search tree. If you wish to record predecessors, do so at this 
        /// event point. 
        /// </summary>
        public event EdgeEventHandler<Vertex, Edge> TreeEdge;


        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
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
        /// Raises the <see cref="BackEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
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
        /// Raises the <see cref="ForwardOrCrossEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnForwardOrCrossEdge(Edge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        /// <summary>
        /// Invoked on a vertex after all of its out edges have been added to 
        /// the search tree and all of the adjacent vertices have been 
        /// discovered (but before their out-edges have been examined). 
        /// </summary>
        public event VertexEventHandler<Vertex> FinishVertex;

        /// <summary>
        /// Raises the <see cref="FinishVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnFinishVertex(Vertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<Vertex>(v));
        }

        protected override void InternalCompute()
        {
            if (this.RootVertex == null)
                throw new RootVertexNotSpecifiedException();

            this.Initialize();
            this.Visit(this.RootVertex, 0);
        }

        private void Initialize()
        {
            this.VertexColors.Clear();
        }

        private void Visit(Vertex u, int depth)
        {
            if (depth > this.MaxDepth)
                return;
            if (this.IsAborting)
                return;

            VertexColors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            foreach (Edge e in VisitedGraph.OutEdges(u))
            {
                if (this.IsAborting)
                    return;
                OnExamineEdge(e);
                Vertex v = e.Target;

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