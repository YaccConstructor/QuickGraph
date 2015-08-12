using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Algorithms.ConnectedComponents
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class StronglyConnectedComponentsAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>,
        IConnectedComponentAlgorithm<TVertex,TEdge,IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
		private readonly IDictionary<TVertex,int> components;
		private readonly Dictionary<TVertex,int> discoverTimes;
		private readonly Dictionary<TVertex,TVertex> roots;
		private Stack<TVertex> stack;
		int componentCount;
		int dfsTime;

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<TVertex,TEdge> g)
            :this(g, new Dictionary<TVertex,int>())
		{}

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            : this(null, g, components)
        { }

        public StronglyConnectedComponentsAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex,TEdge> g,
			IDictionary<TVertex,int> components)
            :base(host, g)
		{
            Contract.Requires(components != null);

			this.components = components;
            this.roots = new Dictionary<TVertex, TVertex>();
            this.discoverTimes = new Dictionary<TVertex, int>();
            this.stack = new Stack<TVertex>();
			this.componentCount = 0;
			this.dfsTime = 0;
        }

		public IDictionary<TVertex,int> Components
		{
			get
			{
				return this.components;
			}
		}

		public IDictionary<TVertex,TVertex> Roots
		{
			get
			{
				return this.roots;
			}
		}

        public IDictionary<TVertex, int> DiscoverTimes
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

		private void DiscoverVertex(TVertex v)
		{
			this.Roots[v]=v;
			this.Components[v]=int.MaxValue;
			this.DiscoverTimes[v]=dfsTime++;
			this.stack.Push(v);
		}

		/// <summary>
		/// Used internally
		/// </summary>
		private void FinishVertex(TVertex v)
		{
            var roots = this.Roots;

			foreach(var e in this.VisitedGraph.OutEdges(v))
			{
				var w = e.Target;
				if (this.Components[w] == int.MaxValue)
					roots[v] = this.MinDiscoverTime(roots[v], roots[w]);
			}

			if (this.roots[v].Equals(v)) 
			{
				var w = default(TVertex);
				do 
				{
					w = this.stack.Pop(); 
					this.Components[w] = componentCount;
				} 
				while (!w.Equals(v));
				++componentCount;
			}	
		}

		private TVertex MinDiscoverTime(TVertex u, TVertex v)
		{
            Contract.Requires(u != null);
            Contract.Requires(v != null);
            Contract.Ensures(this.DiscoverTimes[u] < this.DiscoverTimes[v] 
                ? Contract.Result<TVertex>().Equals(u) 
                : Contract.Result<TVertex>().Equals(v)
                );

			if (this.discoverTimes[u] < this.discoverTimes[v])
				return u;
			else
				return v;
		}

		protected override void InternalCompute()
		{
            Contract.Ensures(this.ComponentCount >= 0);
            Contract.Ensures(this.VisitedGraph.VertexCount == 0 || this.ComponentCount > 0); 
            Contract.Ensures(Enumerable.All(this.VisitedGraph.Vertices, v => this.Components.ContainsKey(v)));
            Contract.Ensures(this.VisitedGraph.VertexCount == this.Components.Count);
            Contract.Ensures(Enumerable.All(this.Components.Values, c => c <= this.ComponentCount));

			this.Components.Clear();
			this.Roots.Clear();
			this.DiscoverTimes.Clear();
            this.stack.Clear();
			this.componentCount = 0;
			this.dfsTime = 0;

            DepthFirstSearchAlgorithm<TVertex, TEdge> dfs = null;
            try
            {
                dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(
                    this, 
                    VisitedGraph,
                    new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount)
                    );
                dfs.DiscoverVertex += new VertexAction<TVertex>(this.DiscoverVertex);
                dfs.FinishVertex += new VertexAction<TVertex>(this.FinishVertex);

                dfs.Compute();
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.DiscoverVertex -= new VertexAction<TVertex>(this.DiscoverVertex);
                    dfs.FinishVertex -= new VertexAction<TVertex>(this.FinishVertex);
                }
            }
		}
    }
}
