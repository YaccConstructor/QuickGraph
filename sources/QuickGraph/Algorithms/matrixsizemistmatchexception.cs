using System;
using QuickGraph.Algorithms.Matrix;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public class MatrixSizeMistmatchException : ApplicationException
    {
        public MatrixSizeMistmatchException(
            DoubleDenseMatrix left,
            DoubleDenseMatrix right)
            :this(String.Format("Matrix size ({0}x{1}) does not match ({2}x{3})",
                left.RowCount,left.ColumnCount,right.RowCount, right.ColumnCount))
        {
        }
        public MatrixSizeMistmatchException() { }
        public MatrixSizeMistmatchException(string message) : base(message) { }
        public MatrixSizeMistmatchException(string message, Exception inner) : base(message, inner) { }
        protected MatrixSizeMistmatchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
