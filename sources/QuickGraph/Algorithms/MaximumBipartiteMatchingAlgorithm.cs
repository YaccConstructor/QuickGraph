using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.MaximumFlow;

namespace QuickGraph.Algorithms
{
    public sealed class MaximumBipartiteMatchingAlgorithm<Vertex,Edge> :
        AlgorithmBase<IMutableVertexAndEdgeListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private IVertexFactory<Vertex> vertexFactory;
        private IEdgeFactory<Vertex, Edge> edgeFactory;
        private IList<Edge> matchedEdges = new List<Edge>();

        public MaximumBipartiteMatchingAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<Vertex>(),
                FactoryCompiler.GetEdgeFactory<Vertex, Edge>()
                )
        { }

        public MaximumBipartiteMatchingAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex,Edge> visitedGraph,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory
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

        public IVertexFactory<Vertex> VertexFactory
        {
            get { return this.vertexFactory; }
        }

        public IEdgeFactory<Vertex, Edge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public ICollection<Edge> MatchedEdges
        {
            get { return this.matchedEdges; }
        }

        protected override void InternalCompute()
        {
            this.matchedEdges.Clear();
            AllVerticesGraphAugmentorAlgorithm<Vertex, Edge> augmentor=null;
            ReversedEdgeAugmentorAlgorithm<Vertex,Edge> reverser=null;
            try
            {
                if (this.IsAborting)
                    return;

                //augmenting graph
                augmentor = new AllVerticesGraphAugmentorAlgorithm<Vertex, Edge>(
                    this.VisitedGraph,
                    this.VertexFactory,
                    this.EdgeFactory);
                augmentor.Compute();
                if (this.IsAborting)
                    return;


                // adding reverse edges
                reverser = new ReversedEdgeAugmentorAlgorithm<Vertex,Edge>(
                    this.VisitedGraph,
                    this.EdgeFactory
                    );
                reverser.AddReversedEdges();
                if (this.IsAborting)
                    return;


                // compute maxflow
                EdmondsKarpMaximumFlowAlgorithm<Vertex, Edge> flow = new EdmondsKarpMaximumFlowAlgorithm<Vertex, Edge>(
                    this.VisitedGraph,
                    AlgoUtility.ConstantCapacities(this.VisitedGraph, 1),
                    reverser.ReversedEdges
                    );
                flow.Compute(augmentor.SuperSource, augmentor.SuperSink);
                if (this.IsAborting)
                    return;


                foreach (Edge edge in this.VisitedGraph.Edges)
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
