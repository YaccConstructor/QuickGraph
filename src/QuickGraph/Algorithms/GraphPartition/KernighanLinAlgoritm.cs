using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms;

namespace QuickGraph.Algorithms.KernighanLinAlgoritm
{
    public sealed class KernighanLinAlgoritm<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        private GraphWithWeights<TVertex, TEdge> g;
        private int itersNum;
        private int partitionSize;
        private SortedSet<TVertex> A, B;
        private SortedSet<TVertex> unswappedA, unswappedB;


        public KernighanLinAlgoritm(GraphWithWeights<TVertex, TEdge> g, int itersNum)
        {
            this.g = g;
            this.itersNum = itersNum;
            this.partitionSize = g.graph.Vertices.Count() / 2;
        }


        public Partition<TVertex> Execute()
        {

            A = new SortedSet<TVertex>();
            B = new SortedSet<TVertex>();

            getStartPartition();

            unswappedA = new SortedSet<TVertex>(A);
            unswappedB = new SortedSet<TVertex>(B);

            var bestPartition = new Partition<TVertex>(A, B);
            int minCost = int.MaxValue;

            for (int i = 0; i < itersNum; i++)
            {
                var tmpPartition = doAllSwaps();
                var tmpCutCost = tmpPartition.cutCost;

                A = tmpPartition.A;
                B = tmpPartition.B;
                unswappedA = A;
                unswappedB = B;

                if (tmpCutCost < minCost)
                {
                    bestPartition = tmpPartition;
                    minCost = tmpCutCost;
                }
            }

            return bestPartition;
        }


        private Partition<TVertex> doAllSwaps()
        {
            List<Pair<TVertex>> swaps = new List<Pair<TVertex>>();
            int minCost = int.MaxValue;
            int minId = -1;
            // int partSize = 0;
            // if (g.graph.Vertices.Count() != partitionSize * 2) partSize = partitionSize + 1;
            // else partSize = partitionSize;

            for (int i = 0; i < partitionSize; i++)
            {
                int cost = doSingleSwap(swaps);
                if (cost < minCost)
                {
                    minCost = cost; minId = i;
                }
            }

            //Back to swap step with min cutcost
            while (swaps.Count - 1 > minId)
            {
                Pair<TVertex> pair = swaps.Last();
                swaps.Remove(pair);
                swapVertices(A, pair.second, B, pair.first);
            }

            return new Partition<TVertex>(A, B, minCost);
        }


        private int doSingleSwap(List<Pair<TVertex>> swaps)
        {
            Pair<TVertex> maxPair = null;
            int maxGain = int.MinValue;
            foreach (TVertex vertFromA in unswappedA)
                foreach (TVertex vertFromB in unswappedB)
                {
                    TEdge edge = findEdge(vertFromA, vertFromB);
                    int edgeCost = edge != null ? g.edgeWeights[edge] : 0;
                    int gain = getVertexCost(vertFromA) + getVertexCost(vertFromB) - 2 * edgeCost;
                    if (gain > maxGain)
                    {
                        maxPair = new Pair<TVertex>(vertFromA, vertFromB);
                        maxGain = gain;
                    }

                }
            swapVertices(A, maxPair.first, B, maxPair.second);
            swaps.Add(maxPair);
            unswappedA.Remove(maxPair.first);
            unswappedB.Remove(maxPair.second);

            return getCutCost();
        }


        private int getVertexCost(TVertex vert)
        {

            int cost = 0;
            bool vertIsInA = A.Contains(vert);
            var neib = getNeighbors(vert);

            foreach (TVertex vertNeighbord in neib)
            {
                bool vertNeighbordIsInA = A.Contains(vertNeighbord);
                TEdge edge = findEdge(vert, vertNeighbord);
                if (vertIsInA != vertNeighbordIsInA) // external
                    cost += g.edgeWeights[edge];
                else
                    cost -= g.edgeWeights[edge];
            }

            return cost;
        }


        private List<TVertex> getNeighbors(TVertex vert)
        {
            var v = vert.ToString();
            var neibList = new List<TVertex>();
            foreach (TEdge edge in g.graph.Edges)
            {
                if (edge.Source.ToString() == v && !neibList.Contains(edge.Target))
                    neibList.Add(edge.Target);
                if (edge.Target.ToString() == v && !neibList.Contains(edge.Source))
                    neibList.Add(edge.Source);
            }

            return neibList;
        }


        private static void swapVertices(SortedSet<TVertex> a, TVertex vertA, SortedSet<TVertex> b, TVertex vertB)
        {
            if (!a.Contains(vertA) || a.Contains(vertB) ||
                !b.Contains(vertB) || b.Contains(vertA)) throw new Exception("Invalid swap");
            a.Remove(vertA); a.Add(vertB);
            b.Remove(vertB); b.Add(vertA);
        }


        private int getCutCost()
        {
            int cost = 0;
            foreach (TEdge edge in g.graph.Edges)
            {
                if (A.Contains(edge.Source) != A.Contains(edge.Target))
                {
                    cost += g.edgeWeights[edge];
                }
            }
            return cost;
        }


        private TEdge findEdge(TVertex vertFromA, TVertex vertFromB)
        {
            var vA = vertFromA.ToString();
            var vB = vertFromB.ToString();
            foreach (TEdge edge in g.graph.Edges)
            {
                if ((edge.Source.ToString() == vA && edge.Target.ToString() == vB)
                     || (edge.Target.ToString() == vA && edge.Source.ToString() == vB))
                    return edge;
            }
            return default(TEdge);
        }


        public void getStartPartition()
        {
            int i = 0;
            foreach (TVertex vert in g.graph.Vertices)
            {
                if (i < partitionSize) A.Add(vert);
                else B.Add(vert);
                i++;
            }
        }
    }
}

