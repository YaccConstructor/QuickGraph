using System;
using System.Collections.Generic;

using QuickGraph.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.TopologicalSort
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class SourceFirstTopologicalSortAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexAndEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> inDegrees = new Dictionary<TVertex, int>();
        private BinaryQueue<TVertex,int> heap;
        private IList<TVertex> sortedVertices = new List<TVertex>();

        public SourceFirstTopologicalSortAlgorithm(
            IVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph
            )
            :base(visitedGraph)
        {
            this.heap = new BinaryQueue<TVertex,int>(e => this.inDegrees[e]);
        }

        public ICollection<TVertex> SortedVertices
        {
            get
            {
                return this.sortedVertices;
            }
        }

        public BinaryQueue<TVertex,int> Heap
        {
            get
            {
                return this.heap;
            }
        }

        public IDictionary<TVertex,int> InDegrees
        {
            get
            {
                return this.inDegrees;
            }
        }

        public event VertexAction<TVertex> AddVertex;
        private void OnAddVertex(TVertex v)
        {
            var eh = this.AddVertex;
            if (eh != null)
                eh(v);
        }

        public void Compute(IList<TVertex> vertices)
        {
            Contract.Requires(vertices != null);

            this.sortedVertices = vertices;
            Compute();
        }


        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;
            this.InitializeInDegrees();

            while (this.heap.Count != 0)
            {
                if (cancelManager.IsCancelling) break;

                TVertex v = this.heap.Dequeue();
                if (this.inDegrees[v] != 0)
                    throw new NonAcyclicGraphException();

                this.sortedVertices.Add(v);
                this.OnAddVertex(v);

                // update the count of it's adjacent vertices
                foreach (var e in this.VisitedGraph.OutEdges(v))
                {
                    if (e.Source.Equals(e.Target))
                        continue;

                    this.inDegrees[e.Target]--;
                    Contract.Assert(this.inDegrees[e.Target] >= 0);
                    this.heap.Update(e.Target);
                }
            }
        }

        private void InitializeInDegrees()
        {
            foreach (var v in this.VisitedGraph.Vertices)
            {
                this.inDegrees.Add(v, 0);
                this.heap.Enqueue(v);
            }

            foreach (var e in this.VisitedGraph.Edges)
            {
                if (e.Source.Equals(e.Target))
                    continue;
                this.inDegrees[e.Target]++;
            }
        }
    }
}
