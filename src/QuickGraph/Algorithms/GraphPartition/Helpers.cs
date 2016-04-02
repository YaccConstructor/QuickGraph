using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.KernighanLinAlgoritm
{
    public class GraphWithWeights<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public UndirectedGraph<TVertex, TEdge> graph { get; set; }
        public Dictionary<TEdge, int> edgeWeights { get; set; }
        public GraphWithWeights(UndirectedGraph<TVertex, TEdge> graph, Dictionary<TEdge, int> edgeWeights)
        {
            this.graph = graph;
            this.edgeWeights = edgeWeights;
        }
    }

    public class Partition<TVertex>
    {
        public SortedSet<TVertex> A { get; set; }
        public SortedSet<TVertex> B { get; set; }
        public int cutCost { get; set; }
        public Partition(SortedSet<TVertex> A, SortedSet<TVertex> B, int cutCost = 0)
        {
            this.A = A;
            this.B = B;
            this.cutCost = cutCost;

        }
    }

    public class Pair<T>
    {
        public T first { get; set; }
        public T second { get; set; }

        public Pair(T first, T second)
        {
            this.first = first;
            this.second = second;
        }

    }

}
