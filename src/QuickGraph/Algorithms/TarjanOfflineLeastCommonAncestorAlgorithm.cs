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
    /// Offline least common ancestor in a rooted tre
    /// </summary>
    /// <remarks>
    /// Reference:
    /// Gabow, H. N. and Tarjan, R. E. 1983. A linear-time algorithm for a special case 
    /// of disjoint set union. In Proceedings of the Fifteenth Annual ACM Symposium 
    /// on theory of Computing STOC '83. ACM, New York, NY, 246-251. 
    /// DOI= http://doi.acm.org/10.1145/800061.808753 
    /// </remarks>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public sealed class TarjanOfflineLeastCommonAncestorAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex , IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private SEquatableEdge<TVertex>[] pairs;
        private readonly Dictionary<SEquatableEdge<TVertex>, TVertex> ancestors =
            new Dictionary<SEquatableEdge<TVertex>, TVertex>();

        public TarjanOfflineLeastCommonAncestorAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

        public TarjanOfflineLeastCommonAncestorAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph)
        { }

        public bool TryGetVertexPairs(out IEnumerable<SEquatableEdge<TVertex>> pairs)
        {
            pairs = this.pairs;
            return pairs!=null;
        }

        public void SetVertexPairs(IEnumerable<SEquatableEdge<TVertex>> pairs)
        {
            Contract.Requires(pairs != null);

            this.pairs = new List<SEquatableEdge<TVertex>>(pairs).ToArray();
        }

        public void Compute(TVertex root, IEnumerable<SEquatableEdge<TVertex>> pairs)
        {
            Contract.Requires(root != null);
            Contract.Requires(pairs != null);

            this.pairs = Enumerable.ToArray(pairs);
            this.Compute(root);
        }

        public IDictionary<SEquatableEdge<TVertex>, TVertex> Ancestors
        {
            get { return this.ancestors; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.ancestors.Clear();
        }

        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;

            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new InvalidOperationException("root vertex not set");
            if (this.pairs == null)
                throw new InvalidOperationException("pairs not set");

            var gpair = GraphExtensions.ToAdjacencyGraph(this.pairs);
            var disjointSet = new ForestDisjointSet<TVertex>();
            var vancestors = new Dictionary<TVertex, TVertex>();
            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(this, this.VisitedGraph, new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount));

            dfs.InitializeVertex += v => disjointSet.MakeSet(v);
            dfs.DiscoverVertex += v => vancestors[v] = v;
            dfs.TreeEdge += edge =>
                {
                    disjointSet.Union(edge.Source, edge.Target);
                    vancestors[disjointSet.FindSet(edge.Source)] = edge.Source;
                };
            dfs.FinishVertex += v =>
                {
                    foreach (var e in gpair.OutEdges(v))
                        if (dfs.VertexColors[e.Target] == GraphColor.Black)
                            this.ancestors[EdgeExtensions.ToVertexPair<TVertex, SEquatableEdge<TVertex>>(e)] = vancestors[disjointSet.FindSet(e.Target)];
                };

            // go!
            dfs.Compute(root);
        }
    }
}
