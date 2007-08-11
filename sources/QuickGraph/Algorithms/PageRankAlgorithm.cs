using System;
using System.Collections.Generic;
using QuickGraph.Predicates;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Ranking
{
    [Serializable]
    public sealed class PageRankAlgorithm<Vertex, Edge> :
        AlgorithmBase<IBidirectionalGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex,double> ranks = new Dictionary<Vertex,double>();

        private int maxIterations = 60;
        private double tolerance = 2 * double.Epsilon;
        private double damping = 0.85;

        public PageRankAlgorithm(IBidirectionalGraph<Vertex, Edge> visitedGraph)
            :base(visitedGraph)
        {}

        public IDictionary<Vertex,double> Ranks
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
            foreach (Vertex v in this.VisitedGraph.Vertices)
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
            IDictionary<Vertex,double> tempRanks = new Dictionary<Vertex,double>();
            // create filtered graph
            FilteredBidirectionalGraph<
                Vertex,
                Edge,
                IBidirectionalGraph<Vertex,Edge>
                > fg = new FilteredBidirectionalGraph<Vertex, Edge, IBidirectionalGraph<Vertex, Edge>>(
                this.VisitedGraph,
                new InDictionaryVertexPredicate<Vertex,double>(this.ranks).Test,
                new AnyEdgePredicate<Vertex,Edge>().Test
                );

            int iter = 0;
            double error = 0;
            do
            {
                if (this.IsAborting)
                    return;
                  
                // compute page ranks
                error = 0;
                foreach (KeyValuePair<Vertex,double> de in this.Ranks)
                {
                    if (this.IsAborting)
                        return;

                    Vertex v = de.Key;
                    double rank = de.Value;
                    // compute ARi
                    double r = 0;
                    foreach (Edge e in fg.InEdges(v))
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
                IDictionary<Vertex,double> temp = ranks;
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
