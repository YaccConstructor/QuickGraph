using System;
using System.Collections.Generic;

using QuickGraph.Collections;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class SourceFirstTopologicalSortAlgorithm<Vertex, Edge> :
        AlgorithmBase<IVertexAndEdgeListGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, int> inDegrees = new Dictionary<Vertex, int>();
        private PriorithizedVertexBuffer<Vertex,int> heap;
        private IList<Vertex> sortedVertices = new List<Vertex>();

        public SourceFirstTopologicalSortAlgorithm(
            IVertexAndEdgeListGraph<Vertex,Edge> visitedGraph
            )
            :base(visitedGraph)
        {
            this.heap = new PriorithizedVertexBuffer<Vertex,int>(this.inDegrees);
        }

        public ICollection<Vertex> SortedVertices
        {
            get
            {
                return this.sortedVertices;
            }
        }

        public PriorithizedVertexBuffer<Vertex,int> Heap
        {
            get
            {
                return this.heap;
            }
        }

        public IDictionary<Vertex,int> InDegrees
        {
            get
            {
                return this.inDegrees;
            }
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
                if (this.inDegrees[v] != 0)
                    throw new NonAcyclicGraphException();

                this.sortedVertices.Add(v);
                this.OnAddVertex(v);

                // update the count of it's adjacent vertices
                foreach (Edge e in this.VisitedGraph.OutEdges(v))
                {
                    if (e.Source.Equals(e.Target))
                        continue;

                    this.inDegrees[e.Target]--;
                    if (this.inDegrees[e.Target] < 0)
                        throw new InvalidOperationException("InDegree is negative, and cannot be");
                    this.heap.Update(e.Target);
                }
            }
        }

        private void InitializeInDegrees()
        {
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                this.inDegrees.Add(v, 0);
                this.heap.Push(v);
            }

            foreach (Edge e in this.VisitedGraph.Edges)
            {
                if (e.Source.Equals(e.Target))
                    continue;
                this.inDegrees[e.Target]++;
            }

            this.heap.Sort();
        }
    }
}
