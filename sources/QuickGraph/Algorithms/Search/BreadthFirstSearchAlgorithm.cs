using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A breath first search algorithm for directed graphs
    /// </summary>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class BreadthFirstSearchAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex, IVertexListGraph<TVertex, TEdge>>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>,
        IDistanceRecorderAlgorithm<TVertex,TEdge>,
        IVertexColorizerAlgorithm<TVertex,TEdge>,
        ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> vertexColors;
        private VertexBuffer<TVertex> vertexQueue;

        public BreadthFirstSearchAlgorithm(IVertexListGraph<TVertex,TEdge> g)
            :this(g, new VertexBuffer<TVertex>(), new Dictionary<TVertex, GraphColor>())
        {}

        public BreadthFirstSearchAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            VertexBuffer<TVertex> vertexQueue,
            IDictionary<TVertex, GraphColor> vertexColors
            )
            :base(visitedGraph)
        {
            if (vertexQueue == null)
                throw new ArgumentNullException("vertexQueue");
            if (vertexColors == null)
                throw new ArgumentNullException("vertexColors");

            this.vertexColors = vertexColors;
            this.vertexQueue = vertexQueue;
        }

        public IDictionary<TVertex,GraphColor> VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        public event VertexEventHandler<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            VertexEventHandler<TVertex> eh = this.StartVertex;
            if (eh!=null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> ExamineVertex;
        private void OnExamineVertex(TVertex v)
        {
            if (ExamineVertex != null)
                ExamineVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            if (DiscoverVertex != null)
                DiscoverVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex,TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> NonTreeEdge;
        private void OnNonTreeEdge(TEdge e)
        {
            if (NonTreeEdge != null)
                NonTreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> GrayTarget;
        private void OnGrayTarget(TEdge e)
        {
            if (GrayTarget != null)
                GrayTarget(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event EdgeEventHandler<TVertex,TEdge> BlackTarget;
        private void OnBlackTarget(TEdge e)
        {
            if (BlackTarget != null)
                BlackTarget(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (FinishVertex != null)
                FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public void Initialize()
        {
            // initialize vertex u
            foreach (TVertex v in VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                VertexColors[v] = GraphColor.White;
                OnInitializeVertex(v);
            }
        }

        protected override void  InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;

            this.Initialize();
            if (this.RootVertex == null)
            {
                this.RootVertex = TraversalHelper.GetFirstVertex<TVertex, TEdge>(this.VisitedGraph);
                foreach (TVertex v in this.VisitedGraph.Vertices)
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

        public void Visit(TVertex s)
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
                TVertex u = this.vertexQueue.Pop();

                OnExamineVertex(u);
                foreach (TEdge e in VisitedGraph.OutEdges(u))
                {
                    TVertex v = e.Target;
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
