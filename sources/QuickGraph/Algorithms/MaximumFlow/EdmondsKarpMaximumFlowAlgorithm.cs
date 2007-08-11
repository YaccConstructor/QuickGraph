using System;
using System.Collections.Generic;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Predicates;
using QuickGraph.Algorithms.Search;

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
    public sealed class EdmondsKarpMaximumFlowAlgorithm<Vertex, Edge>
        : MaximumFlowAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
		public EdmondsKarpMaximumFlowAlgorithm(
			IVertexListGraph<Vertex,Edge> g,
			IDictionary<Edge,double> capacities,
			IDictionary<Edge,Edge> reversedEdges
			)
			: base(g,capacities,reversedEdges)
		{}
	
		private IVertexListGraph<Vertex,Edge> ResidualGraph
		{
			get
			{
				return new FilteredVertexListGraph<
                        Vertex,
                        Edge,
                        IVertexListGraph<Vertex,Edge>
                        >(
        					VisitedGraph,
                            new AnyVertexPredicate<Vertex>().Test,
				        	new ResidualEdgePredicate<Vertex,Edge>(ResidualCapacities).Test
    					);
			}
		}
	
		private void Augment(
			Vertex src,
			Vertex sink
			)
		{
			Edge e;
			Vertex u;

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

            foreach(Vertex u in VisitedGraph.Vertices)
			{
				foreach(Edge e in VisitedGraph.OutEdges(u))
				{
					ResidualCapacities[e] = Capacities[e];   			
				}
			}
    
			VertexColors[Sink] = GraphColor.Gray;
			while (VertexColors[Sink] != GraphColor.White)
			{
                VertexPredecessorRecorderObserver<Vertex,Edge> vis = new VertexPredecessorRecorderObserver<Vertex,Edge>(
                    Predecessors
					);
				VertexBuffer<Vertex> Q = new VertexBuffer<Vertex>();
				BreadthFirstSearchAlgorithm<Vertex,Edge> bfs = new BreadthFirstSearchAlgorithm<Vertex,Edge>(
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
            foreach(Edge e in VisitedGraph.OutEdges(Source))
				this.MaxFlow += (Capacities[e] - ResidualCapacities[e]);
		} 
	}

}