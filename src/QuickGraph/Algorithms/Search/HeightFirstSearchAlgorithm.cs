using System;
using System.Collections.Generic;

namespace ModelDriven.Graph.Algorithms.Search
{

    /// <summary>
    /// </summary>
    public sealed class HeightFirstSearchAlgorithm<Vertex,Edge> :
        IAlgorithm<I,
        IPredecessorRecorderAlgorithm,
        ITimeStamperAlgorithm,
        IVertexColorizerAlgorithm,
        ITreeEdgeBuilderAlgorithm
    {
        private IBidirectionalVertexListGraph visitedGraph;
        private VertexColorDictionary colors;
        private int maxDepth = int.MaxValue;

        /// <summary>
        /// A height first search algorithm on a directed graph
        /// </summary>
        /// <param name="g">The graph to traverse</param>
        /// <exception cref="ArgumentNullException">g is null</exception>
        public HeightFirstSearchAlgorithm(IBidirectionalVertexListGraph g)
        {
            if (g == null)
                throw new ArgumentNullException("g");
            this.visitedGraph = g;
            this.colors = new VertexColorDictionary();
        }

        /// <summary>
        /// A height first search algorithm on a directed graph
        /// </summary>
        /// <param name="g">The graph to traverse</param>
        /// <param name="colors">vertex color map</param>
        /// <exception cref="ArgumentNullException">g or colors are null</exception>
        public HeightFirstSearchAlgorithm(
            IBidirectionalVertexListGraph g,
            VertexColorDictionary colors
            )
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (colors == null)
                throw new ArgumentNullException("Colors");

            this.visitedGraph = g;
            this.colors = colors;
        }

        /// <summary>
        /// Visited graph
        /// </summary>
        public IBidirectionalVertexListGraph VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        Object IAlgorithm.VisitedGraph
        {
            get
            {
                return this.VisitedGraph;
            }
        }

        /// <summary>
        /// Gets the vertex color map
        /// </summary>
        /// <value>
        /// Vertex color (<see cref="GraphColor"/>) dictionary
        /// </value>
        public VertexColorDictionary Colors
        {
            get
            {
                return this.colors;
            }
        }

        /// <summary>
        /// IVertexColorizerAlgorithm implementation
        /// </summary>
        IDictionary IVertexColorizerAlgorithm.Colors
        {
            get
            {
                return this.Colors;
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

        #region Events
        /// <summary>
        /// Invoked on every vertex of the graph before the start of the graph 
        /// search.
        /// </summary>
        public event VertexEventHandler InitializeVertex;

        /// <summary>
        /// Raises the <see cref="InitializeVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        protected void OnInitializeVertex(IVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs(v));
        }

        /// <summary>
        /// Invoked on the source vertex once before the start of the search. 
        /// </summary>
        public event VertexEventHandler StartVertex;

        /// <summary>
        /// Raises the <see cref="StartVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        protected void OnStartVertex(IVertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs(v));
        }

        /// <summary>
        /// Invoked when a vertex is encountered for the first time. 
        /// </summary>
        public event VertexEventHandler DiscoverVertex;


        /// <summary>
        /// Raises the <see cref="DiscoverVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        protected void OnDiscoverVertex(IVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs(v));
        }

        /// <summary>
        /// Invoked on every out-edge of each vertex after it is discovered. 
        /// </summary>
        public event EdgeEventHandler ExamineEdge;


        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        protected void OnExamineEdge(IEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs(e));
        }

        /// <summary>
        /// Invoked on each edge as it becomes a member of the edges that form 
        /// the search tree. If you wish to record predecessors, do so at this 
        /// event point. 
        /// </summary>
        public event EdgeEventHandler TreeEdge;


        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        protected void OnTreeEdge(IEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs(e));
        }

        /// <summary>
        /// Invoked on the back edges in the graph. 
        /// </summary>
        public event EdgeEventHandler BackEdge;


        /// <summary>
        /// Raises the <see cref="BackEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        protected void OnBackEdge(IEdge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs(e));
        }

        /// <summary>
        /// Invoked on forward or cross edges in the graph. 
        /// (In an undirected graph this method is never called.) 
        /// </summary>
        public event EdgeEventHandler ForwardOrCrossEdge;


        /// <summary>
        /// Raises the <see cref="ForwardOrCrossEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        protected void OnForwardOrCrossEdge(IEdge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs(e));
        }

        /// <summary>
        /// Invoked on a vertex after all of its out edges have been added to 
        /// the search tree and all of the adjacent vertices have been 
        /// discovered (but before their out-edges have been examined). 
        /// </summary>
        public event VertexEventHandler FinishVertex;

        /// <summary>
        /// Raises the <see cref="FinishVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        protected void OnFinishVertex(IVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs(v));
        }
        #endregion

        /// <summary>
        /// Execute the DFS search.
        /// </summary>
        public void Compute()
        {
            Compute(null);
        }

        /// <summary>
        /// Execute the DFS starting with the vertex s
        /// </summary>
        /// <param name="s">Starting vertex</param>
        public void Compute(IVertex s)
        {
            // put all vertex to white
            Initialize();

            // if there is a starting vertex, start whith him:
            if (s != null)
            {
                OnStartVertex(s);
                Visit(s, 0);
            }

            // process each vertex 
            foreach (IVertex u in VisitedGraph.Vertices)
            {
                if (Colors[u] == GraphColor.White)
                {
                    OnStartVertex(u);
                    Visit(u, 0);
                }
            }
        }

        /// <summary>
        /// Initializes the vertex color map
        /// </summary>
        /// <remarks>
        /// </remarks>
        public void Initialize()
        {
            foreach (IVertex u in VisitedGraph.Vertices)
            {
                Colors[u] = GraphColor.White;
                OnInitializeVertex(u);
            }
        }

        /// <summary>
        /// Does a depth first search on the vertex u
        /// </summary>
        /// <param name="u">vertex to explore</param>
        /// <param name="depth">current recursion depth</param>
        /// <exception cref="ArgumentNullException">u cannot be null</exception>
        public void Visit(IVertex u, int depth)
        {
            if (depth > this.maxDepth)
                return;
            if (u == null)
                throw new ArgumentNullException("u");

            Colors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            IVertex v = null;
            foreach (IEdge e in VisitedGraph.InEdges(u))
            {
                OnExamineEdge(e);
                v = e.Source;
                GraphColor c = Colors[v];
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

            Colors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }

        /// <summary>
        /// Registers the predecessors handler
        /// </summary>
        /// <param name="vis"></param>
        public void RegisterPredecessorRecorderHandlers(IPredecessorRecorderVisitor vis)
        {
            if (vis == null)
                throw new ArgumentNullException("visitor");
            TreeEdge += new EdgeEventHandler(vis.TreeEdge);
            FinishVertex += new VertexEventHandler(vis.FinishVertex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vis"></param>
        public void RegisterTimeStamperHandlers(ITimeStamperVisitor vis)
        {
            if (vis == null)
                throw new ArgumentNullException("visitor");

            DiscoverVertex += new VertexEventHandler(vis.DiscoverVertex);
            FinishVertex += new VertexEventHandler(vis.FinishVertex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vis"></param>
        public void RegisterVertexColorizerHandlers(IVertexColorizerVisitor vis)
        {
            if (vis == null)
                throw new ArgumentNullException("visitor");

            InitializeVertex += new VertexEventHandler(vis.InitializeVertex);
            DiscoverVertex += new VertexEventHandler(vis.DiscoverVertex);
            FinishVertex += new VertexEventHandler(vis.FinishVertex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vis"></param>
        public void RegisterTreeEdgeBuilderHandlers(ITreeEdgeBuilderVisitor vis)
        {
            if (vis == null)
                throw new ArgumentNullException("visitor");

            TreeEdge += new EdgeEventHandler(vis.TreeEdge);
        }
    }
}
