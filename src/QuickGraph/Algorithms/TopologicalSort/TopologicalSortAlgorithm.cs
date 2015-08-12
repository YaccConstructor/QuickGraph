using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.TopologicalSort
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class TopologicalSortAlgorithm<TVertex,TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IList<TVertex> vertices = new List<TVertex>();
        private bool allowCyclicGraph = false;

        public TopologicalSortAlgorithm(IVertexListGraph<TVertex,TEdge> g)
            :this(g, new List<TVertex>())
        {}

        public TopologicalSortAlgorithm(
            IVertexListGraph<TVertex,TEdge> g, 
            IList<TVertex> vertices)
            :base(g)
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

        private void FinishVertex(TVertex v)
        {
            vertices.Insert(0, v);
        }

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
                dfs.FinishVertex += new VertexAction<TVertex>(this.FinishVertex);

                dfs.Compute();
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.BackEdge -= new EdgeAction<TVertex, TEdge>(this.BackEdge);
                    dfs.FinishVertex -= new VertexAction<TVertex>(this.FinishVertex);
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
