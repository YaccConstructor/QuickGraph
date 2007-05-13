using System;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Matrix;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class DoubleDenseMatrixTest
    {
        [PexTest]
        public void ToString([PexAssumeIsNotNull]DoubleDenseMatrix matrix)
        {
            Console.WriteLine(matrix);
        }

        [PexTest]
        public void WriteMatrix([PexAssumeIsNotNull]DoubleDenseMatrix matrix)
        {
            matrix.WriteMatrix(Console.Out);
        }

        [PexTest]
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
