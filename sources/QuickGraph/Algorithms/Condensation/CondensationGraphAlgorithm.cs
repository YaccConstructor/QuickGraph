using System;
using System.Collections.Generic;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Condensation
{
    public sealed class CondensationGraphAlgorithm<
            Vertex,
            Edge,
            Graph
            > :
        AlgorithmBase<IVertexAndEdgeListGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
        where Graph : IMutableVertexAndEdgeListGraph<Vertex, Edge>, new()
    {
        private bool stronglyConnected = true;
        private IConnectedComponentAlgorithm<Vertex,Edge,IVertexListGraph<Vertex,Edge>> componentAlgorithm = null;

        private IMutableBidirectionalGraph<
            Graph,
            CondensatedEdge<Vertex, Edge, Graph>
            > condensatedGraph;

        public CondensationGraphAlgorithm(IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph)
            :base(visitedGraph)
        {}

        public IMutableBidirectionalGraph<
            Graph,
            CondensatedEdge<Vertex, Edge,Graph>
            > CondensatedGraph
        {
            get { return this.condensatedGraph; }
        }

        public bool StronglyConnected
        {
            get { return this.stronglyConnected; }
            set { this.stronglyConnected = value; }
        }

        protected override void InternalCompute()
        {
            // create condensated graph
            this.condensatedGraph = new BidirectionalGraph<
                Graph,
                CondensatedEdge<Vertex, Edge,Graph>
                >(false);
            if (this.VisitedGraph.VertexCount == 0)
                return;

            // compute strongly connected components
            Dictionary<Vertex,int> components = new Dictionary<Vertex,int>(this.VisitedGraph.VertexCount);
            int componentCount;
            lock (this.SyncRoot)
            {
                if (this.StronglyConnected)
                    this.componentAlgorithm = new StronglyConnectedComponentsAlgorithm<Vertex, Edge>(this.VisitedGraph, components);
                else
                    this.componentAlgorithm = new WeaklyConnectedComponentsAlgorithm<Vertex, Edge>(this.VisitedGraph, components);
            }
            this.componentAlgorithm.Compute();
            componentCount = this.componentAlgorithm.ComponentCount;
            lock (SyncRoot)
            {
                this.componentAlgorithm = null;
            }
            if (this.IsAborting)
                return;

            // create list vertices
            Dictionary<int, Graph> condensatedVertices = new Dictionary<int, Graph>(componentCount);
            for (int i = 0; i < componentCount; ++i)
            {
                if (this.IsAborting)
                    return;

                Graph v = new Graph();
                condensatedVertices.Add(i, v);
                this.condensatedGraph.AddVertex(v);
            }

            // addingvertices
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                condensatedVertices[components[v]].AddVertex(v);
            }
            if (this.IsAborting)
                return;

            // condensated edges
            Dictionary<EdgeKey, CondensatedEdge<Vertex,Edge,Graph>> condensatedEdges = new Dictionary<EdgeKey,CondensatedEdge<Vertex,Edge,Graph>>(componentCount);

            // iterate over edges and condensate graph
            foreach (Edge edge in this.VisitedGraph.Edges)
            {
                if (this.IsAborting)
                    return;

                // get component ids
                int sourceID = components[edge.Source];
                int targetID = components[edge.Target];

                // get vertices
                Graph sources = condensatedVertices[sourceID];
                if (sourceID == targetID)
                {
                    sources.AddEdge(edge);
                    continue;
                }

                //
                Graph targets = condensatedVertices[targetID];

                // at last add edge
                EdgeKey edgeKey = new EdgeKey(sourceID, targetID);
                CondensatedEdge<Vertex,Edge,Graph> condensatedEdge;
                if (!condensatedEdges.TryGetValue(edgeKey, out condensatedEdge))
                {
                    condensatedEdge = new CondensatedEdge<Vertex, Edge,Graph>(sources, targets);
                    condensatedEdges.Add(edgeKey, condensatedEdge);
                    this.condensatedGraph.AddEdge(condensatedEdge);
                }
                condensatedEdge.Edges.Add(edge);
            }
        }

        public override void Abort()
        {
            if (this.componentAlgorithm != null)
            {
                this.componentAlgorithm.Abort();
                this.componentAlgorithm = null;
            }
            base.Abort();
        }

        private struct EdgeKey : IComparable<EdgeKey>
        {
            int SourceID;
            int TargetID;

            public EdgeKey(int sourceID, int targetID)
            {
                SourceID = sourceID;
                TargetID = targetID;
            }

            public int CompareTo(EdgeKey other)
            {
                int compare = SourceID.CompareTo(other.SourceID);
                if (compare != 0)
                    return compare;
                return TargetID.CompareTo(other.TargetID);                
            }

            public bool Equals(EdgeKey other)
            {
                return SourceID == other.SourceID && TargetID == other.TargetID;
            }
        }
    }
}
