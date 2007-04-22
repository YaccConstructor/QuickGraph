using System;
using System.Collections.Generic;

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
    public sealed class BellmanFordShortestPathAlgorithm<Vertex, Edge> :
        ShortestPathAlgorithmBase<Vertex,Edge,IVertexAndEdgeListGraph<Vertex,Edge>>,
        ITreeBuilderAlgorithm<Vertex,Edge>
       // IVertexPredecessorRecorderAlgorithm<Vertex,Edge>,
       // IDistanceRecorderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex,Vertex> predecessors;
        private bool foundNegativeCycle;

        /// <summary>
        /// Builds a new Bellman Ford searcher.
        /// </summary>
        /// <param name="g">The graph</param>
        /// <param name="weights">Edge weights</param>
        /// <exception cref="ArgumentNullException">Any argument is null</exception>
        /// <remarks>This algorithm uses the <seealso cref="BreadthFirstSearchAlgorithm"/>.</remarks>
        public BellmanFordShortestPathAlgorithm(
            IVertexAndEdgeListGraph<Vertex,Edge> visitedGraph,
            IDictionary<Edge,double> weights
            )
            :base(visitedGraph,weights)
        {
            this.predecessors = new Dictionary<Vertex,Vertex>();
        }

        public bool FoundNegativeCycle
        {
            get { return this.foundNegativeCycle;}
        }

        /// <summary>
        /// Invoked on each vertex in the graph before the start of the 
        /// algorithm.
        /// </summary>
        public event VertexEventHandler<Vertex> InitializeVertex;

        /// <summary>
        /// Raises the <see cref="InitializeVertex"/> event.
        /// </summary>
        /// <param name="v">vertex that raised the event</param>
        private void OnInitializeVertex(Vertex v)
        {
            if (InitializeVertex != null)
                InitializeVertex(this, new VertexEventArgs<Vertex>(v));
        }

        /// <summary>
        /// Invoked on every edge in the graph |V| times.
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> ExamineEdge;

        /// <summary>
        /// Raises the <see cref="ExamineEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnExamineEdge(Edge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        /// <summary>
        /// Invoked when the distance label for the target vertex is decreased. 
        /// The edge that participated in the last relaxation for vertex v is 
        /// an edge in the shortest paths tree.
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> TreeEdge;


        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        /// <summary>
        ///  Invoked if the distance label for the target vertex is not 
        ///  decreased.
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> EdgeNotRelaxed;

        /// <summary>
        /// Raises the <see cref="EdgeNotRelaxed"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotRelaxed(Edge e)
        {
            if (EdgeNotRelaxed != null)
                EdgeNotRelaxed(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        /// <summary>
        ///  Invoked during the second stage of the algorithm, 
        ///  during the test of whether each edge was minimized. 
        ///  
        ///  If the edge is minimized then this function is invoked.
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> EdgeMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeMinimized(Edge e)
        {
            if (EdgeMinimized != null)
                EdgeMinimized(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        /// <summary>
        /// Invoked during the second stage of the algorithm, 
        /// during the test of whether each edge was minimized. 
        /// 
        /// If the edge was not minimized, this function is invoked. 
        /// This happens when there is a negative cycle in the graph. 
        /// </summary>
        public event EdgeEventHandler<Vertex,Edge> EdgeNotMinimized;


        /// <summary>
        /// Raises the <see cref="EdgeNotMinimized"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        private void OnEdgeNotMinimized(Edge e)
        {
            if (EdgeNotMinimized != null)
                EdgeNotMinimized(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        /// <summary>
        /// Constructed predecessor map
        /// </summary>
        public IDictionary<Vertex,Vertex> Predecessors
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
            foreach (Vertex u in VisitedGraph.Vertices)
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
                foreach (Edge e in this.VisitedGraph.Edges)
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

            foreach (Edge e in VisitedGraph.Edges)
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

        private bool Relax(Edge e)
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
