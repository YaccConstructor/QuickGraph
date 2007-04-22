using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class UndirectedTopologicalSortAlgorithm<Vertex, Edge> :
        AlgorithmBase<IUndirectedGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IList<Vertex> vertices;
        private UndirectedDepthFirstSearchAlgorithm<Vertex, Edge> dfs;
        private bool allowCyclicGraph = false;

        public UndirectedTopologicalSortAlgorithm(IUndirectedGraph<Vertex, Edge> g)
            : this(g, new List<Vertex>())
        { }

        public UndirectedTopologicalSortAlgorithm(
            IUndirectedGraph<Vertex, Edge> g,
            IList<Vertex> vertices)
            : base(g)
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");

            this.vertices = vertices;
            this.dfs = new UndirectedDepthFirstSearchAlgorithm<Vertex, Edge>(VisitedGraph);
            this.dfs.BackEdge += new EdgeEventHandler<Vertex, Edge>(this.BackEdge);
            this.dfs.FinishVertex += new VertexEventHandler<Vertex>(this.FinishVertex);
        }

        public IList<Vertex> SortedVertices
        {
            get
            {
                return vertices;
            }
        }

        public bool AllowCyclicGraph
        {
            get { return this.allowCyclicGraph; }
            set { this.allowCyclicGraph = value; }
        }

        private void BackEdge(Object sender, EdgeEventArgs<Vertex, Edge> args)
        {
            if (!this.AllowCyclicGraph)
                throw new NonAcyclicGraphException();
        }

        private void FinishVertex(Object sender, VertexEventArgs<Vertex> args)
        {
            vertices.Insert(0, args.Vertex);
        }

        protected override void InternalCompute()
        {
            this.dfs.Compute();
        }

        public override void Abort()
        {
            this.dfs.Abort();
            base.Abort();
        }

        public void Compute(IList<Vertex> vertices)
        {
            this.vertices = vertices;
            this.vertices.Clear();
            this.Compute();
        }
    }
}
