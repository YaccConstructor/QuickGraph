using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A breath first search algorithm for undirected graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class UndirectedBreadthFirstSearchAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex, IUndirectedGraph<Vertex, Edge>>,
        IVertexPredecessorRecorderAlgorithm<Vertex, Edge>,
        IDistanceRecorderAlgorithm<Vertex, Edge>,
        IVertexColorizerAlgorithm<Vertex, Edge>,
        ITreeBuilderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, GraphColor> vertexColors;
        private VertexBuffer<Vertex> vertexQueue;

        public UndirectedBreadthFirstSearchAlgorithm(IUndirectedGraph<Vertex, Edge> g)
            : this(g, new VertexBuffer<Vertex>(), new Dictionary<Vertex, GraphColor>())
        { }

        public UndirectedBreadthFirstSearchAlgorithm(
            IUndirectedGraph<Vertex, Edge> visitedGraph,
            VertexBuffer<Vertex> vertexQueue,
            IDictionary<Vertex, GraphColor> vertexColors
            )
            : base(visitedGraph)
        {
            if (vertexQueue == null)
                throw new ArgumentNullException("vertexQueue");
            if (vertexColors == null)
                throw new ArgumentNullException("vertexColors");

            this.vertexColors = vertexColors;
            this.vertexQueue = vertexQueue;
        }

        public IDictionary<Vertex, GraphColor> VertexColors
        {
            get
            {
                return vertexColors;
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
            VertexEventHandler<Vertex> eh = this.StartVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> ExamineVertex;
        private void OnExamineVertex(Vertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(this, new VertexEventArgs<Vertex>(v));
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

        public event EdgeEventHandler<Vertex, Edge> NonTreeEdge;
        private void OnNonTreeEdge(Edge e)
        {
            if (NonTreeEdge != null)
                NonTreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> GrayTarget;
        private void OnGrayTarget(Edge e)
        {
            if (GrayTarget != null)
                GrayTarget(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> BlackTarget;
        private void OnBlackTarget(Edge e)
        {
            if (BlackTarget != null)
                BlackTarget(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event VertexEventHandler<Vertex> FinishVertex;
        private void OnFinishVertex(Vertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public void Initialize()
        {
            // initialize vertex u
            foreach (Vertex v in VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                VertexColors[v] = GraphColor.White;
                OnInitializeVertex(v);
            }
        }

        protected override void InternalCompute()
        {
            if (this.RootVertex == null)
                this.RootVertex = TraversalHelper.GetFirstVertex<Vertex, Edge>(this.VisitedGraph);

            this.Initialize();
            if (this.RootVertex == null)
            {
                this.RootVertex = TraversalHelper.GetFirstVertex<Vertex, Edge>(this.VisitedGraph);
                foreach (Vertex v in this.VisitedGraph.Vertices)
                {
                    if (this.VertexColors[v] == GraphColor.White)
                    {
                        this.OnStartVertex(v);
                        this.Visit(v);
                    }
                }
            }
            else
            {
                this.OnStartVertex(this.RootVertex);
                this.Visit(this.RootVertex);
            }
        }

        public void Visit(Vertex s)
        {
            if (this.IsAborting)
                return;

            this.VertexColors[s] = GraphColor.Gray;
            OnDiscoverVertex(s);

            this.vertexQueue.Push(s);
            while (this.vertexQueue.Count != 0)
            {
                if (this.IsAborting)
                    return;
                Vertex u = this.vertexQueue.Pop();

                OnExamineVertex(u);
                foreach (Edge e in VisitedGraph.AdjacentEdges(u))
                {
                    Vertex v = (e.Source.Equals(u)) ? e.Target : e.Source; 
                    OnExamineEdge(e);

                    GraphColor vColor = VertexColors[v];
                    if (vColor == GraphColor.White)
                    {
                        OnTreeEdge(e);
                        VertexColors[v] = GraphColor.Gray;
                        OnDiscoverVertex(v);
                        this.vertexQueue.Push(v);
                    }
                    else
                    {
                        OnNonTreeEdge(e);
                        if (vColor == GraphColor.Gray)
                        {
                            OnGrayTarget(e);
                        }
                        else
                        {
                            OnBlackTarget(e);
                        }
                    }
                }
                VertexColors[u] = GraphColor.Black;

                OnFinishVertex(u);
            }
        }
    }
}
