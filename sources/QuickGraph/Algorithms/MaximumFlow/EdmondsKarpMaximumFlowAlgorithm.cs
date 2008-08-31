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
            IDictionary<TEdge, double> capacities,
            IDictionary<TEdge, TEdge> reversedEdges
            )
            : this(null, g, capacities, reversedEdges)
        { }

		public EdmondsKarpMaximumFlowAlgorithm(
            IAlgorithmComponent host,
			IVertexListGraph<TVertex,TEdge> g,
			IDictionary<TEdge,double> capacities,
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

            foreach(TVertex u in VisitedGraph.Vertices)
			{
				foreach(TEdge e in VisitedGraph.OutEdges(u))
				{
					ResidualCapacities[e] = Capacities[e];   			
				}
			}
    
			VertexColors[Sink] = GraphColor.Gray;
			while (VertexColors[Sink] != GraphColor.White)
			{
                VertexPredecessorRecorderObserver<TVertex,TEdge> vis = new VertexPredecessorRecorderObserver<TVertex,TEdge>(
                    Predecessors
					);
				var Q = new QuickGraph.Collections.Queue<TVertex>();
				var bfs = new BreadthFirstSearchAlgorithm<TVertex,TEdge>(
					ResidualGraph,
					Q,
					VertexColors
					);
                vis.Attach(bfs);
                bfs.Compute(this.Source);
                vis.Detach(bfs);

                if (VertexColors[this.Sink] != GraphColor.White)
					Augment(this.Source, this.Sink);
			} // while

            this.MaxFlow = 0;
            foreach(TEdge e in VisitedGraph.OutEdges(Source))
				this.MaxFlow += (Capacities[e] - ResidualCapacities[e]);
		} 
	}

}