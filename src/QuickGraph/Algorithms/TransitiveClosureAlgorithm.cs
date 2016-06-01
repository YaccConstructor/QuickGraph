using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public class TransitiveClosureAlgorithm<TVertex, TEdge> : AlgorithmBase<BidirectionalGraph<TVertex, TEdge>> where TEdge : IEdge<TVertex>
    {
        public TransitiveClosureAlgorithm(
            BidirectionalGraph<TVertex, TEdge> visitedGraph
            )
            : base(visitedGraph)
        {
            TransitiveClosure = new BidirectionalGraph<TVertex, TEdge>();
        }

        public BidirectionalGraph<TVertex, TEdge> TransitiveClosure { get; }

        protected override void InternalCompute()
        {
            // Clone the visited graph
            TransitiveClosure.AddVerticesAndEdgeRange(VisitedGraph.Edges);

            // Iterate in topo order, track indirect ancestors and remove edges from them to the visited vertex
            var ancestorsOfVertices = new Dictionary<TVertex, HashSet<TVertex>>();
            foreach (var vertexId in VisitedGraph.TopologicalSort())
            {
                var thisVertexPredecessors = new List<TVertex>();
                var thisVertexAncestors = new HashSet<TVertex>();
                ancestorsOfVertices[vertexId] = thisVertexAncestors;

                // Get indirect ancestors
                foreach (var inEdge in VisitedGraph.InEdges(vertexId))
                {
                    var predecessor = inEdge.Source;
                    thisVertexPredecessors.Add(predecessor);

                    // Add all the ancestors of the predeccessors
                    foreach (var ancestorId in ancestorsOfVertices[predecessor])
                    {
                        thisVertexAncestors.Add(ancestorId);
                    }
                }

                // Add indirect edges
                foreach (var indirectAncestor in thisVertexAncestors)
                {
                    if (!TransitiveClosure.ContainsEdge(indirectAncestor, vertexId))
                    {
                        //this is probably not good
                        TransitiveClosure.AddEdge((TEdge) Activator.CreateInstance(typeof(TEdge), indirectAncestor, vertexId));
                    }
                }
                

                // Add predecessors to ancestors list
                foreach (var pred in thisVertexPredecessors)
                {
                    thisVertexAncestors.Add(pred);
                }
            }
        }
    }
}
