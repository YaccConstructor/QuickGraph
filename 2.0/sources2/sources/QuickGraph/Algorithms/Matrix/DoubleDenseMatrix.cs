using System;
using System.IO;
namespace QuickGraph.Algorithms.Matrix
{
    [Serializable]
    public sealed class DoubleDenseMatrix : ICloneable
    {
        private readonly double[] data;
        private readonly int rowCount;
        private readonly int columnCount;

        public static DoubleDenseMatrix Create(DoubleDenseMatrix matrix)
        {
            return new DoubleDenseMatrix(matrix.RowCount, matrix.ColumnCount, matrix.GetData());
        }

        public static DoubleDenseMatrix Create(int rowCount, int columnCount, Double value)
        {
            // <preconditions>
            if (columnCount * rowCount < 0)
                throw new ArgumentException("columnCount * rowCount < 0");
            // </preconditions>
            double[] data = new double[rowCount * columnCount];
            for (int i = 0; i < data.Length; ++i)
                data[i] = value;

            return new DoubleDenseMatrix(rowCount, columnCount, data);
        }

        public DoubleDenseMatrix (int rowCount, int columnCount)
        {
            // <preconditions>
            if (rowCount < 0)
                throw new ArgumentOutOfRangeException("rowCount");
            if (columnCount < 0)
                throw new ArgumentOutOfRangeException("rowCount");
            if (columnCount * rowCount < 0)
                throw new ArgumentException("columnCount * rowCount < 0");
            // </preconditions>

            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.data = new double[rowCount * columnCount];
        }

        private DoubleDenseMatrix(int rowCount, int columnCount, double[] data)
        {
            if (rowCount < 0)
                throw new ArgumentOutOfRangeException("rowCount");
            if (columnCount < 0)
                throw new ArgumentOutOfRangeException("rowCount");
            if (rowCount * columnCount != data.Length)
                throw new ArgumentOutOfRangeException("data");

            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.data = data;
        }

        private void VerifySize(DoubleDenseMatrix matrix)
        {
            if (this.RowCount != matrix.RowCount)
                throw new MatrixSizeMistmatchException();
            if (this.ColumnCount != matrix.ColumnCount)
                throw new MatrixSizeMistmatchException();
        }

        #region Basic matrix
        public int RowCount
        {
            get { return this.rowCount; }
        }

        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        public int Count
        {
            get { return this.rowCount * this.columnCount; }
        }

        public double[] GetData()
        {
            return this.data;
        }

        public double this[int row, int column]
        {
            get { return this.data[row + column * this.RowCount]; }
            set { this.data[row + column * this.RowCount] = value; }
        }
        #endregion

        #region Cloning
        public DoubleDenseMatrix Clone()
        {
            return new DoubleDenseMatrix(
                this.RowCount,
                this.ColumnCount,
                (double[])this.data.Clone()
                );
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Basic operations
        public DoubleDenseMatrix Add(DoubleDenseMatrix matrix)
        {
            VerifySize(matrix);

            for (int i = 0; i < data.Length; ++i)
                this.data[i] += matrix.data[i];

            return this;
        }

        public static DoubleDenseMatrix Add(DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            // <preconditions>
            if (left == (DoubleDenseMatrix)null)
                throw new ArgumentNullException("left");
            // </preconditions>
            DoubleDenseMatrix m = Create(left);
            return m.Add(right);
        }

        public static DoubleDenseMatrix operator +(DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            return Add(left, right);
        }

        public DoubleDenseMatrix Sub(DoubleDenseMatrix matrix)
        {
            VerifySize(matrix);

            for (int i = 0; i < data.Length; ++i)
                this.data[i] -= matrix.data[i];

            return this;
        }

        public static DoubleDenseMatrix Sub(DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            DoubleDenseMatrix m = Create(left);
            return m.Sub(right);
        }

        public static DoubleDenseMatrix operator - (DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            return Sub(left, right);
        }

        public DoubleDenseMatrix Mul(double factor)
        {
            for (int i = 0; i < data.Length; ++i)
                this.data[i] *= factor;
            return this;
        }

        public static DoubleDenseMatrix Mul(DoubleDenseMatrix left, double factor)
        {
            DoubleDenseMatrix m = Create(left);
            return m.Mul(factor);
        }

        public static DoubleDenseMatrix operator * (DoubleDenseMatrix left, double factor)
        {
            return Mul(left, factor);
        }

        public static DoubleDenseMatrix Mul(double factor, DoubleDenseMatrix right)
        {
            DoubleDenseMatrix m = Create(right);
            return m.Mul(factor);
        }

        public static DoubleDenseMatrix operator *(double factor, DoubleDenseMatrix right)
        {
            return Mul(factor, right);
        }

        public static DoubleDenseMatrix Mul(DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            // <preconditions>
            if (left == (DoubleDenseMatrix)null)
                throw new ArgumentNullException("left");
            if (right == (DoubleDenseMatrix)null)
                throw new ArgumentNullException("right");
            // </preconditions>
            if (left.ColumnCount != right.RowCount)
                throw new MatrixSizeMistmatchException();

            DoubleDenseMatrix m = new DoubleDenseMatrix(left.RowCount, right.ColumnCount);
            for (int i = 0; i < left.RowCount; ++i)
            {
                for (int j = 0; j < left.ColumnCount; ++j)
                {
                    for (int k = 0; k < right.ColumnCount; ++k)
                    {
                        m[i,k] += left[i,j] * right[j,k];
                    }
                }
            }

            return m;
        }

        public static DoubleDenseMatrix operator *(DoubleDenseMatrix left, DoubleDenseMatrix right)
        {
            return Mul(left, right);
        }

        public DoubleDenseMatrix Div(double factor)
        {
            for (int i = 0; i < data.Length; ++i)
                this.data[i] = checked(this.data[i]/factor);

            return this;
        }
        #endregion

        #region norms
        public double GetNorm1()
        {
            double norm = 0;
            for (int i = 0; i < this.RowCount; ++i)
            {
                double sum = 0;
                for (int j = 0; j < this.ColumnCount; ++j)
                {
                    sum += this[i, j];
                }

                norm = Math.Max(sum, norm);
            }

            return norm;
        }

        public double GetNormInfinity()
        {
            double norm = 0;
            for (int j = 0; j < this.ColumnCount; ++j)
            {
                double sum = 0;
                for (int i = 0; i < this.RowCount; ++i)
                {
                    sum += this[i, j];
                }
                norm = Math.Max(sum, norm);
            }

            return norm;
        }

        public double GetNorm2()
        {
            double norm = 0;
            for (int i = 0; i < this.Count; ++i)
            {
                norm += this.data[i] * this.data[i];
            }
            return Math.Sqrt(norm);
        }
        #endregion

        #region special methods
        public DoubleDenseMatrix Trace()
        {
            if (this.RowCount != this.ColumnCount)
                throw new MatrixSizeMistmatchException();
            DoubleDenseMatrix m = new DoubleDenseMatrix(this.RowCount,1);
            for (int i = 0; i < this.RowCount; ++i)
                m.data[i] = this[i, i];

            return m;
        }

        public DoubleDenseMatrix Transpose()
        {
            DoubleDenseMatrix m = new DoubleDenseMatrix(this.ColumnCount, this.RowCount);
            for (int i = 0; i < this.RowCount; ++i)
                for (int j = 0; j < this.ColumnCount; ++j)
                    m[j, i] = this[i, j];
            return m;
        }
        #endregion

        #region Factories
        public static DoubleDenseMatrix Identity(int count)
        {
            DoubleDenseMatrix m = new DoubleDenseMatrix(count, count);
            for (int i = 0; i < count; ++i)
                m[i, i] = 1;
            return m;
        }
        #endregion

        #region similarity
        public static DoubleDenseMatrix Similarity(
            DoubleDenseMatrix A, 
            DoubleDenseMatrix B,
            double tolerance
            )
        {
            // <preconditions>
            if (A == (DoubleDenseMatrix)null)
                throw new ArgumentNullException("A");
            if (B == (DoubleDenseMatrix)null)
                throw new ArgumentNullException("B");
            // </preconditions>
            DoubleDenseMatrix AT = A.Transpose();
            DoubleDenseMatrix BT = B.Transpose();
            DoubleDenseMatrix Zk = DoubleDenseMatrix.Create(B.RowCount, A.RowCount, 1.0/(A.RowCount*B.RowCount));
            DoubleDenseMatrix Zk1 = null;
            DoubleDenseMatrix ZkOld = null;

            int iteration = 0;
            do
            {
                Zk1 = B * Zk * AT + BT * Zk * A;
                Zk1.Div(Zk1.GetNorm2());

                ZkOld = Zk;
                Zk = B * Zk1 * AT + BT * Zk1 * A;
                Zk.Div(Zk.GetNorm2());

                Console.WriteLine(iteration);
                Zk.WriteMatrix(Console.Out);
                Console.WriteLine((Zk - ZkOld).GetNorm2());

                if (iteration++ > 100)
                    throw new InvalidOperationException();

            } while ((Zk - ZkOld).GetNorm2() > tolerance);

            return Zk;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return String.Format("({0}x{1})", this.RowCount, this.ColumnCount);
        }

        public void WriteMatrix(TextWriter writer)
        {
            for (int i = 0; i < this.RowCount; ++i)
            {
                for (int j = 0; j < this.ColumnCount; ++j)
                {
                    if (j != 0)
                        writer.Write(" ");
                    writer.Write(this[i, j]);
                }
                writer.WriteLine();
            }
        }

        public void WriteMatrix(TextWriter writer, string formatString)
        {
            for (int i = 0; i < this.RowCount; ++i)
            {
                for (int j = 0; j < this.ColumnCount; ++j)
                {
                    if (j != 0)
                        writer.Write(" ");
                    writer.Write(formatString, this[i, j]);
                }
                writer.WriteLine();
            }
        }
        #endregion
    }
}
