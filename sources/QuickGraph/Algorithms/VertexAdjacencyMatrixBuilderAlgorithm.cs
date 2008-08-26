using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Algorithms.Matrix;

namespace QuickGraph.Algorithms
{
    public sealed class VertexAdjacencyMatrixBuilderAlgorithm<TVertex, TEdge> : 
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private DoubleDenseMatrix matrix;
        private Dictionary<TVertex, int> vertexIndices;

        public VertexAdjacencyMatrixBuilderAlgorithm(IVertexListGraph<TVertex, TEdge> visitedGraph)
            :base(visitedGraph)
        {
        }

        public DoubleDenseMatrix Matrix
        {
            get { return this.matrix; }
        }

        public Dictionary<TVertex, int> VertexIndices
        {
            get { return this.vertexIndices; }
        }

        protected override void InternalCompute()
        {
            this.matrix = new DoubleDenseMatrix(this.VisitedGraph.VertexCount, this.VisitedGraph.VertexCount);
            this.vertexIndices = new Dictionary<TVertex, int>(this.VisitedGraph.VertexCount);

            int index = 0;
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                this.vertexIndices.Add(v, index++);
            }

            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;
                int source = this.VertexIndices[v];
                foreach (var edge in this.VisitedGraph.OutEdges(v))
                {
                    int target = this.VertexIndices[edge.Target];

                    matrix[source, target] = 1;
                }
            }
        }
    }
}
