using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.TopologicalSort
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class UndirectedFirstTopologicalSortAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> degrees = new Dictionary<TVertex, int>();
        private BinaryQueue<TVertex, int> heap;
        private IList<TVertex> sortedVertices = new List<TVertex>();
        private bool allowCyclicGraph = false;

        public UndirectedFirstTopologicalSortAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph
            )
            : base(visitedGraph)
        {
            this.heap = new BinaryQueue<TVertex, int>(e => this.degrees[e]);
        }

        public ICollection<TVertex> SortedVertices
        {
            get
            {
                return this.sortedVertices;
            }
        }

        public BinaryQueue<TVertex, int> Heap
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

        public event VertexAction<TVertex> AddVertex;
        private void OnAddVertex(TVertex v)
        {
            if (this.AddVertex != null)
                this.AddVertex(v);
        }

        public void Compute(IList<TVertex> vertices)
        {
            Contract.Requires(vertices != null);

            this.sortedVertices = vertices;
            Compute();
        }


        protected override void InternalCompute()
        {
            this.InitializeInDegrees();
            var cancelManager = this.Services.CancelManager;

            while (this.heap.Count != 0)
            {
                if (cancelManager.IsCancelling) return;

                TVertex v = this.heap.Dequeue();
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
                    if (this.heap.Contains(e.Target))
                        this.heap.Update(e.Target);
                }
            }
        }

        private void InitializeInDegrees()
        {
            foreach (var v in this.VisitedGraph.Vertices)
            {
                this.degrees.Add(v, this.VisitedGraph.AdjacentDegree(v));
                this.heap.Enqueue(v);
            }
        }
    }
}
