using System;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Matrix;

namespace QuickGraph.Algorithms
{
    [TypeFixture(typeof(DoubleDenseMatrix))]
    [TypeFactory(typeof(DoubleDenseMatrixFactory))]
    public sealed class DoubleDenseMatrixTest
    {
        [Test]
        public void ToString(DoubleDenseMatrix matrix)
        {
            Console.WriteLine(matrix);
        }

        [Test]
        public void WriteMatrix(DoubleDenseMatrix matrix)
        {
            matrix.WriteMatrix(Console.Out);
        }

        [Test]
        public void SelfSimilarity(DoubleDenseMatrix matrix)
        {
            Console.WriteLine("Matrix");
            matrix.WriteMatrix(Console.Out);

            Console.WriteLine("Similarity");
            DoubleDenseMatrix similarity = DoubleDenseMatrix.Similarity(matrix, matrix, 0.001);
            similarity.WriteMatrix(Console.Out);
        }
    }
}
