using System;
using System.Collections.Generic;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Predicates;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MaximumFlow
{
    /// <summary>
    /// Edmond and Karp maximum flow algorithm
    /// for directed graph with positive capacities and
    /// flows.
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    [Serializable]
    public sealed class EdmondsKarpMaximumFlowAlgorithm<TVertex, TEdge>
        : MaximumFlowAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        public EdmondsKarpMaximumFlowAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            Func<TEdge, double> capacities,
            IDictionary<TEdge, TEdge> reversedEdges
            )
            : this(null, g, capacities, reversedEdges)
        { }

		public EdmondsKarpMaximumFlowAlgorithm(
            IAlgorithmComponent host,
			IVertexListGraph<TVertex,TEdge> g,
			Func<TEdge,double> capacities,
			IDictionary<TEdge,TEdge> reversedEdges
			)
			: base(host, g,capacities,reversedEdges)
		{}
	
		private IVertexListGraph<TVertex,TEdge> ResidualGraph
		{
			get
			{
				return new FilteredVertexListGraph<
                        TVertex,
                        TEdge,
                        IVertexListGraph<TVertex,TEdge>
                        >(
        					VisitedGraph,
                            new AnyVertexPredicate<TVertex>().Test,
				        	new ResidualEdgePredicate<TVertex,TEdge>(ResidualCapacities).Test
    					);
			}
		}
	
		private void Augment(
			TVertex src,
			TVertex sink
			)
		{
			TEdge e;
			TVertex u;

			// find minimum residual capacity along the augmenting path
			double delta = double.MaxValue;
            u = sink;
            do
			{
                e = Predecessors[u];
                delta = Math.Min(delta, ResidualCapacities[e]);
                u = e.Source;
			} while (!u.Equals(src));

			// push delta units of flow along the augmenting path
            u = sink;
            do 
			{
                e = Predecessors[u];
                ResidualCapacities[e] -= delta;
                ResidualCapacities[ ReversedEdges[e] ] += delta;
				u = e.Source;
			} while (!u.Equals(src));
		}
    
		/// <summary>
		/// Computes the maximum flow between <paramref name="src"/> and
		/// <paramref name="sink"/>
		/// </summary>
		/// <param name="src"></param>
		/// <param name="sink"></param>
		/// <returns></returns>
		protected override void InternalCompute()
		{
			if (this.Source==null)
				throw new InvalidOperationException("Source is not specified");
			if (this.Sink==null)
                throw new InvalidOperationException("Sink is not specified");

            foreach(var u in VisitedGraph.Vertices)
				foreach(var e in this.VisitedGraph.OutEdges(u))
					this.ResidualCapacities[e] = this.Capacities(e);   			
    
			this.VertexColors[Sink] = GraphColor.Gray;
			while (this.VertexColors[Sink] != GraphColor.White)
			{
                var vis = new VertexPredecessorRecorderObserver<TVertex,TEdge>(
                    Predecessors
					);
				var Q = new QuickGraph.Collections.Queue<TVertex>();
				var bfs = new BreadthFirstSearchAlgorithm<TVertex,TEdge>(
					ResidualGraph,
					Q,
					VertexColors
					);
                using (ObserverScope.Create(bfs, vis))
                    bfs.Compute(this.Source);

                if (this.VertexColors[this.Sink] != GraphColor.White)
					Augment(this.Source, this.Sink);
			} // while

            this.MaxFlow = 0;
            foreach(var e in this.VisitedGraph.OutEdges(Source))
				this.MaxFlow += (this.Capacities(e) - this.ResidualCapacities[e]);
		} 
	}

}