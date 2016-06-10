﻿using System.Linq;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public class IsHamiltonianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;
        private double threshold;
        public List<List<TVertex>> permutations;

        private void Swap(IList<TVertex> list, int indexA, int indexB)
        {
            TVertex tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public void GetPermutations()
        {
            IEnumerable<TVertex> list = graph.Vertices;
            GetPermutations(list.ToList(), 0, list.Count() - 1);
        }

        private void GetPermutations(List<TVertex> list, int recursionDepth, int maxDepth)
        {
            if (recursionDepth == maxDepth)
            {
                permutations.Add(new List<TVertex>(list));
            }
            else
                for (int i = recursionDepth; i <= maxDepth; i++)
                {
                    Swap(list, recursionDepth, i);
                    GetPermutations(list, recursionDepth + 1, maxDepth);
                    Swap(list, recursionDepth, i);
                }
        }

        public IsHamiltonianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            // Create new graph without parallel edges
            var newGraph = new UndirectedGraph<TVertex, UndirectedEdge<TVertex>>(false, graph.EdgeEqualityComparer);
            newGraph.AddVertexRange(graph.Vertices);
            newGraph.AddEdgeRange(graph.Edges);
            // Remove loops
            EdgePredicate<TVertex, UndirectedEdge<TVertex>> isLoop = e => e.Source.Equals(e.Target);
            newGraph.RemoveEdgeIf(isLoop);
            this.graph = newGraph;
            permutations = new List<List<TVertex>>();
            threshold = newGraph.VertexCount / 2.0;
        }

        private bool existsInGraph(List<TVertex> path)
        {
            if (path.Count > 1)
            {
                path.Add(path[0]);      // make cycle, not simple path
            }
            for (int i = 0; i < path.Count() - 1; i++)
            {
                if (!graph.AdjacentVertices(path[i]).Contains(path[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool satisfiesDiracsTheorem(TVertex vertex)
        {
            // Using Dirac's theorem: if |vertices| >= 3 and for any vertex deg(vertex) >= (|vertices| / 2) then graph is Hamiltonian 
            return graph.AdjacentEdges(vertex).Count() >= threshold;
        }

        public bool IsHamiltonian()
        {
            if (graph.VertexCount >= 3 && graph.Vertices.All<TVertex>(satisfiesDiracsTheorem))
            {
                return true;
            }
            GetPermutations();  // generate all possible cycles through all vertices 
            return permutations.Any<List<TVertex>>(existsInGraph);
        }
    }
}