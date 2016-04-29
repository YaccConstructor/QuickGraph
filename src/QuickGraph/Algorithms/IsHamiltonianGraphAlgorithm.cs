using System.Linq;

namespace QuickGraph.Algorithms
{
    public class IsHamiltonianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;

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
            //Remove loops
            EdgePredicate<TVertex, UndirectedEdge<TVertex>> isLoop = e => e.Source.Equals(e.Target);
            newGraph.RemoveEdgeIf(isLoop);
            this.graph = newGraph;
        }

        public bool IsHamiltonian()
        {
            // Using Dirac's theorem: if |vertices| >= 2 and for any vertex deg(vertex) >= (|vertices| / 2) then graph is Hamiltonian
            int n = graph.VertexCount;
            if (n == 0)
            {
                return false;
            }
            else if (n == 1)
            {
                return true;
            }
            else
            {
                double threshold = n / 2.0;
                foreach (var v in graph.Vertices)
                {
                    if (graph.AdjacentEdges(v).Count() < threshold)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}