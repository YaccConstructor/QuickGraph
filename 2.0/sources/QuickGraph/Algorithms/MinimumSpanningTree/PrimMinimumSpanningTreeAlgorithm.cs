using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;

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
    public sealed class PrimMinimumSpanningTreeAlgorithm<TVertex,TEdge> : 
        RootedAlgorithmBase<TVertex,IUndirectedGraph<TVertex,TEdge>>,
        ITreeBuilderAlgorithm<TVertex,TEdge>,
        IVertexPredecessorRecorderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {        
        private IDictionary<TEdge, double> edgeWeights;
        private Dictionary<TVertex, double> minimumWeights;
        private PriorityQueue<TVertex, double> queue;

        public PrimMinimumSpanningTreeAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TEdge, double> edgeWeights
            )
            : this(null, visitedGraph, edgeWeights)
        {}

        public PrimMinimumSpanningTreeAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TEdge, double> edgeWeights
            )
            :base(host, visitedGraph)
        {
            if (edgeWeights == null)
                throw new ArgumentNullException("edgeWeights");
            this.edgeWeights = edgeWeights;
        }

        public IDictionary<TEdge, double> EdgeWeights
        {
            get { return this.edgeWeights; }
        }


        public event VertexEventHandler<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            VertexEventHandler<TVertex> eh = this.StartVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            EdgeEventHandler<TVertex, TEdge> eh = this.TreeEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> FinishVertex;

        private void OnFinishVertex(TVertex v)
        {
            VertexEventHandler<TVertex> eh = this.FinishVertex;
            if (eh != null)
                eh(this, new VertexEventArgs<TVertex>(v));
        }

        protected override void InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;
            var cancelManager = this.Services.CancelManager;
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                rootVertex = TraversalHelper.GetFirstVertex<TVertex, TEdge>(this.VisitedGraph);

            this.Initialize();

            try
            {
                this.minimumWeights[rootVertex] = 0;
                this.queue.Update(rootVertex);
                this.OnStartVertex(rootVertex);

                while (queue.Count != 0)
                {
                    if (cancelManager.IsCancelling)
                        return;
                    TVertex u = queue.Dequeue();
                    foreach (var edge in this.VisitedGraph.AdjacentEdges(u))
                    {
                        if (cancelManager.IsCancelling)
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
            this.minimumWeights = new Dictionary<TVertex, double>(this.VisitedGraph.VertexCount);
            this.queue = new PriorityQueue<TVertex, double>(this.minimumWeights);
            foreach (var u in this.VisitedGraph.Vertices)
            {
                this.minimumWeights.Add(u, double.MaxValue);
                this.queue.Enqueue(u);
            }
        }

        private void CleanUp()
        {
            this.minimumWeights = null;
            this.queue = null;
        }
    }
}
