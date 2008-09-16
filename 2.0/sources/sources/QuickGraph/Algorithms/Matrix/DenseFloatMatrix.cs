using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms.Matrix
{
    public sealed class DenseFloatMatrix
    {
        private readonly int rowCount;
        private readonly int columnCount;
        private float[] data;

        public DenseFloatMatrix(int rowCount, int columnCount)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.data = new float[this.rowCount * this.columnCount];
        }

        public int RowCount
        {
            get { return this.rowCount; }
        }

        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        public float this[int i, int j]
        {
            get { return this.data[i * this.columnCount + j]; }
            set { this.data[i * this.columnCount + j] = value; }
        }
    }
}
