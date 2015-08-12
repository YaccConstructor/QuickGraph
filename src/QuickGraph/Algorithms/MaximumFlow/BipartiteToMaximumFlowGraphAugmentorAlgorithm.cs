using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.MaximumFlow
{
    /// <summary>
    /// This algorithm modifies a bipartite graph into a related graph, where each Vertex in one partition is 
    /// connected to a newly added "SuperSource" and each Vertex in the other partition is connected to a newly added "SuperSink"
    /// When the maximum flow of this related graph is computed, the edges used for the flow are also those which make up
    /// the maximum match for the bipartite graph.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    class BipartiteToMaximumFlowGraphAugmentorAlgorithm<TVertex,TEdge>
        : GraphAugmentorAlgorithmBase<TVertex, TEdge, IMutableVertexAndEdgeSet<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        public BipartiteToMaximumFlowGraphAugmentorAlgorithm(
            IMutableVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            IEnumerable<TVertex> vertexSetA,
            IEnumerable<TVertex> vertexSetB,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex, TEdge> edgeFactory
            )
            : this(null, visitedGraph, vertexSetA, vertexSetB, vertexFactory, edgeFactory)
        { }

        public BipartiteToMaximumFlowGraphAugmentorAlgorithm(
            IAlgorithmComponent host,
            IMutableVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            IEnumerable<TVertex> vertexSetA,
            IEnumerable<TVertex> vertexSetB,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory
            )
            : base(host, visitedGraph,vertexFactory,edgeFactory)
        {
            this.VertexSetA = vertexSetA;
            this.VertexSetB = vertexSetB;
        }

        public IEnumerable<TVertex> VertexSetA { get; private set; }
        public IEnumerable<TVertex> VertexSetB { get; private set; }


        protected override void AugmentGraph()
        {
            var cancelManager = this.Services.CancelManager;
            foreach (var v in this.VertexSetA)
            {
                if (cancelManager.IsCancelling) break;

                this.AddAugmentedEdge(this.SuperSource, v);
            }

            foreach (var v in this.VertexSetB)
            {
                if (cancelManager.IsCancelling) break;

                this.AddAugmentedEdge(v, this.SuperSink);
            }
        }
    }
}
