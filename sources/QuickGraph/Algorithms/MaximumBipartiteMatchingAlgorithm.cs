using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.MaximumFlow;

namespace QuickGraph.Algorithms
{
    public sealed class MaximumBipartiteMatchingAlgorithm<TVertex,TEdge> :
        AlgorithmBase<IMutableVertexAndEdgeListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IVertexFactory<TVertex> vertexFactory;
        private IEdgeFactory<TVertex, TEdge> edgeFactory;
        private IList<TEdge> matchedEdges = new List<TEdge>();

        public MaximumBipartiteMatchingAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<TVertex>(),
                FactoryCompiler.GetEdgeFactory<TVertex, TEdge>()
                )
        { }

        public MaximumBipartiteMatchingAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory
            )
            :base(visitedGraph)
        {
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");

            this.vertexFactory = vertexFactory;
            this.edgeFactory = edgeFactory;
        }

        public IVertexFactory<TVertex> VertexFactory
        {
            get { return this.vertexFactory; }
        }

        public IEdgeFactory<TVertex, TEdge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public ICollection<TEdge> MatchedEdges
        {
            get { return this.matchedEdges; }
        }

        protected override void InternalCompute()
        {
            this.matchedEdges.Clear();
            AllVerticesGraphAugmentorAlgorithm<TVertex, TEdge> augmentor=null;
            ReversedEdgeAugmentorAlgorithm<TVertex,TEdge> reverser=null;
            try
            {
                if (this.IsAborting)
                    return;

                //augmenting graph
                augmentor = new AllVerticesGraphAugmentorAlgorithm<TVertex, TEdge>(
                    this.VisitedGraph,
                    this.VertexFactory,
                    this.EdgeFactory);
                augmentor.Compute();
                if (this.IsAborting)
                    return;


                // adding reverse edges
                reverser = new ReversedEdgeAugmentorAlgorithm<TVertex,TEdge>(
                    this.VisitedGraph,
                    this.EdgeFactory
                    );
                reverser.AddReversedEdges();
                if (this.IsAborting)
                    return;


                // compute maxflow
                EdmondsKarpMaximumFlowAlgorithm<TVertex, TEdge> flow = new EdmondsKarpMaximumFlowAlgorithm<TVertex, TEdge>(
                    this.VisitedGraph,
                    AlgoUtility.ConstantCapacities(this.VisitedGraph, 1),
                    reverser.ReversedEdges
                    );
                flow.Compute(augmentor.SuperSource, augmentor.SuperSink);
                if (this.IsAborting)
                    return;


                foreach (TEdge edge in this.VisitedGraph.Edges)
                {
                    if (this.IsAborting)
                        return;

                    if (flow.ResidualCapacities[edge] == 0)
                        this.matchedEdges.Add(edge);
                }
            }
            finally
            {
                if (reverser!=null && reverser.Augmented)
                {                    
                    reverser.RemoveReversedEdges();
                    reverser=null;
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
