using System;
using System.Collections.Generic;
using QuickGraph.Predicates;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Ranking
{
    [Serializable]
    public sealed class PageRankAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IBidirectionalGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex,double> ranks = new Dictionary<TVertex,double>();

        private int maxIterations = 60;
        private double tolerance = 2 * double.Epsilon;
        private double damping = 0.85;

        public PageRankAlgorithm(IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            :base(visitedGraph)
        {}

        public IDictionary<TVertex,double> Ranks
        {
            get
            {
                return this.ranks;
            }
        }

        public double Damping
        {
            get
            {
                return this.damping;
            }
            set
            {
                this.damping = value;
            }
        }

        public double Tolerance
        {
            get
            {
                return this.tolerance;
            }
            set
            {
                this.tolerance = value;
            }
        }

        public int MaxIteration
        {
            get
            {
                return this.maxIterations;
            }
            set
            {
                this.maxIterations = value;
            }
        }

        public void InitializeRanks()
        {
            this.ranks.Clear();
            foreach (TVertex v in this.VisitedGraph.Vertices)
            {
                this.ranks.Add(v, 0);
            }
//            this.RemoveDanglingLinks();
        }
/*
        public void RemoveDanglingLinks()
        {
            VertexCollection danglings = new VertexCollection();
            do
            {
                danglings.Clear();

                // create filtered graph
                IVertexListGraph fg = new FilteredVertexListGraph(
                    this.VisitedGraph,
                    new InDictionaryVertexPredicate(this.ranks)
                    );

                // iterate over of the vertices in the rank map
                foreach (IVertex v in this.ranks.Keys)
                {
                    // if v does not have out-edge in the filtered graph, remove
                    if (fg.OutDegree(v) == 0)
                        danglings.Add(v);
                }

                // remove from ranks
                foreach (IVertex v in danglings)
                    this.ranks.Remove(v);
                // iterate until no dangling was removed
            } while (danglings.Count != 0);
        }
*/
        protected override void InternalCompute()
        {
            IDictionary<TVertex,double> tempRanks = new Dictionary<TVertex,double>();
            // create filtered graph
            FilteredBidirectionalGraph<
                TVertex,
                TEdge,
                IBidirectionalGraph<TVertex,TEdge>
                > fg = new FilteredBidirectionalGraph<TVertex, TEdge, IBidirectionalGraph<TVertex, TEdge>>(
                this.VisitedGraph,
                new InDictionaryVertexPredicate<TVertex,double>(this.ranks).Test,
                new AnyEdgePredicate<TVertex,TEdge>().Test
                );

            int iter = 0;
            double error = 0;
            do
            {
                if (this.IsAborting)
                    return;
                  
                // compute page ranks
                error = 0;
                foreach (KeyValuePair<TVertex,double> de in this.Ranks)
                {
                    if (this.IsAborting)
                        return;

                    TVertex v = de.Key;
                    double rank = de.Value;
                    // compute ARi
                    double r = 0;
                    foreach (TEdge e in fg.InEdges(v))
                    {
                        r += this.ranks[e.Source] / fg.OutDegree(e.Source);
                    }

                    // add sourceRank and store
                    double newRank = (1 - this.damping) + this.damping * r;
                    tempRanks[v] = newRank;
                    // compute deviation
                    error += Math.Abs(rank - newRank);
                }

                // swap ranks
                IDictionary<TVertex,double> temp = ranks;
                ranks = tempRanks;
                tempRanks = temp;

                iter++;
            } while (error > this.tolerance && iter < this.maxIterations);
            Console.WriteLine("{0}, {1}", iter, error);
        }

        public double GetRanksSum()
        {
            double sum = 0;
            foreach (double rank in this.ranks.Values)
            {
                sum += rank;
            }
            return sum;
        }

        public double GetRanksMean()
        {
            return GetRanksSum() / this.ranks.Count;
        }
    }
}
