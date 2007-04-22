using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Algorithms.Matrix;

namespace QuickGraph.Algorithms
{
    public sealed class VertexAdjacencyMatrixBuilderAlgorithm<Vertex, Edge> : AlgorithmBase<IVertexListGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private DoubleDenseMatrix matrix;
        private Dictionary<Vertex, int> vertexIndices;

        public VertexAdjacencyMatrixBuilderAlgorithm(IVertexListGraph<Vertex, Edge> visitedGraph)
            :base(visitedGraph)
        {
        }

        public DoubleDenseMatrix Matrix
        {
            get { return this.matrix; }
        }

        public Dictionary<Vertex, int> VertexIndices
        {
            get { return this.vertexIndices; }
        }

        protected override void InternalCompute()
        {
            this.matrix = new DoubleDenseMatrix(this.VisitedGraph.VertexCount, this.VisitedGraph.VertexCount);
            this.vertexIndices = new Dictionary<Vertex, int>(this.VisitedGraph.VertexCount);

            int index = 0;
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                this.vertexIndices.Add(v, index++);
            }

            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                int source = this.VertexIndices[v];
                foreach (Edge edge in this.VisitedGraph.OutEdges(v))
                {
                    int target = this.VertexIndices[edge.Target];

                    matrix[source, target] = 1;
                }
            }
        }
    }
}
