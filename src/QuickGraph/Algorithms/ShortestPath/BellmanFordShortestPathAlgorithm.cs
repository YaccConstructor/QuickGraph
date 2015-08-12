using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// Bellman Ford shortest path algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Bellman-Ford algorithm solves the single-source shortest paths 
    /// problem for a graph with both positive and negative edge weights. 
    /// </para>
    /// <para>
    /// If you only need to solve the shortest paths problem for positive 
    /// edge weights, Dijkstra's algorithm provides a more efficient 
    /// alternative. 
    /// </para>
    /// <para>
    /// If all the edge weights are all equal to one then breadth-first search 
    /// provides an even more efficient alternative. 
    /// </para>
    /// </remarks>
    /// <reference-ref
    ///     idref="shi03datastructures"
    ///     />
    public sealed class BellmanFordShortestPathAlgorithm<TVertex, TEdge> 
        : ShortestPathAlgorithmBase<TVertex,TEdge
        , IVertexAndEdgeListGraph<TVertex,TEdge>>
        , ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Dictionary<TVertex,TVertex> predecessors;
        private bool foundNegativeCycle;

        public BellmanFordShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights
            )
            : this(visitedGraph, weights, DistanceRelaxers.ShortestDistance)
        { }

        public BellmanFordShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, visitedGraph, weights, distanceRelaxer)
        { }

        public BellmanFordShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph,
            Func<TEdge,double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph, weights, distanceRelaxer)
        {
            this.predecessors = new Dictionary<TVertex,TVertex>();
        }

        /// <summary>
        /// Indicates if a negative cycle was found
        /// </summary>
        public bool FoundNegativeCycle
        {
            get { return this.foundNegativeCycle;}
        }

        /// <summary>
        /// Invoked on each vertex in the graph before the start of the 
        /// algorithm.
        /// </summary>
        public event VertexAction<TVertex> InitializeVertex;

        /// <summary>
        /// Raises the <see cref="InitializeVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnInitializeVertex(TVertex v)
        {
            var eh = this.InitializeVertex;
            if (eh != null)
                eh(v);
        }

        /// <summary>
        /// Invoked on every edge in the graph |V| times.
        /// </summary>
        public event EdgeAction<TVertex,TEdge> ExamineEdge;

        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnExamineEdge(TEdge e)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(e);
        }

        /// <summary>
        ///  Invoked if the distance label for the target vertex is not 
        ///  decreased.
        /// </summary>
        public event EdgeAction<TVertex,TEdge> EdgeNotRelaxed;

        /// <summary>
        /// Raises the <see cref="EdgeNotRelaxed"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotRelaxed(TEdge e)
        {
            var eh = this.EdgeNotRelaxed;
            if (eh != null)
                eh(e);
        }

        /// <summary>
        ///  Invoked during the second stage of the algorithm, 
        ///  during the test of whether each edge was minimized. 
        ///  
        ///  If the edge is minimized then this function is invoked.
        /// </summary>
        public event EdgeAction<TVertex,TEdge> EdgeMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeMinimized(TEdge e)
        {
            var eh = this.EdgeMinimized;
            if (eh != null)
                eh(e);
        }

        /// <summary>
        /// Invoked during the second stage of the algorithm, 
        /// during the test of whether each edge was minimized. 
        /// 
        /// If the edge was not minimized, this function is invoked. 
        /// This happens when there is a negative cycle in the graph. 
        /// </summary>
        public event EdgeAction<TVertex,TEdge> EdgeNotMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeNotMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotMinimized(TEdge e)
        {
            var eh = this.EdgeNotMinimized;
            if (eh != null)
                eh(e);
        }

        /// <summary>
        /// Constructed predecessor map
        /// </summary>
        public IDictionary<TVertex,TVertex> Predecessors
        {
            get
            {
                return predecessors;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.foundNegativeCycle = false;
            // init color, distance
            this.VertexColors.Clear();
            foreach (var u in VisitedGraph.Vertices)
            {
                this.VertexColors[u] = GraphColor.White;
                this.Distances[u] = double.PositiveInfinity;
                this.OnInitializeVertex(u);
            }

            TVertex root;
            if (!this.TryGetRootVertex(out root))
                foreach (var v in this.VisitedGraph.Vertices)
                {
                    root = v;
                    break;
                }

            this.Distances[root] = 0;
        }

        /// <summary>
        /// Applies the Bellman Ford algorithm
        /// </summary>
        /// <remarks>
        /// Does not initialize the predecessor and distance map.
        /// </remarks>
        protected override void InternalCompute()
        {
            // getting the number of 
            int N = this.VisitedGraph.VertexCount;
            for (int k = 0; k < N; ++k)
            {
                bool atLeastOneTreeEdge = false;
                foreach (var e in this.VisitedGraph.Edges)
                {
                    this.OnExamineEdge(e);

                    if (Relax(e))
                    {
                        atLeastOneTreeEdge = true;
                        OnTreeEdge(e);
                    }
                    else
                        this.OnEdgeNotRelaxed(e);
                }
                if (!atLeastOneTreeEdge)
                    break;
            }

            var relaxer = this.DistanceRelaxer;
            foreach (var e in this.VisitedGraph.Edges)
            {
                var edgeWeight = Weights(e);
                if (relaxer.Compare(
                        relaxer.Combine(
                            this.Distances[e.Source], edgeWeight),
                            this.Distances[e.Target]
                        ) < 0
                    )
                {
                    this.OnEdgeMinimized(e);
                    this.foundNegativeCycle = true;
                    return;
                }
                else
                    this.OnEdgeNotMinimized(e);
            }
            this.foundNegativeCycle = false;
        }
    }
}
