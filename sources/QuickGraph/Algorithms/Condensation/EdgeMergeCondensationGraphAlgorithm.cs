using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Condensation
{
    public sealed class EdgeMergeCondensationGraphAlgorithm<Vertex,Edge> :
        AlgorithmBase<IBidirectionalGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IMutableBidirectionalGraph<
            Vertex, 
            MergedEdge<Vertex,Edge>
            > condensatedGraph;
        private VertexPredicate<Vertex> vertexPredicate;

        public EdgeMergeCondensationGraphAlgorithm(
                IBidirectionalGraph<Vertex, Edge> visitedGraph,
                IMutableBidirectionalGraph<Vertex, MergedEdge<Vertex,Edge>> condensatedGraph,
                VertexPredicate<Vertex> vertexPredicate
            )
            :base(visitedGraph)
        {
            if (condensatedGraph == null)
                throw new ArgumentNullException("condensatedGraph");
            if (vertexPredicate == null)
                throw new ArgumentNullException("vertexPredicate");

            this.condensatedGraph = condensatedGraph;
            this.vertexPredicate = vertexPredicate;
        }

        public IMutableBidirectionalGraph<Vertex,
            MergedEdge<Vertex,Edge>
            > CondensatedGraph
        {
            get { return this.condensatedGraph; }
        }

        public VertexPredicate<Vertex> VertexPredicate
        {
            get { return this.vertexPredicate; }
        }

        protected override void InternalCompute()
        {
            // adding vertices to the new graph
            // and pusing filtered vertices in queue
            Queue<Vertex> filteredVertices = new Queue<Vertex>();
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                this.CondensatedGraph.AddVertex(v);
                if (!this.VertexPredicate(v))
                    filteredVertices.Enqueue(v);
            }

            // adding all edges
            foreach (Edge edge in this.VisitedGraph.Edges)
            {
                MergedEdge<Vertex, Edge> mergedEdge = new MergedEdge<Vertex, Edge>(edge.Source, edge.Target);
                mergedEdge.Edges.Add(edge);

                this.CondensatedGraph.AddEdge(mergedEdge);
            }

            // remove vertices
            while (filteredVertices.Count > 0)
            {
                Vertex filteredVertex = filteredVertices.Dequeue();

                // do the cross product between inedges and outedges
                MergeVertex(filteredVertex);
            }
        }

        private void MergeVertex(Vertex v)
        {
            // get in edges and outedge
            List<MergedEdge<Vertex, Edge>> inEdges =
                new List<MergedEdge<Vertex, Edge>>(this.CondensatedGraph.InEdges(v));
            List<MergedEdge<Vertex, Edge>> outEdges =
                new List<MergedEdge<Vertex, Edge>>(this.CondensatedGraph.OutEdges(v));

            // remove vertex
            this.CondensatedGraph.RemoveVertex(v);

            // add condensated edges
            for (int i = 0; i < inEdges.Count; ++i)
            {
                MergedEdge<Vertex, Edge> inEdge = inEdges[i];
                if (inEdge.Source.Equals(v))
                    continue;

                for (int j = 0; j < outEdges.Count; ++j)
                {
                    MergedEdge<Vertex, Edge> outEdge = outEdges[j];
                    if (outEdge.Target.Equals(v))
                        continue;

                    MergedEdge<Vertex, Edge> newEdge =
                        MergedEdge<Vertex, Edge>.Merge(inEdge, outEdge);
                    this.CondensatedGraph.AddEdge(newEdge);
                }
            }
        }
    }
}
