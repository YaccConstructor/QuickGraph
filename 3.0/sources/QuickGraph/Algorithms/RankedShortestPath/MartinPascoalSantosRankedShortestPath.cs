using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;

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
        public MartinPascoalSantosRankedShortestPath(IAlgorithmComponent host, IVertexListGraph<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph)
        { }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new InvalidOperationException("root vertex not set");

            var h = new Dictionary<int, TVertex>();
            var h1 = new Dictionary<TVertex, int>();
            var count = new Dictionary<TVertex, int>();
            foreach (var v in this.VisitedGraph.Vertices)
                count[v] = 0;
            int elm = 1;
            var X = new Dictionary<int, int>();

            h[elm] = root;
            h1[root] = elm;
            X.Add(elm, elm);

            while (X.Count > 0)
            {
                var k = X.First().Key;
                X.Remove(k);
                var i = h[k];
                foreach (var e in this.VisitedGraph.OutEdges(i))
                {
                    var j = e.Target;
                    if (count[j] == K)
                    {
                    }
                    else
                    {
                    }
                }
            }
        }

    }
}
