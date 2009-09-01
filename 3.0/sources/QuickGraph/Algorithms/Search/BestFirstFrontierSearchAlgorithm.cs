using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// NOT FINISHED
    /// </summary>
    /// <remarks>
    /// Algorithm from Frontier Search, Korkf, Zhand, Thayer, Hohwald.
    /// </remarks>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    internal sealed class BestFirstFrontierSearchAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex, IImplicitGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private TVertex targetVertex;
        private bool hasTargetVertex;

        public BestFirstFrontierSearchAlgorithm(
            IAlgorithmComponent host,
            IImplicitGraph<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph)
        {
        }

        public BestFirstFrontierSearchAlgorithm(
            IImplicitGraph<TVertex, TEdge> visitedGraph)
            :this(null, visitedGraph)
        {
        }

        public bool TryGetTargetVertex(out TVertex targetVertex)
        {
            targetVertex = this.targetVertex;
            return this.hasTargetVertex;
        }

        public TVertex TargetVertex
        {
            set
            {
                this.targetVertex = value;
                this.hasTargetVertex = this.targetVertex != null;
            }
        }

        struct Operator
        {
            public readonly TEdge Edge;
            public bool Used;

            public Operator(TEdge edge)
            {
                Contract.Requires(edge != null);

                this.Edge = edge;
                this.Used = false;
            }
        }

        struct Node
        {
            public readonly TVertex Vertex;
            public readonly Operator[] Operators;
            public Node(TVertex vertex, IEnumerable<TEdge> edges)
            {
                Contract.Requires(vertex != null); 
                Contract.Requires(edges != null);

                this.Vertex = vertex;
                this.Operators = Array.ConvertAll(Enumerable.ToArray(edges), e => new Operator(e));
            }
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();

            var cancelManager = this.Services.CancelManager;
            var queue = new BinaryHeap<double, Node>();
            var g = this.VisitedGraph;

            queue.Add(0, new Node(root, g.OutEdges(root)));
            while (queue.Count > 0)
            {
                var node = queue.RemoveMinimum();
            }
        }
    }
}
