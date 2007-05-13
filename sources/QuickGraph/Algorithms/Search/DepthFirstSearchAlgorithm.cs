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
		private IDictionary<Vertex,GraphColor> colors;
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
			if (InitializeVertex!=null)
				InitializeVertex(this, new VertexEventArgs<Vertex>(v));
		}

		public event VertexEventHandler<Vertex> StartVertex;
		private void OnStartVertex(Vertex v)
		{
			if (StartVertex!=null)
				StartVertex(this, new VertexEventArgs<Vertex>(v));
		}

		public event VertexEventHandler<Vertex> DiscoverVertex;
		private void OnDiscoverVertex(Vertex v)
		{
			if (DiscoverVertex!=null)
				DiscoverVertex(this, new VertexEventArgs<Vertex>(v));
		}

		public event EdgeEventHandler<Vertex,Edge> ExamineEdge;
		private void OnExamineEdge(Edge e)
		{
			if (ExamineEdge!=null)
				ExamineEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> TreeEdge;
		private void OnTreeEdge(Edge e)
		{
			if (TreeEdge!=null)
				TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> BackEdge;
		private void OnBackEdge(Edge e)
		{
			if (BackEdge!=null)
				BackEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event EdgeEventHandler<Vertex,Edge> ForwardOrCrossEdge;
		private void OnForwardOrCrossEdge(Edge e)
		{
			if (ForwardOrCrossEdge!=null)
				ForwardOrCrossEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
		}

		public event VertexEventHandler<Vertex> FinishVertex;
		private void OnFinishVertex(Vertex v)
		{
			if (FinishVertex!=null)
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

		public void Visit(Vertex root, int depth)
		{
			if (root==null)
				throw new ArgumentNullException("root");

            Stack<Vertex> todo = new Stack<Vertex>(this.VisitedGraph.VertexCount / 4);
            todo.Push(root);

            while (todo.Count > 0)
            {
                if (this.IsAborting)
                    return;

                Vertex u = todo.Pop();
                VertexColors[u] = GraphColor.Gray;
                OnDiscoverVertex(u);

                Vertex v = default(Vertex);
                foreach (Edge e in VisitedGraph.OutEdges(u))
                {
                    if (this.IsAborting)
                        return;
                    OnExamineEdge(e);
                    v = e.Target;
                    GraphColor c = VertexColors[v];
                    if (c == GraphColor.White)
                    {
                        OnTreeEdge(e);
                        if (depth < this.MaxDepth)
                            todo.Push(v);
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
                VertexColors[u] = GraphColor.Black;
                OnFinishVertex(u);
            }
		}
    }
}
