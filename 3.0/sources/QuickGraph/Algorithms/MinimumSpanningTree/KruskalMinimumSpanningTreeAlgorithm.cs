using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Collections;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MinimumSpanningTree
{
    [Serializable]
    public sealed class KruskalMinimumSpanningTreeAlgorithm<TVertex,TEdge> 
        : RootedAlgorithmBase<TVertex,IUndirectedGraph<TVertex,TEdge>>
        , ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly Func<TEdge, double> edgeWeights;

        public KruskalMinimumSpanningTreeAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights
            )
            : this(null, visitedGraph, edgeWeights)
        {}

        public KruskalMinimumSpanningTreeAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights
            )
            :base(host, visitedGraph)
        {
            Contract.Requires(edgeWeights != null);

            this.edgeWeights = edgeWeights;
        }

        public event EdgeEventHandler<TVertex, TEdge> ExamineEdge;
        private void OnExamineEdge(TEdge edge)
        {
            var eh = this.ExamineEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex, TEdge>(edge));
        }

        public event EdgeEventHandler<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge edge)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new EdgeEventArgs<TVertex, TEdge>(edge));
        }

        protected override void InternalCompute()
        {
            var ds = new ForestDisjointSet<TVertex>(this.VisitedGraph.VertexCount);
            foreach (var v in this.VisitedGraph.Vertices)
                ds.MakeSet(v);

            var queue = new BinaryQueue<TEdge, double>(this.edgeWeights);
            foreach (var e in this.VisitedGraph.Edges)
                queue.Enqueue(e);

            int vertexCount = this.VisitedGraph.VertexCount;
            while (queue.Count > 0)
            {
                var e = queue.Dequeue();
                this.OnExamineEdge(e);
                if (!ds.AreInSameSet(e.Source, e.Target))
                {
                    this.OnTreeEdge(e);
                    ds.Union(e.Source, e.Target);
                }
            }
        }
    }
}
