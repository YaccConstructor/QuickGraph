using System;
using System.Collections.Generic;

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
    public sealed class BidirectionalDepthFirstSearchAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex, IBidirectionalGraph<Vertex, Edge>>,
        IVertexColorizerAlgorithm<Vertex, Edge>,
        ITreeBuilderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, GraphColor> colors;
        private int maxDepth = int.MaxValue;

        public BidirectionalDepthFirstSearchAlgorithm(IBidirectionalGraph<Vertex, Edge> g)
            : this(g, new Dictionary<Vertex, GraphColor>())
        { }

        public BidirectionalDepthFirstSearchAlgorithm(
            IBidirectionalGraph<Vertex, Edge> visitedGraph,
            IDictionary<Vertex, GraphColor> colors
            )
            : base(visitedGraph)
        {
            if (colors == null)
                throw new ArgumentNullException("VertexColors");

            this.colors = colors;
        }

        public IDictionary<Vertex, GraphColor> VertexColors
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

        public event VertexEventHandler<Vertex> InitializeVertex;
        private void OnInitializeVertex(Vertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> StartVertex;
        private void OnStartVertex(Vertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> DiscoverVertex;
        private void OnDiscoverVertex(Vertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex, Edge> ExamineEdge;
        private void OnExamineEdge(Edge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> BackEdge;
        private void OnBackEdge(Edge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> ForwardOrCrossEdge;
        private void OnForwardOrCrossEdge(Edge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event VertexEventHandler<Vertex> FinishVertex;
        private void OnFinishVertex(Vertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<Vertex>(v));
        }

        protected override void InternalCompute()
        {
            // put all vertex to white
            Initialize();

            // if there is a starting vertex, start whith him:
            if (this.RootVertex != null)
            {
                OnStartVertex(this.RootVertex);
                Visit(this.RootVertex, 0);
            }

            // process each vertex 
            foreach (Vertex u in VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                if (VertexColors[u] == GraphColor.White)
                {
                    OnStartVertex(u);
                    Visit(u, 0);
                }
            }
        }

        public void Initialize()
        {
            foreach (Vertex u in VisitedGraph.Vertices)
            {
                VertexColors[u] = GraphColor.White;
                OnInitializeVertex(u);
            }
        }

        public void Visit(Vertex u, int depth)
        {
            if (depth > this.maxDepth)
                return;
            if (u == null)
                throw new ArgumentNullException("u");
            if (this.IsAborting)
                return;

            VertexColors[u] = GraphColor.Gray;
            OnDiscoverVertex(u);

            Vertex v = default(Vertex);
            foreach (Edge e in VisitedGraph.OutEdges(u))
            {
                if (this.IsAborting)
                    return;
                OnExamineEdge(e);
                v = e.Target;
                ProcessEdge(depth, v, e);
            }
            foreach (Edge e in VisitedGraph.InEdges(u))
            {
                if (this.IsAborting)
                    return;
                OnExamineEdge(e);
                v = e.Source;
                ProcessEdge(depth, v, e);
            }

            VertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }

        private void ProcessEdge(int depth, Vertex v, Edge e)
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
