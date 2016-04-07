using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms
{
    public class IsHamiltonianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;

        public IsHamiltonianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            this.graph = graph;
        }

        public bool IsHamiltonian()
        {
            // Using Dirac's theorem: if n >= 3 every vertex has degree n / 2 or greater.
            int n = graph.VertexCount;
            if (n >= 3)
            {
                foreach (var v in graph.Vertices)
                {
                    if (!(graph.AdjacentEdges(v).Count() >= n / 2))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
