using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms
{
    internal class TransitiveAlgorithmHelper<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        internal TransitiveAlgorithmHelper(
        BidirectionalGraph<TVertex, TEdge> initialGraph
        )
        {
            _graph = initialGraph;
        }

        readonly BidirectionalGraph<TVertex, TEdge> _graph;

        public void InternalCompute(Action<BidirectionalGraph<TVertex, TEdge>, TVertex, TVertex, TEdge> action)
        {

            // Iterate in topo order, track indirect ancestors and remove edges from them to the visited vertex
            var ancestorsOfVertices = new Dictionary<TVertex, HashSet<TVertex>>();
            foreach (var vertexId in _graph.TopologicalSort().ToList()) //making sure we do not mess enumerator or smthn
            {
                //TODO think of some heuristic value here. Like (verticesCount / 2) or (verticesCount / 3)
                var thisVertexPredecessors = new List<TVertex>();
                var thisVertexAncestors = new HashSet<TVertex>();
                ancestorsOfVertices[vertexId] = thisVertexAncestors;

                // Get indirect ancestors
                foreach (var inEdge in _graph.InEdges(vertexId))
                {
                    var predecessor = inEdge.Source;
                    thisVertexPredecessors.Add(predecessor);

                    // Add all the ancestors of the predeccessors
                    thisVertexAncestors.UnionWith(ancestorsOfVertices[predecessor]);
                }

                // Add indirect edges
                foreach (var indirectAncestor in thisVertexAncestors)
                {
                    TEdge foundIndirectEdge;
                    var exists = _graph.TryGetEdge(indirectAncestor, vertexId, out foundIndirectEdge);
                    action(_graph, indirectAncestor, vertexId, foundIndirectEdge);
                }

                // Add predecessors to ancestors list
                thisVertexAncestors.UnionWith(thisVertexPredecessors);
            }
        }

    }
}
