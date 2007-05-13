using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A depth first search algorithm for directed graph
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class DepthFirstSearchAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex,IVertexListGraph<Vertex, Edge>>,
        IDistanceRecorderAlgorithm<Vertex,Edge>,
        IVertexColorizerAlgorithm<Vertex,Edge>,
        IVertexPredecessorRecorderAlgorithm<Vertex,Edge>,
        IVertexTimeStamperAlgorithm<Vertex,Edge>,
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
		private readonly IDictionary<Vertex,GraphColor> colors;
		private int maxDepth = int.MaxValue;

        public DepthFirstSearchAlgorithm(IVertexListGraph<Vertex,Edge> g)
            :this(g, new Dictionary<Vertex, GraphColor>())
        {}

		public DepthFirstSearchAlgorithm(
            IVertexListGraph<Vertex, Edge> visitedGraph,
            IDictionary<Vertex, GraphColor> colors
            )
            :base(visitedGraph)
		{
			if (colors == null)
				throw new ArgumentNullException("VertexColors");

			this.colors = colors;
		}

        public IDictionary<Vertex,GraphColor> VertexColors
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
            VertexEventHandler<Vertex> eh = this.InitializeVertex;
			if (eh!=null)
				eh(this, new VertexEventArgs<Vertex>(v));
		}

		public event VertexEventHandler<Vertex> StartVertex;
		private void OnStartVertex(Vertex v)
		{
            VertexEventHandler<Vertex> eh = this.StartVertex;
			if (eh!=null)
				eh(this, new VertexEventArgs<Vertex>(v));
		}

		public event VertexEventHandler<Vertex> DiscoverVertex;
		private void OnDiscoverVertex(Vertex v)
		{
            VertexEventHandler<Vertex> eh = this.DiscoverVertex;
			if (eh!=null)
				eh(this, new VertexEventArgs<Vertex>(v));
		}

		public event EdgeEventHandler<Vertex,Edge> ExamineEdge;
		private void OnExamineEdge(Edge e)
		{
            EdgeEventHandler<Vertex, Edge> eh = this.ExamineEdge;
			if (eh!=null)
				eh(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> TreeEdge;
		private void OnTreeEdge(Edge e)
		{
            EdgeEventHandler<Vertex, Edge> eh = this.TreeEdge;
			if (eh!=null)
				eh(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> BackEdge;
		private void OnBackEdge(Edge e)
		{
            EdgeEventHandler<Vertex, Edge> eh = this.BackEdge;
			if (eh!=null)
				eh(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> ForwardOrCrossEdge;
		private void OnForwardOrCrossEdge(Edge e)
		{
            EdgeEventHandler<Vertex, Edge> eh = this.ForwardOrCrossEdge;
			if (eh!=null)
				eh(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event VertexEventHandler<Vertex> FinishVertex;
		private void OnFinishVertex(Vertex v)
		{
            VertexEventHandler<Vertex> eh = this.FinishVertex;
			if (eh!=null)
				eh(this, new VertexEventArgs<Vertex>(v));
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
			foreach(Vertex u in VisitedGraph.Vertices)
			{
                if (this.IsAborting)
                    return;
                if (VertexColors[u] == GraphColor.White)
                {
					OnStartVertex(u);
					Visit(u,0);
				}
			}
		}

		public void Initialize()
		{
			foreach(Vertex u in VisitedGraph.Vertices)
			{
                VertexColors[u] = GraphColor.White;
                OnInitializeVertex(u);
			}
		}

        private sealed class SearchFrame
        {
            public readonly Vertex Vertex;
            public readonly IEnumerator<Edge> Edges;
            public SearchFrame(Vertex vertex, IEnumerator<Edge> edges)
            {
                this.Vertex = vertex;
                this.Edges = edges;
            }
        }

		public void Visit(Vertex root, int depth)
		{
			if (root==null)
				throw new ArgumentNullException("root");

            Stack<SearchFrame> todo = new Stack<SearchFrame>();
            this.VertexColors[root] = GraphColor.Gray;
            OnDiscoverVertex(root);

            todo.Push(new SearchFrame(root, this.VisitedGraph.OutEdges(root).GetEnumerator()));
            while (todo.Count > 0)
            {
                if (this.IsAborting)
                    return;

                SearchFrame frame = todo.Pop();

                Vertex u = frame.Vertex;
                IEnumerator<Edge> edges = frame.Edges;
                while(edges.MoveNext())
                {
                    Edge e = edges.Current;
                    if (this.IsAborting)
                        return;
                    this.OnExamineEdge(e);
                    Vertex v = e.Target;
                    GraphColor c = this.VertexColors[v];
                    switch (c)
                    {
                        case GraphColor.White:
                            OnTreeEdge(e);
                            todo.Push(new SearchFrame(u, edges));
                            u = v;
                            edges = this.VisitedGraph.OutEdges(u).GetEnumerator();
                            this.VertexColors[u] = GraphColor.Gray;
                            this.OnDiscoverVertex(u);
                            break;
                        case GraphColor.Gray:
                            OnBackEdge(e); break;
                        case GraphColor.Black:
                            OnForwardOrCrossEdge(e); break;
                    }
                }

                this.VertexColors[u] = GraphColor.Black;
                this.OnFinishVertex(u);
            }
		}
    }
}
