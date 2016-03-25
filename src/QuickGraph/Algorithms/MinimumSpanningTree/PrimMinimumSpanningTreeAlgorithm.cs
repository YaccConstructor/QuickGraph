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
            var ds = new HashSet<TVertex>();
            var x = this.VisitedGraph.Vertices.GetEnumerator();
            x.MoveNext();
            var z = x.Current;
            ds.Add(z);

            if (cancelManager.IsCancelling)
                return;

            var queue = new BinaryQueue<TEdge, double>(this.edgeWeights);
            foreach (var e in this.VisitedGraph.Edges)
            {
                if (ds.Contains(e.Source) || ds.Contains(e.Target))
                {
                    queue.Enqueue(e);
                }
            }

            if (cancelManager.IsCancelling)
                return;

            while (ds.Count != VisitedGraph.VertexCount)
            {
                var e = queue.Dequeue();
                this.OnExamineEdge(e);
                if (!ds.Contains(e.Source))
                {
                    this.OnTreeEdge(e);
                    foreach (var ed in this.VisitedGraph.Edges)
                    {
                        if ((ed.Source.Equals(e.Source) && !ds.Contains(ed.Target)) || (ed.Target.Equals(e.Source) && !ds.Contains(ed.Source)))
                        {
                            queue.Enqueue(ed);
                        }
                    }
                }
                else if (!ds.Contains(e.Target))
                {
                    this.OnTreeEdge(e);
                    foreach (var ed in this.VisitedGraph.Edges)
                    {
                        if ((ed.Source.Equals(e.Target) && !ds.Contains(ed.Target)) || (ed.Target.Equals(e.Target) && !ds.Contains(ed.Source)))
                        {
                            queue.Enqueue(ed);
                        }
                    }
                }
            }
        }
    }
}
