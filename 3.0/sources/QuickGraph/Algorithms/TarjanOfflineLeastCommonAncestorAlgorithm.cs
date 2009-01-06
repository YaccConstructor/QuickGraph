using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Search;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms
{
    /// <summary>
    /// under construction
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    sealed class TarjanOfflineLeastCommonAncestorAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex , IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private VertexPair<TVertex>[] pairs;

        public TarjanOfflineLeastCommonAncestorAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph)
        { }

        public bool TryGetVertexPairs(out IEnumerable<VertexPair<TVertex>> pairs)
        {
            pairs = this.pairs;
            return pairs!=null;
        }

        public void SetVertexPairs(IEnumerable<VertexPair<TVertex>> pairs)
        {
            Contract.Requires(pairs != null);

            this.pairs = new List<VertexPair<TVertex>>(pairs).ToArray();
        }

        public void Compute(TVertex root, params VertexPair<TVertex>[] pairs)
        {
            Contract.Requires(root != null);
            Contract.Requires(pairs != null);

            this.pairs = pairs;
            this.Compute(root);
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();
            if (this.pairs == null)
                throw new InvalidProgramException("pairs not set");

            var lca = new Dictionary<VertexPair<TVertex>, TVertex>();
            var disjointSet = new ForestDisjointSet<TVertex>();
            foreach (var v in this.VisitedGraph.Vertices)
                disjointSet.MakeSet(v);

            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(this.VisitedGraph);
            dfs.ExamineEdge += (sender, args) =>
                {
                };
            dfs.FinishVertex += (sender, args) =>
                {
                    //foreach (var pair in this.pairs)
                    //    if (pair.Source.Equals(args.Vertex))
                    //        lca[pair] = disjointSet.FindSetRepresentative(pair.Target);
                };
        }
    }
}
