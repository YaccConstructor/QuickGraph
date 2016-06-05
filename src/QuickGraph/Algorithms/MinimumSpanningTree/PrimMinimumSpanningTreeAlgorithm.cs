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
            var edges = new List<TEdge>();
            var ds = new ForestDisjointSet<TVertex>(this.VisitedGraph.VertexCount);
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (visetedVert.Count == 0)
                    visetedVert.Add(v);
                ds.MakeSet(v);
            }

            if (cancelManager.IsCancelling)
                return;
            var lastVert = visetedVert[visetedVert.Count - 1];
            foreach (var edge in this.VisitedGraph.Edges)
                if ((lastVert.Equals(edge.Source) && !visetedVert.Contains(edge.Target)) || (lastVert.Equals(edge.Target) && !visetedVert.Contains(edge.Source)))
                    edges.Add(edge);

            if (cancelManager.IsCancelling)
                return;

            while (edges.Count > 0 && visetedVert.Count < VisitedGraph.VertexCount)
            {
                var min = edgeWeights(edges[0]);
                var mined = edges[0];
                foreach (var ed in edges)
                {
                    if (min > edgeWeights(ed))
                    {
                        min = edgeWeights(ed);
                        mined = ed;
                    }
                }
                this.OnExamineEdge(mined);
                if (!ds.AreInSameSet(mined.Source, mined.Target))
                {
                    this.OnTreeEdge(mined);
                    ds.Union(mined.Source, mined.Target);
                    if (visetedVert.Contains(mined.Source))
                        visetedVert.Add(mined.Target);
                    else
                        visetedVert.Add(mined.Source);
                    edges.Remove(mined);
                    lastVert = visetedVert[visetedVert.Count - 1];
                    foreach (var edge in this.VisitedGraph.Edges)
                        if ((lastVert.Equals(edge.Source) && !visetedVert.Contains(edge.Target)) || (lastVert.Equals(edge.Target) && !visetedVert.Contains(edge.Source)))
                            edges.Add(edge);
                }
                else
                    edges.Remove(mined);
            }
        }
    }
}
