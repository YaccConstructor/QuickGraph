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
            var dic = new Dictionary<TVertex, HashSet<TEdge>>();
            var cancelManager = this.Services.CancelManager;
            var visetedVert = new HashSet<TVertex>();
            var edges = new HashSet<TEdge>();
            var queue = new BinaryQueue<TEdge, double>(this.edgeWeights);
            var ds = new ForestDisjointSet<TVertex>(this.VisitedGraph.VertexCount);
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (visetedVert.Count == 0)
                {
                    visetedVert.Add(v);
                }
                ds.MakeSet(v);
                dic.Add(v, new HashSet<TEdge>());
            }
            foreach (var e in this.VisitedGraph.Edges)
            {
                dic[e.Source].Add(e);
                dic[e.Target].Add(e);
            }

            if (cancelManager.IsCancelling)
                return;
            var enumerator = visetedVert.GetEnumerator();
            enumerator.MoveNext();
            var lastVert = enumerator.Current;
            foreach (var edge in dic[lastVert])
                if (!edges.Contains(edge))
                {
                    edges.Add(edge);
                    queue.Enqueue(edge);
                }
            if (cancelManager.IsCancelling)
                return;

            while (edges.Count > 0 && visetedVert.Count < VisitedGraph.VertexCount)
            {
                var mined = queue.Dequeue();
                this.OnExamineEdge(mined);
                if (!ds.AreInSameSet(mined.Source, mined.Target))
                {
                    this.OnTreeEdge(mined);
                    ds.Union(mined.Source, mined.Target);
                    if (visetedVert.Contains(mined.Source))
                    {
                        lastVert = mined.Target;
                        visetedVert.Add(mined.Target);
                    }
                    else
                    {
                        lastVert = mined.Source;
                        visetedVert.Add(mined.Source);
                    }
                    foreach (var edge in dic[lastVert])
                        if (!edges.Contains(edge))
                        {
                            edges.Add(edge);
                            queue.Enqueue(edge);
                        }
                }
            }
        }
    }
}
