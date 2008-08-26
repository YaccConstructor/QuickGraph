using System;
using System.Collections.Generic;

using QuickGraph.Collections;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class UndirectedFirstTopologicalSortAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> degrees = new Dictionary<TVertex, int>();
        private PriorithizedVertexBuffer<TVertex, int> heap;
        private IList<TVertex> sortedVertices = new List<TVertex>();
        private bool allowCyclicGraph = false;

        public UndirectedFirstTopologicalSortAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph
            )
            : base(visitedGraph)
        {
            this.heap = new PriorithizedVertexBuffer<TVertex, int>(this.degrees);
        }

        public ICollection<TVertex> SortedVertices
        {
            get
            {
                return this.sortedVertices;
            }
        }

        public PriorithizedVertexBuffer<TVertex, int> Heap
        {
            get
            {
                return this.heap;
            }
        }

        public IDictionary<TVertex, int> Degrees
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

        public event VertexEventHandler<TVertex> AddVertex;
        private void OnAddVertex(TVertex v)
        {
            if (this.AddVertex != null)
                this.AddVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public void Compute(IList<TVertex> vertices)
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

                TVertex v = this.heap.Pop();
                if (this.degrees[v] != 0 && !this.AllowCyclicGraph)
                    throw new NonAcyclicGraphException();

                this.sortedVertices.Add(v);
                this.OnAddVertex(v);

                // update the count of it's adjacent vertices
                foreach (var e in this.VisitedGraph.AdjacentEdges(v))
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
            foreach (var v in this.VisitedGraph.Vertices)
            {
                this.degrees.Add(v, this.VisitedGraph.AdjacentDegree(v));
                this.heap.Push(v);
            }

            this.heap.Sort();
        }
    }
}
