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
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    sealed class TarjanOfflineLeastCommonAncestorAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex , IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private VertexPair<TVertex>[] pairs;
        private readonly Dictionary<VertexPair<TVertex>, TVertex> ancestors =
            new Dictionary<VertexPair<TVertex>, TVertex>();

        public TarjanOfflineLeastCommonAncestorAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

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

        public void Compute(TVertex root, IEnumerable<VertexPair<TVertex>> pairs)
        {
            Contract.Requires(root != null);
            Contract.Requires(pairs != null);

            this.pairs = Enumerable.ToArray(pairs);
            this.Compute(root);
        }

        public IDictionary<VertexPair<TVertex>, TVertex> Ancestors
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
                throw new RootVertexNotSpecifiedException();
            if (this.pairs == null)
                throw new InvalidProgramException("pairs not set");

            var gpair = GraphExtensions.ToAdjacencyGraph(this.pairs);
            var disjointSet = new ForestDisjointSet<TVertex>();
            var vancestors = new Dictionary<TVertex, TVertex>();
            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(this, this.VisitedGraph, new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount));

            dfs.InitializeVertex += (sender, args) => disjointSet.MakeSet(args.Vertex);
            dfs.DiscoverVertex += (sender, args) => vancestors[args.Vertex] = args.Vertex;
            dfs.TreeEdge += (sender, args) =>
                {
                    var edge = args.Edge;
                    disjointSet.Union(edge.Source, edge.Target);
                    vancestors[disjointSet.FindSet(edge.Source)] = edge.Source;
                };
            dfs.FinishVertex += (sender, args) =>
                {
                    foreach (var e in gpair.OutEdges(args.Vertex))
                        if (dfs.VertexColors[e.Target] == GraphColor.Black)
                            this.ancestors[EdgeExtensions.ToVertexPair(e)] = vancestors[disjointSet.FindSet(e.Target)];
                };

            // go!
            dfs.Compute(root);
        }
    }
}
