/*
 * Code by Yoad Snapir <yoadsn@gmail.com> with a bit of refactoring from YC team
 * Taken from https://github.com/yoadsn/ArrowDiagramGenerator because PR was not opened
 * 
 * */

namespace QuickGraph.Algorithms
{
    using System.Collections.Generic;

    public class TransitiveReductionAlgorithm<TVertex, TEdge> : AlgorithmBase<BidirectionalGraph<TVertex, TEdge>> where TEdge : IEdge<TVertex>
    {
        public TransitiveReductionAlgorithm(
            BidirectionalGraph<TVertex, TEdge> visitedGraph
            )
            : base(visitedGraph)
        {
            TransitiveReduction = new BidirectionalGraph<TVertex, TEdge>();
        }

        public BidirectionalGraph<TVertex, TEdge> TransitiveReduction { get; private set; } //R# will say you do not need this. AppVeyor wants it.

        protected override void InternalCompute()
        {
            // Clone the visited graph
            TransitiveReduction.AddVerticesAndEdgeRange(VisitedGraph.Edges);

            // Iterate in topo order, track indirect ancestors and remove edges from them to the visited vertex
            var ancestorsOfVertices = new Dictionary<TVertex, HashSet<TVertex>>();
            foreach (var vertexId in VisitedGraph.TopologicalSort())
            {
                //TODO think of some heuristic value here. Like (verticesCount / 2) or (verticesCount / 3)
                var thisVertexPredecessors = new List<TVertex>(); 
                var thisVertexAncestors = new HashSet<TVertex>();
                ancestorsOfVertices[vertexId] = thisVertexAncestors;

                // Get indirect ancestors
                foreach (var inEdge in VisitedGraph.InEdges(vertexId))
                {
                    var predecessor = inEdge.Source;
                    thisVertexPredecessors.Add(predecessor);

                    // Add all the ancestors of the predeccessors
                    thisVertexAncestors.UnionWith(ancestorsOfVertices[predecessor]);
                }

                // Remove indirect edges
                foreach (var indirectAncestor in thisVertexAncestors)
                {
                    TEdge foundIndirectEdge;
                    if (TransitiveReduction.TryGetEdge(indirectAncestor, vertexId, out foundIndirectEdge))
                    {
                        TransitiveReduction.RemoveEdge(foundIndirectEdge);
                    }
                }

                // Add predecessors to ancestors list
                thisVertexAncestors.UnionWith(thisVertexPredecessors);
            }
        }
    }
}