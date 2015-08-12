using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.MaximumFlow;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph.Algorithms
{
    public sealed class MaximumBipartiteMatchingAlgorithm<TVertex, TEdge>
        : AlgorithmBase<IMutableVertexAndEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        public MaximumBipartiteMatchingAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IEnumerable<TVertex> vertexSetA,
            IEnumerable<TVertex> vertexSetB,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex, TEdge> edgeFactory
            )
            : base(visitedGraph)
        {
            Contract.Requires(vertexFactory != null);
            Contract.Requires(edgeFactory != null);

            this.VertexSetA = vertexSetA;
            this.VertexSetB = vertexSetB;
            this.VertexFactory = vertexFactory;
            this.EdgeFactory = edgeFactory;
            this.MatchedEdges = new List<TEdge>();
        }

        public IEnumerable<TVertex> VertexSetA { get; private set; }
        public IEnumerable<TVertex> VertexSetB { get; private set; }
        public VertexFactory<TVertex> VertexFactory { get; private set; }
        public EdgeFactory<TVertex, TEdge> EdgeFactory { get; private set; }
        public ICollection<TEdge> MatchedEdges { get; private set; }

        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;
            this.MatchedEdges.Clear();

            BipartiteToMaximumFlowGraphAugmentorAlgorithm<TVertex, TEdge> augmentor = null;
            ReversedEdgeAugmentorAlgorithm<TVertex, TEdge> reverser = null;

            try
            {
                if (cancelManager.IsCancelling)
                    return;

//#if !SILVERLIGHT
//                this.VisitedGraph.OpenAsDGML("before.dgml");
//#endif

                //augmenting graph
                augmentor = new BipartiteToMaximumFlowGraphAugmentorAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    this.VertexSetA,
                    this.VertexSetB,
                    this.VertexFactory,
                    this.EdgeFactory);
                augmentor.Compute();

                if (cancelManager.IsCancelling)
                    return;

//#if !SILVERLIGHT
//                this.VisitedGraph.OpenAsDGML("afteraugment.dgml");
//#endif
                //adding reverse edges
                reverser = new ReversedEdgeAugmentorAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    this.EdgeFactory
                    );
                reverser.AddReversedEdges();
                if (cancelManager.IsCancelling)
                    return;

//#if !SILVERLIGHT
//                this.VisitedGraph.OpenAsDGML("afterreversal.dgml");
//#endif

                // compute maxflow
                var flow = new EdmondsKarpMaximumFlowAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    e => 1,
                    this.EdgeFactory
                    );

                flow.Compute(augmentor.SuperSource, augmentor.SuperSink);

                if (cancelManager.IsCancelling)
                    return;



                foreach (var edge in this.VisitedGraph.Edges)
                {
                    if (flow.ResidualCapacities[edge] == 0)
                    {
                        if (edge.Source.Equals(augmentor.SuperSource) ||
                            edge.Source.Equals(augmentor.SuperSource) ||
                            edge.Target.Equals(augmentor.SuperSink) ||
                            edge.Target.Equals(augmentor.SuperSink))
                        {
                            //Skip all edges that connect to SuperSource or SuperSink
                            continue;
                        }

                        this.MatchedEdges.Add(edge);
                    }
                }
            }
            finally
            {
                if (reverser != null && reverser.Augmented)
                {
                    reverser.RemoveReversedEdges();
                    reverser = null;
                }
                if (augmentor != null && augmentor.Augmented)
                {
                    augmentor.Rollback();
                    augmentor = null;
                }
            }
        }

    }
}
