using System.Linq;

namespace QuickGraph.Algorithms
{
    public class IsHamiltonianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;
        private double threshold;

        public IsHamiltonianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            // Create new graph without parallel edges
            var newGraph = new UndirectedGraph<TVertex, UndirectedEdge<TVertex>>(false, graph.EdgeEqualityComparer);
            foreach (var vertex in graph.Vertices)
            {
                newGraph.AddVertex(vertex);
            }
            foreach (var edge in graph.Edges)
            {
                newGraph.AddEdge(edge);
            }
            // Remove loops
            EdgePredicate<TVertex, UndirectedEdge<TVertex>> isLoop = e => e.Source.Equals(e.Target);
            newGraph.RemoveEdgeIf(isLoop);
            this.graph = newGraph;
            threshold = newGraph.VertexCount / 2.0;
        }

        public bool satisfiesHamiltonianCondition(TVertex vertex)
        {
            return graph.AdjacentEdges(vertex).Count() >= threshold;
        }

        public bool IsHamiltonian()
        {
            // Using Dirac's theorem: if |vertices| >= 2 and for any vertex deg(vertex) >= (|vertices| / 2) then graph is Hamiltonian
            int n = graph.VertexCount;
            return (n != 0) && ((n == 1) || graph.Vertices.All<TVertex>(satisfiesHamiltonianCondition));
        }
    }
}