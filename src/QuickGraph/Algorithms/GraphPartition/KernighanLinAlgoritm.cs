using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms;

namespace QuickGraph.Algorithms.KernighanLinAlgoritm
{
    public sealed class KernighanLinAlgoritm<TVertex, TTag>
        where TTag : IEquatable<TTag>, IConvertible, IComparable
    {
        private UndirectedGraph<TVertex, TaggedUndirectedEdge<TVertex, TTag>> g;
        private int itersNum;
        private int partitionSize;
        private SortedSet<TVertex> A, B;
        private SortedSet<TVertex> unswappedA, unswappedB;

        public KernighanLinAlgoritm(UndirectedGraph<TVertex, TaggedUndirectedEdge<TVertex, TTag>> g, int itersNum) 
        {
            this.g = g;
            this.itersNum = itersNum;
            this.partitionSize = g.Vertices.Count() / 2;
        }


        public Partition<TVertex> Execute()
        {

            A = new SortedSet<TVertex>();
            B = new SortedSet<TVertex>();

            getStartPartition();

            unswappedA = new SortedSet<TVertex>(A);
            unswappedB = new SortedSet<TVertex>(B);

            var bestPartition = new Partition<TVertex>(A, B);
            double minCost = double.MaxValue;

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
            List<Tuple<TVertex, TVertex>> swaps = new List<Tuple<TVertex, TVertex>>();
            double minCost = double.MaxValue;
            int minId = -1;

            for (int i = 0; i < partitionSize; i++)
            {
                double cost = doSingleSwap(swaps);
                if (cost < minCost)
                {
                    minCost = cost; minId = i;
                }
            }

            //Back to swap step with min cutcost
            while (swaps.Count - 1 > minId)
            {
                Tuple<TVertex, TVertex> pair = swaps.Last();
                swaps.Remove(pair);
                swapVertices(A, pair.Item2, B, pair.Item1);
            }

            return new Partition<TVertex>(A, B, minCost);
        }


        private double doSingleSwap(List<Tuple<TVertex, TVertex>> swaps)
        {
            Tuple<TVertex, TVertex> maxPair = null;
            double maxGain = double.MinValue;
            foreach (TVertex vertFromA in unswappedA)
                foreach (TVertex vertFromB in unswappedB)
                {
                    TaggedUndirectedEdge<TVertex, TTag> edge = findEdge(vertFromA, vertFromB);
                    double edgeCost;
                    if (edge != null) edgeCost = Convert.ToDouble(edge.Tag); else edgeCost = 0.0;
                    double gain = getVertexCost(vertFromA) + getVertexCost(vertFromB) - (edgeCost + edgeCost);
                    if (gain > maxGain)
                    {
                        maxPair = new Tuple<TVertex, TVertex>(vertFromA, vertFromB);
                        maxGain = gain;
                    }

                }
            swapVertices(A, maxPair.Item1, B, maxPair.Item2);
            swaps.Add(maxPair);
            unswappedA.Remove(maxPair.Item1);
            unswappedB.Remove(maxPair.Item2);

            return getCutCost();
        }


        private double getVertexCost(TVertex vert)
        {

            double cost = 0;
            bool vertIsInA = A.Contains(vert);
            var neib = getNeighbors(vert);

            foreach (TVertex vertNeighbord in neib)
            {
                bool vertNeighbordIsInA = A.Contains(vertNeighbord);
                TaggedUndirectedEdge<TVertex, TTag> edge = findEdge(vert, vertNeighbord);
                if (vertIsInA != vertNeighbordIsInA) // external
                    cost += Convert.ToDouble(edge.Tag);
                else
                    cost -= Convert.ToDouble(edge.Tag);
;
            }

            return cost;
        }


        private List<TVertex> getNeighbors(TVertex vert)
        {
            var neibList = new List<TVertex>();
            foreach (TaggedUndirectedEdge<TVertex, TTag> edge in g.AdjacentEdges(vert))
            {
                if (edge.Source.Equals(vert) && !neibList.Contains(edge.Target))
                    neibList.Add(edge.Target);
                if (edge.Target.Equals(vert) && !neibList.Contains(edge.Source))
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


        private double getCutCost()
        {
            double cost = 0;
            foreach (TaggedUndirectedEdge<TVertex, TTag> edge in g.Edges)
            {
                if (A.Contains(edge.Source) != A.Contains(edge.Target))
                {
                    cost += Convert.ToDouble(edge.Tag);
                }
            }
            return cost;
        }


        private TaggedUndirectedEdge<TVertex, TTag> findEdge(TVertex vertFromA, TVertex vertFromB)
        {
            foreach (TaggedUndirectedEdge<TVertex, TTag> edge in g.Edges)
            {
                if ((edge.Source.Equals(vertFromA) && edge.Target.Equals(vertFromB))
                     || (edge.Target.Equals(vertFromA) && edge.Source.Equals(vertFromB)))
                    return edge;
            }
            return default(TaggedUndirectedEdge<TVertex, TTag>);
        }


        public void getStartPartition()
        {
            int i = 0;
            foreach (TVertex vert in g.Vertices)
            {
                if (i < partitionSize) A.Add(vert);
                else B.Add(vert);
                i++;
            }
        }
    }
}

