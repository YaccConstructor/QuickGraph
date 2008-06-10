using System;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Matrix;
using Microsoft.Pex.Framework;

namespace QuickGraph.Algorithms
{
    [TestFixture, PexClass]
    public partial class DoubleDenseMatrixTest
    {
        [PexMethod]
        public void ToString([PexAssumeNotNull]DoubleDenseMatrix matrix)
        {
            Console.WriteLine(matrix);
        }

        [PexMethod]
        public void WriteMatrix([PexAssumeNotNull]DoubleDenseMatrix matrix)
        {
            matrix.WriteMatrix(Console.Out);
        }

        [PexMethod]
        public void SelfSimilarity([PexAssumeNotNull]DoubleDenseMatrix matrix)
        {
            Console.WriteLine("Matrix");
            matrix.WriteMatrix(Console.Out);

            Console.WriteLine("Similarity");
            DoubleDenseMatrix similarity = DoubleDenseMatrix.Similarity(matrix, matrix, 0.001);
            similarity.WriteMatrix(Console.Out);
        }
    }
}
