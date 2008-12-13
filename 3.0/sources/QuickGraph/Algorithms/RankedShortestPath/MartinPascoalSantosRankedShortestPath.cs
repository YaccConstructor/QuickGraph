using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.ShortestPath;
using System.Linq;

namespace QuickGraph.Algorithms.RankedShortestPath
{
    /// <summary>
    /// under construction
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    class MartinPascoalSantosRankedShortestPath<TVertex, TEdge>
        : RankingShortestPathBase<TVertex, TEdge, IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        readonly Dictionary<TEdge, double> distances = new Dictionary<TEdge, double>();

        public MartinPascoalSantosRankedShortestPath(IAlgorithmComponent host, IVertexListGraph<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph, ShortestDistanceRelaxer.Instance)
        { }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new InvalidOperationException("root vertex not set");

            var h = new Dictionary<int, TVertex>();
            var count = new Dictionary<TVertex, int>();
            var costs = new Dictionary<int, double>();
            foreach (var v in this.VisitedGraph.Vertices)
                count[v] = 0;
            int elm = 1;
            var X = new Dictionary<int, int>();

            h[elm] = root;
            costs[elm] = 0;
            X[elm] = elm;

            while (X.Count > 0)
            {
                var k = Enumerable.First(X).Key;
                X.Remove(k);
                var i = h[k];
                foreach (var e in this.VisitedGraph.OutEdges(i))
                {
                    var j = e.Target;
                    var costk = costs[k];
                    var costkij = this.Relaxer.Combine(costk, this.distances[e]);
                    if (count[j] == K)
                    {
                        // todo...
                        int l = -1;//(int)h1(h, j).Max(lj => costs[lj]);
                        if (this.Relaxer.Compare(costkij, costs[l]))
                        {
                            costs[k] = costkij;
                            // eps[l] = k;
                            X[l] = l;
                        }
                    }
                    else
                    {
                        elm++;
                        costs[elm] = costkij;
                        // eps[elm] = k;
                        count[j]++;
                        h[elm] = j;
                        X[elm] = elm;
                    }
                }
            }
        }


        static IEnumerable<int> h1(Dictionary<int, TVertex> h, TVertex v)
        {
            foreach (var kv in h)
                if (kv.Value.Equals(v))
                    yield return kv.Key;
        }
    }
}
