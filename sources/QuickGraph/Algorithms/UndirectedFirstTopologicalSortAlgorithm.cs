using System;
using System.Collections.Generic;

using QuickGraph.Collections;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class UndirectedFirstTopologicalSortAlgorithm<Vertex, Edge> :
        AlgorithmBase<IUndirectedGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, int> degrees = new Dictionary<Vertex, int>();
        private PriorithizedVertexBuffer<Vertex, int> heap;
        private IList<Vertex> sortedVertices = new List<Vertex>();
        private bool allowCyclicGraph = false;

        public UndirectedFirstTopologicalSortAlgorithm(
            IUndirectedGraph<Vertex, Edge> visitedGraph
            )
            : base(visitedGraph)
        {
            this.heap = new PriorithizedVertexBuffer<Vertex, int>(this.degrees);
        }

        public ICollection<Vertex> SortedVertices
        {
            get
            {
                return this.sortedVertices;
            }
        }

        public PriorithizedVertexBuffer<Vertex, int> Heap
        {
            get
            {
                return this.heap;
            }
        }

        public IDictionary<Vertex, int> Degrees
        {
            get
            {
                return this.degrees;
            }
        }


        public bool AllowCyclicGraph
        {
            get { return this.allowCyclicGraph; }
            set { this.allowCyclicGraph = value; }
        }

        public event VertexEventHandler<Vertex> AddVertex;
        private void OnAddVertex(Vertex v)
        {
            if (this.AddVertex != null)
                this.AddVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public void Compute(IList<Vertex> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");
            this.sortedVertices = vertices;
            Compute();
        }


        protected override void InternalCompute()
        {
            this.InitializeInDegrees();

            while (this.heap.Count != 0)
            {
                if (this.IsAborting)
                    return;

                Vertex v = this.heap.Pop();
                if (this.degrees[v] != 0 && !this.AllowCyclicGraph)
                    throw new NonAcyclicGraphException();

                this.sortedVertices.Add(v);
                this.OnAddVertex(v);

                // update the count of it's adjacent vertices
                foreach (Edge e in this.VisitedGraph.AdjacentEdges(v))
                {
                    if (e.Source.Equals(e.Target))
                        continue;

                    this.degrees[e.Target]--;
                    if (this.degrees[e.Target] < 0 && !this.AllowCyclicGraph)
                        throw new InvalidOperationException("Degree is negative, and cannot be");
                    this.heap.Update(e.Target);
                }
            }
        }

        private void InitializeInDegrees()
        {
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                this.degrees.Add(v, this.VisitedGraph.AdjacentDegree(v));
                this.heap.Push(v);
            }

            this.heap.Sort();
        }
    }
}
