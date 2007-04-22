using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// A edge depth first search algorithm for directed graphs
    /// </summary>
    /// <remarks>
    /// This is a variant of the classic DFS algorithm where the
    /// edges are color marked instead of the vertices.
    /// </remarks>
    /// <reference-ref
    ///     idref="gross98graphtheory"
    ///     chapter="4.2"
    ///     />
    [Serializable]
    public sealed class EdgeDepthFirstSearchAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex,IEdgeListAndIncidenceGraph<Vertex, Edge>>,
        IEdgeColorizerAlgorithm<Vertex,Edge>,
        IEdgePredecessorRecorderAlgorithm<Vertex,Edge>,
        ITreeBuilderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Edge,GraphColor> colors;
        private int maxDepth = int.MaxValue;

        public EdgeDepthFirstSearchAlgorithm(IEdgeListAndIncidenceGraph<Vertex, Edge> g)
            :this(g, new Dictionary<Edge, GraphColor>())
        {
        }

        public EdgeDepthFirstSearchAlgorithm(
            IEdgeListAndIncidenceGraph<Vertex, Edge> visitedGraph,
            IDictionary<Edge, GraphColor> colors
            )
            :base(visitedGraph)
        {
            if (colors == null)
                throw new ArgumentNullException("VertexColors");

            this.colors = colors;
        }

        public IDictionary<Edge, GraphColor> EdgeColors
        {
            get
            {
                return this.colors;
            }
        }

        public int MaxDepth
        {
            get
            {
                return this.maxDepth;
            }
            set
            {
                this.maxDepth = value;
            }
        }

        public event EdgeEventHandler<Vertex,Edge> InitializeEdge;
        private void OnInitializeEdge(Edge e)
        {
            if (InitializeEdge != null)
                InitializeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event VertexEventHandler<Vertex> StartVertex;
        private void OnStartVertex(Vertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex, Edge> StartEdge;
        private void OnStartEdge(Edge e)
        {
            if (StartEdge != null)
                StartEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEdgeEventHandler<Vertex, Edge> DiscoverTreeEdge;
        private void OnDiscoverTreeEdge(Edge e, Edge targetEge)
        {
            if (DiscoverTreeEdge != null)
                DiscoverTreeEdge(this, new EdgeEdgeEventArgs<Vertex, Edge>(e, targetEge));
        }

        public event EdgeEventHandler<Vertex, Edge> ExamineEdge;
        private void OnExamineEdge(Edge e)
        {
            if (ExamineEdge != null)
                ExamineEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (TreeEdge != null)
                TreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> BackEdge;
        private void OnBackEdge(Edge e)
        {
            if (BackEdge != null)
                BackEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex, Edge> ForwardOrCrossEdge;
        private void OnForwardOrCrossEdge(Edge e)
        {
            if (ForwardOrCrossEdge != null)
                ForwardOrCrossEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        public event EdgeEventHandler<Vertex,Edge> FinishEdge;
        private void OnFinishEdge(Edge e)
        {
            if (FinishEdge != null)
                FinishEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }
        
        protected override void  InternalCompute()
        {
            Initialize();
            if (this.IsAborting)
                return;

            // start whith him:
            if (this.RootVertex != null)
            {
                OnStartVertex(this.RootVertex);

                // process each out edge of v
                foreach (Edge e in VisitedGraph.OutEdges(this.RootVertex))
                {
                    if (this.IsAborting)
                        return;
                    if (EdgeColors[e] == GraphColor.White)
                    {
                        OnStartEdge(e);
                        Visit(e, 0);
                    }
                }
            }

            // process the rest of the graph edges
            foreach (Edge e in VisitedGraph.Edges)
            {
                if (this.IsAborting)
                    return;
                if (EdgeColors[e] == GraphColor.White)
                {
                    OnStartEdge(e);
                    Visit(e, 0);
                }
            }
        }

        public void Initialize()
        {
            // put all vertex to white
            foreach (Edge e in VisitedGraph.Edges)
            {
                if (this.IsAborting)
                    return;
                EdgeColors[e] = GraphColor.White;
                OnInitializeEdge(e);
            }
        }

        public void Visit(Edge se, int depth)
        {
            if (depth > this.maxDepth)
                return;
            if (this.IsAborting)
                return;

            // mark edge as gray
            EdgeColors[se] = GraphColor.Gray;
            // add edge to the search tree
            OnTreeEdge(se);

            // iterate over out-edges
            foreach (Edge e in VisitedGraph.OutEdges(se.Target))
            {
                // check edge is not explored yet,
                // if not, explore it.
                if (EdgeColors[e] == GraphColor.White)
                {
                    OnDiscoverTreeEdge(se, e);
                    Visit(e, depth + 1);
                }
                else if (EdgeColors[e] == GraphColor.Gray)
                {
                    // edge is being explored
                    OnBackEdge(e);
                }
                else
                    // edge is black
                    OnForwardOrCrossEdge(e);
            }

            // all out-edges have been explored
            EdgeColors[se] = GraphColor.Black;
            OnFinishEdge(se);
        }
    }
}
