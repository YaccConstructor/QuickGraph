using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;

namespace QuickGraph.Algorithms
{
    [Serializable]
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
            if (vertices == null)
                throw new ArgumentNullException("vertices");

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

        private void BackEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            if (!this.AllowCyclicGraph)
                throw new NonAcyclicGraphException();
        }

        private void FinishVertex(Object sender, VertexEventArgs<TVertex> args)
        {
            vertices.Insert(0, args.Vertex);
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
                dfs.BackEdge += new EdgeEventHandler<TVertex, TEdge>(this.BackEdge);
                dfs.FinishVertex += new VertexEventHandler<TVertex>(this.FinishVertex);

                dfs.Compute();
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.BackEdge -= new EdgeEventHandler<TVertex, TEdge>(this.BackEdge);
                    dfs.FinishVertex -= new VertexEventHandler<TVertex>(this.FinishVertex);
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
