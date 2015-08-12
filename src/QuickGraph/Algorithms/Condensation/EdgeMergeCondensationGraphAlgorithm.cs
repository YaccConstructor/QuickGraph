using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Condensation
{
    public sealed class EdgeMergeCondensationGraphAlgorithm<TVertex,TEdge> :
        AlgorithmBase<IBidirectionalGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IMutableBidirectionalGraph<
            TVertex, 
            MergedEdge<TVertex,TEdge>
            > condensatedGraph;
        private VertexPredicate<TVertex> vertexPredicate;

        public EdgeMergeCondensationGraphAlgorithm(
                IBidirectionalGraph<TVertex, TEdge> visitedGraph,
                IMutableBidirectionalGraph<TVertex, MergedEdge<TVertex,TEdge>> condensatedGraph,
                VertexPredicate<TVertex> vertexPredicate
            )
            :base(visitedGraph)
        {
            Contract.Requires(condensatedGraph != null);
            Contract.Requires(vertexPredicate != null);

            this.condensatedGraph = condensatedGraph;
            this.vertexPredicate = vertexPredicate;
        }

        public IMutableBidirectionalGraph<TVertex,
            MergedEdge<TVertex,TEdge>
            > CondensatedGraph
        {
            get { return this.condensatedGraph; }
        }

        public VertexPredicate<TVertex> VertexPredicate
        {
            get { return this.vertexPredicate; }
        }

        protected override void InternalCompute()
        {
            // adding vertices to the new graph
            // and pusing filtered vertices in queue
            Queue<TVertex> filteredVertices = new Queue<TVertex>();
            foreach (var v in this.VisitedGraph.Vertices)
            {
                this.CondensatedGraph.AddVertex(v);
                if (!this.VertexPredicate(v))
                    filteredVertices.Enqueue(v);
            }

            // adding all edges
            foreach (var edge in this.VisitedGraph.Edges)
            {
                MergedEdge<TVertex, TEdge> mergedEdge = new MergedEdge<TVertex, TEdge>(edge.Source, edge.Target);
                mergedEdge.Edges.Add(edge);

                this.CondensatedGraph.AddEdge(mergedEdge);
            }

            // remove vertices
            while (filteredVertices.Count > 0)
            {
                TVertex filteredVertex = filteredVertices.Dequeue();

                // do the cross product between inedges and outedges
                MergeVertex(filteredVertex);
            }
        }

        private void MergeVertex(TVertex v)
        {
            // get in edges and outedge
            List<MergedEdge<TVertex, TEdge>> inEdges =
                new List<MergedEdge<TVertex, TEdge>>(this.CondensatedGraph.InEdges(v));
            List<MergedEdge<TVertex, TEdge>> outEdges =
                new List<MergedEdge<TVertex, TEdge>>(this.CondensatedGraph.OutEdges(v));

            // remove vertex
            this.CondensatedGraph.RemoveVertex(v);

            // add condensated edges
            for (int i = 0; i < inEdges.Count; ++i)
            {
                MergedEdge<TVertex, TEdge> inEdge = inEdges[i];
                if (inEdge.Source.Equals(v))
                    continue;

                for (int j = 0; j < outEdges.Count; ++j)
                {
                    MergedEdge<TVertex, TEdge> outEdge = outEdges[j];
                    if (outEdge.Target.Equals(v))
                        continue;

                    MergedEdge<TVertex, TEdge> newEdge =
                        MergedEdge<TVertex, TEdge>.Merge(inEdge, outEdge);
                    this.CondensatedGraph.AddEdge(newEdge);
                }
            }
        }
    }
}
