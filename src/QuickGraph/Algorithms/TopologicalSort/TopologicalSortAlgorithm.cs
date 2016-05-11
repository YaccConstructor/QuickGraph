using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.TopologicalSort
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class TopologicalSortAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IList<TVertex> vertices = new List<TVertex>();
        private bool allowCyclicGraph = false;

        public TopologicalSortAlgorithm(IVertexListGraph<TVertex, TEdge> g)
            : this(g, new List<TVertex>())
        { }

        public TopologicalSortAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            IList<TVertex> vertices)
            : base(g)
        {
            Contract.Requires(vertices != null);

            this.vertices = vertices;
        }


        public IList<TVertex> SortedVertices
        {
            get
            {
                return vertices;
            }
        }

        public bool AllowCyclicGraph
        {
            get { return this.allowCyclicGraph; }
        }

        private void BackEdge(TEdge args)
        {
            if (!this.AllowCyclicGraph)
                throw new NonAcyclicGraphException();
        }

        private void VertexFinished(TVertex v)
        {
            vertices.Insert(0, v);
        }
        public event VertexAction<TVertex> DiscoverVertex;
        public event VertexAction<TVertex> FinishVertex;
        /*
        public event VertexAction<TVertex> InitializeVertex;
        public event VertexAction<TVertex> StartVertex;
        public event VertexAction<TVertex> DiscoverVertex;
        public event EdgeAction<TVertex, TEdge> ExamineEdge;
        public event EdgeAction<TVertex, TEdge> TreeEdge;
        public event EdgeAction<TVertex, TEdge> BackEdge;
        public event EdgeAction<TVertex, TEdge> ForwardOrCrossEdge;
        */
        protected override void InternalCompute()
        {
            DepthFirstSearchAlgorithm<TVertex, TEdge> dfs = null;
            try
            {
                dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount)
                    );
                dfs.BackEdge += new EdgeAction<TVertex, TEdge>(this.BackEdge);
                dfs.FinishVertex += new VertexAction<TVertex>(this.VertexFinished);

                /*
                dfs.InitializeVertex += InitializeVertex;
                dfs.DiscoverVertex += DiscoverVertex;
                dfs.ExamineEdge += ExamineEdge;
                dfs.TreeEdge += TreeEdge;
                dfs.BackEdge += BackEdge;
                dfs.ForwardOrCrossEdge += ForwardOrCrossEdge;
                */
                dfs.DiscoverVertex += DiscoverVertex;
                dfs.FinishVertex += FinishVertex;


                dfs.Compute();
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.BackEdge -= new EdgeAction<TVertex, TEdge>(this.BackEdge);
                    dfs.FinishVertex -= new VertexAction<TVertex>(this.VertexFinished);
                }
            }
        }

        public void Compute(IList<TVertex> vertices)
        {
            this.vertices = vertices;
            this.vertices.Clear();
            this.Compute();
        }
    }
}