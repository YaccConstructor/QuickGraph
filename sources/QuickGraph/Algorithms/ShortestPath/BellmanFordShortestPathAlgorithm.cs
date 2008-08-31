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
    public sealed class BellmanFordShortestPathAlgorithm<TVertex, TEdge> :
        ShortestPathAlgorithmBase<TVertex,TEdge,IVertexAndEdgeListGraph<TVertex,TEdge>>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Dictionary<TVertex,TVertex> predecessors;
        private bool foundNegativeCycle;

        public BellmanFordShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TEdge, double> weights
            )
            : this(visitedGraph, weights, new ShortestDistanceRelaxer())
        { }

        public BellmanFordShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : this(null, visitedGraph, weights, distanceRelaxer)
        { }

        public BellmanFordShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph,
            IDictionary<TEdge,double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph, weights, distanceRelaxer)
        {
            this.predecessors = new Dictionary<TVertex,TVertex>();
        }

        public bool FoundNegativeCycle
        {
            get { return this.foundNegativeCycle;}
        }

        /// <summary>
        /// Invoked on each vertex in the graph before the start of the 
        /// algorithm.
        /// </summary>
        public event VertexEventHandler<TVertex> InitializeVertex;

        /// <summary>
        /// Raises the <see cref="InitializeVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnInitializeVertex(TVertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<TVertex>(v));
        }

        /// <summary>
        /// Invoked on every edge in the graph |V| times.
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> ExamineEdge;

        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnExamineEdge(TEdge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        /// <summary>
        /// Invoked when the distance label for the target vertex is decreased. 
        /// The edge that participated in the last relaxation for vertex v is 
        /// an edge in the shortest paths tree.
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;


        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnTreeEdge(TEdge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        /// <summary>
        ///  Invoked if the distance label for the target vertex is not 
        ///  decreased.
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> EdgeNotRelaxed;

        /// <summary>
        /// Raises the <see cref="EdgeNotRelaxed"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotRelaxed(TEdge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        /// <summary>
        ///  Invoked during the second stage of the algorithm, 
        ///  during the test of whether each edge was minimized. 
        ///  
        ///  If the edge is minimized then this function is invoked.
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> EdgeMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeMinimized(TEdge e)
        {
            if (EdgeMinimized != null)
                EdgeMinimized(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        /// <summary>
        /// Invoked during the second stage of the algorithm, 
        /// during the test of whether each edge was minimized. 
        /// 
        /// If the edge was not minimized, this function is invoked. 
        /// This happens when there is a negative cycle in the graph. 
        /// </summary>
        public event EdgeEventHandler<TVertex,TEdge> EdgeNotMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeNotMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotMinimized(TEdge e)
        {
            if (EdgeNotMinimized != null)
                EdgeNotMinimized(this, new EdgeEventArgs<TVertex,TEdge>(e));
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

        private void Initialize()
        {
            this.foundNegativeCycle = false;
            // init color, distance
            foreach (var u in VisitedGraph.Vertices)
            {
                VertexColors[u] = GraphColor.White;
                Distances[u] = double.PositiveInfinity;
                OnInitializeVertex(u);
            }
        }

        /// <summary>
        /// Applies the Bellman Ford algorithm
        /// </summary>
        /// <remarks>
        /// Does not initialize the predecessor and distance map.
        /// </remarks>
        /// <returns>true if successful, false if there was a negative cycle.</returns>
        protected override void InternalCompute()
        {
            this.Initialize();

            // getting the number of 
            int N = this.VisitedGraph.VertexCount;
            for (int k = 0; k < N; ++k)
            {
                bool atLeastOneTreeEdge = false;
                foreach (var e in this.VisitedGraph.Edges)
                {
                    OnExamineEdge(e);

                    if (Relax(e))
                    {
                        atLeastOneTreeEdge = true;
                        OnTreeEdge(e);
                    }
                    else
                        OnEdgeNotRelaxed(e);
                }
                if (!atLeastOneTreeEdge)
                    break;
            }

            foreach (var e in VisitedGraph.Edges)
            {
                if (
                    Compare(
                        Combine(
                            Distances[e.Source], Weights[e]),
                            Distances[e.Target]
                        )
                    )
                {
                    OnEdgeMinimized(e);
                    this.foundNegativeCycle = true;
                    return;
                }
                else
                    OnEdgeNotMinimized(e);
            }
            this.foundNegativeCycle = false;
        }

        private bool Relax(TEdge e)
        {
            double du = this.Distances[e.Source];
            double dv = this.Distances[e.Target];
            double we = this.Weights[e];

            if (Compare(Combine(du, we), dv))
            {
                this.Distances[e.Target] = Combine(du, we);
                return true;
            }
            else
                return false;
        }
    }
}
