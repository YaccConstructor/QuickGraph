using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Collections;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MinimumSpanningTree
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class PrimMinimumSpanningTreeAlgorithm<TVertex, TEdge>
        : AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>
        , IMinimumSpanningTreeAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly Func<TEdge, double> edgeWeights;

        public PrimMinimumSpanningTreeAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights
            )
            : this(null, visitedGraph, edgeWeights)
        { }

        public PrimMinimumSpanningTreeAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights
            )
            : base(host, visitedGraph)
        {
            Contract.Requires(edgeWeights != null);

            this.edgeWeights = edgeWeights;
        }

        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge edge)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(edge);
        }

        public event EdgeAction<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge edge)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(edge);
        }

        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;
            var visetedVert = new List<TVertex>();
            var visetedEdges = new List<TEdge>();
            var ds = new ForestDisjointSet<TVertex>(this.VisitedGraph.VertexCount);
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (visetedVert.Count == 0)
                    visetedVert.Add(v);
                ds.MakeSet(v);
            }

            if (cancelManager.IsCancelling)
                return;

            var queue = new BinaryQueue<TEdge, double>(this.edgeWeights);
            foreach (var edge in this.VisitedGraph.Edges)
                if (!visetedEdges.Contains(edge) && (visetedVert.Contains(edge.Source) || visetedVert.Contains(edge.Target)))
                {
                    queue.Enqueue(edge);
                    visetedEdges.Add(edge);
                }

            if (cancelManager.IsCancelling)
                return;

            while (queue.Count > 0)
            {
                foreach (var edge in this.VisitedGraph.Edges)
                    if (!visetedEdges.Contains(edge) && (visetedVert.Contains(edge.Source) || visetedVert.Contains(edge.Target)))
                    {
                        queue.Enqueue(edge);
                        visetedEdges.Add(edge);
                    }
                var e = queue.Dequeue();
                this.OnExamineEdge(e);
                if (!ds.AreInSameSet(e.Source, e.Target))
                {
                    this.OnTreeEdge(e);
                    ds.Union(e.Source, e.Target);
                    if (visetedVert.Contains(e.Source))
                        visetedVert.Add(e.Target);
                    else
                        visetedVert.Add(e.Source);
                }
            }
        }
    }
}
