using System;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Matrix;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [PexClass]
    [TypeFixture(typeof(DoubleDenseMatrix))]
    [TypeFactory(typeof(DoubleDenseMatrixFactory))]
    public partial class DoubleDenseMatrixTest
    {
        [Test, PexTest]
        public void ToString([PexAssumeIsNotNull]DoubleDenseMatrix matrix)
        {
            Console.WriteLine(matrix);
        }

        [Test, PexTest]
        public void WriteMatrix([PexAssumeIsNotNull]DoubleDenseMatrix matrix)
        {
            matrix.WriteMatrix(Console.Out);
        }

        [Test, PexTest]
        public void SelfSimilarity([PexAssumeIsNotNull]DoubleDenseMatrix matrix)
        {
            Console.WriteLine("Matrix");
            matrix.WriteMatrix(Console.Out);

            Console.WriteLine("Similarity");
            DoubleDenseMatrix similarity = DoubleDenseMatrix.Similarity(matrix, matrix, 0.001);
            similarity.WriteMatrix(Console.Out);
        }
    }
}
