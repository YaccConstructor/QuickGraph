using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class StronglyConnectedComponentsAlgorithm<Vertex, Edge> :
        AlgorithmBase<IVertexListGraph<Vertex, Edge>>,
        IConnectedComponentAlgorithm<Vertex,Edge,IVertexListGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
		private IDictionary<Vertex,int> components;
		private IDictionary<Vertex,int> discoverTimes;
		private IDictionary<Vertex,Vertex> roots;
		private Stack<Vertex> stack;
		int componentCount;
		int dfsTime;
        private DepthFirstSearchAlgorithm<Vertex, Edge> dfs;

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<Vertex,Edge> g)
            :this(g, new Dictionary<Vertex,int>())
		{}

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<Vertex,Edge> g,
			IDictionary<Vertex,int> components)
            :base(g)
		{
			if (components==null)
				throw new ArgumentNullException("components");

			this.components = components;
            this.roots = new Dictionary<Vertex, Vertex>();
            this.discoverTimes = new Dictionary<Vertex, int>();
            this.stack = new Stack<Vertex>();
			this.componentCount = 0;
			this.dfsTime = 0;
            this.dfs = new DepthFirstSearchAlgorithm<Vertex, Edge>(VisitedGraph);
            this.dfs.DiscoverVertex += new VertexEventHandler<Vertex>(this.DiscoverVertex);
            this.dfs.FinishVertex += new VertexEventHandler<Vertex>(this.FinishVertex);
        }

		public IDictionary<Vertex,int> Components
		{
			get
			{
				return this.components;
			}
		}

		public IDictionary<Vertex,Vertex> Roots
		{
			get
			{
				return this.roots;
			}
		}

        public IDictionary<Vertex, int> DiscoverTimes
        {
			get
			{
				return this.discoverTimes;
			}
		}

		public int ComponentCount
		{
			get
			{
				return this.componentCount;
			}
		}

		private void DiscoverVertex(Object sender, VertexEventArgs<Vertex> args)
		{
			Vertex v = args.Vertex;
			this.Roots[v]=v;
			this.Components[v]=int.MaxValue;
			this.DiscoverTimes[v]=dfsTime++;
			this.stack.Push(v);
		}

		/// <summary>
		/// Used internally
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void FinishVertex(Object sender, VertexEventArgs<Vertex> args)
		{
			Vertex v = args.Vertex;
			foreach(Edge e in VisitedGraph.OutEdges(v))
			{
				Vertex w = e.Target;
				if (this.Components[w] == int.MaxValue)
					this.Roots[v]=MinDiscoverTime(this.Roots[v], this.Roots[w]);
			}

			if (Roots[v].Equals(v)) 
			{
				Vertex w=default(Vertex);
				do 
				{
					w = this.stack.Pop(); 
					this.Components[w]=componentCount;
				} 
				while (!w.Equals(v));
				++componentCount;
			}	
		}

		private Vertex MinDiscoverTime(Vertex u, Vertex v)
		{
			if (this.DiscoverTimes[u]<this.DiscoverTimes[v])
				return u;
			else
				return v;
		}

        public override void Abort()
        {
            this.dfs.Abort();
            base.Abort();
        }

		protected override void InternalCompute()
		{
			this.Components.Clear();
			this.Roots.Clear();
			this.DiscoverTimes.Clear();
			componentCount = 0;
			dfsTime = 0;

			dfs.Compute();
		}
    }
}
