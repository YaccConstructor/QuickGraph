using System;
using System.Collections.Generic;

using QuickGraph.Collections;

namespace QuickGraph.Algorithms.MinimumSpanningTree
{
    /// <summary>
    /// Prim's classic minimum spanning tree algorithm for undirected graphs
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="shi03datastructures"
    ///     />
    [Serializable]
    public sealed class PrimMinimumSpanningTreeAlgorithm<Vertex,Edge> : 
        RootedAlgorithmBase<Vertex,IUndirectedGraph<Vertex,Edge>>,
        ITreeBuilderAlgorithm<Vertex,Edge>,
        IVertexPredecessorRecorderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {        
        private IDictionary<Edge, double> edgeWeights;
        private Dictionary<Vertex, double> minimumWeights;
        private PriorithizedVertexBuffer<Vertex, double> queue;

        public PrimMinimumSpanningTreeAlgorithm(
            IUndirectedGraph<Vertex, Edge> visitedGraph,
            IDictionary<Edge, double> edgeWeights
            )
            :base(visitedGraph)
        {
            this.edgeWeights = edgeWeights;
        }

        public IDictionary<Edge, double> EdgeWeights
        {
            get { return this.edgeWeights; }
        }


        public event VertexEventHandler<Vertex> StartVertex;
        private void OnStartVertex(Vertex v)
        {
            VertexEventHandler<Vertex> eh = this.StartVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex, Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            EdgeEventHandler<Vertex, Edge> eh = this.TreeEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event VertexEventHandler<Vertex> FinishVertex;

        private void OnFinishVertex(Vertex v)
        {
            VertexEventHandler<Vertex> eh = this.FinishVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<Vertex>(v));
        }

        protected override void InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;
            if (this.RootVertex == null)
                this.RootVertex = TraversalHelper.GetFirstVertex<Vertex, Edge>(this.VisitedGraph);

            this.Initialize();

            try
            {
                this.minimumWeights[this.RootVertex] = 0;
                this.queue.Update(this.RootVertex);
                this.OnStartVertex(this.RootVertex);

                while (queue.Count != 0)
                {
                    if (this.IsAborting)
                        return;
                    Vertex u = queue.Pop();
                    foreach (Edge edge in this.VisitedGraph.AdjacentEdges(u))
                    {
                        if (this.IsAborting)
                            return;
                        double edgeWeight = this.EdgeWeights[edge];
                        if (
                            queue.Contains(edge.Target) &&
                            edgeWeight < this.minimumWeights[edge.Target]
                            )
                        {
                            this.minimumWeights[edge.Target] = edgeWeight;
                            this.queue.Update(edge.Target);
                            this.OnTreeEdge(edge);
                        }
                    }
                    this.OnFinishVertex(u);
                }
            }
            finally
            {
                this.CleanUp();
            }
        }

        private void Initialize()
        {
            this.minimumWeights = new Dictionary<Vertex, double>(this.VisitedGraph.VertexCount);
            this.queue = new PriorithizedVertexBuffer<Vertex, double>(this.minimumWeights);
            foreach (Vertex u in this.VisitedGraph.Vertices)
            {
                this.minimumWeights.Add(u, double.MaxValue);
                this.queue.Add(u);
            }
            this.queue.Sort();
        }

        private void CleanUp()
        {
            this.minimumWeights = null;
            this.queue = null;
        }
    }
}
