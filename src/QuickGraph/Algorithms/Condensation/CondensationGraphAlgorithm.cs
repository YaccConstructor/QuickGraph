using System;
using System.Collections.Generic;
using QuickGraph.Collections;
using QuickGraph.Algorithms.ConnectedComponents;

namespace QuickGraph.Algorithms.Condensation
{
    public sealed class CondensationGraphAlgorithm<TVertex,TEdge,TGraph> :
        AlgorithmBase<IVertexAndEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
        where TGraph : IMutableVertexAndEdgeSet<TVertex, TEdge>, new()
    {
        private bool stronglyConnected = true;

        private IMutableBidirectionalGraph<
            TGraph,
            CondensedEdge<TVertex, TEdge, TGraph>
            > condensedGraph;

        public CondensationGraphAlgorithm(IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            :base(visitedGraph)
        {}

        public IMutableBidirectionalGraph<
            TGraph,
            CondensedEdge<TVertex, TEdge,TGraph>
            > CondensedGraph
        {
            get { return this.condensedGraph; }
        }

        public bool StronglyConnected
        {
            get { return this.stronglyConnected; }
            set { this.stronglyConnected = value; }
        }

        protected override void InternalCompute()
        {
            // create condensated graph
            this.condensedGraph = new BidirectionalGraph<
                TGraph,
                CondensedEdge<TVertex, TEdge, TGraph>
                >(false);
            if (this.VisitedGraph.VertexCount == 0)
                return;

            // compute strongly connected components
            var components = new Dictionary<TVertex, int>(this.VisitedGraph.VertexCount);
            int componentCount = ComputeComponentCount(components);

            var cancelManager = this.Services.CancelManager;
            if (cancelManager.IsCancelling) return;

            // create list vertices
            var condensatedVertices = new Dictionary<int, TGraph>(componentCount);
            for (int i = 0; i < componentCount; ++i)
            {
                TGraph v = new TGraph();
                condensatedVertices.Add(i, v);
                this.condensedGraph.AddVertex(v);
            }

            // addingvertices
            foreach (var v in this.VisitedGraph.Vertices)
            {
                condensatedVertices[components[v]].AddVertex(v);
            }
            if (cancelManager.IsCancelling) return;

            // condensated edges
            var condensatedEdges = new Dictionary<EdgeKey, CondensedEdge<TVertex, TEdge, TGraph>>(componentCount);

            // iterate over edges and condensate graph
            foreach (var edge in this.VisitedGraph.Edges)
            {
                // get component ids
                int sourceID = components[edge.Source];
                int targetID = components[edge.Target];

                // get vertices
                TGraph sources = condensatedVertices[sourceID];
                if (sourceID == targetID)
                {
                    sources.AddEdge(edge);
                    continue;
                }

                var targets = condensatedVertices[targetID];
                // at last add edge
                var edgeKey = new EdgeKey(sourceID, targetID);
                CondensedEdge<TVertex, TEdge, TGraph> condensatedEdge;
                if (!condensatedEdges.TryGetValue(edgeKey, out condensatedEdge))
                {
                    condensatedEdge = new CondensedEdge<TVertex, TEdge, TGraph>(sources, targets);
                    condensatedEdges.Add(edgeKey, condensatedEdge);
                    this.condensedGraph.AddEdge(condensatedEdge);
                }
                condensatedEdge.Edges.Add(edge);
            }
        }

        private int ComputeComponentCount(Dictionary<TVertex, int> components)
        {
            IConnectedComponentAlgorithm<TVertex, TEdge, IVertexListGraph<TVertex, TEdge>> componentAlgorithm;
            if (this.StronglyConnected)
                componentAlgorithm = new StronglyConnectedComponentsAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    components);
            else
                componentAlgorithm = new WeaklyConnectedComponentsAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    components);
            componentAlgorithm.Compute();
            return componentAlgorithm.ComponentCount;
        }

        struct EdgeKey 
            : IEquatable<EdgeKey>
        {
            readonly int SourceID;
            readonly int TargetID;

            public EdgeKey(int sourceID, int targetID)
            {
                SourceID = sourceID;
                TargetID = targetID;
            }

            public bool Equals(EdgeKey other)
            {
                return 
                    SourceID == other.SourceID 
                    && TargetID == other.TargetID;
            }

            public override int GetHashCode()
            {
                return HashCodeHelper.Combine(this.SourceID, this.TargetID);
            }
        }
    }
}
