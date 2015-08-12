namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Text;

    public class GraphvizRecord
    {
        private readonly GraphvizRecordCellCollection cells = new GraphvizRecordCellCollection();

        public string ToDot()
        {
            if (this.Cells.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            foreach (GraphvizRecordCell cell in this.Cells)
            {
                if (flag)
                {
                    builder.AppendFormat(" | {0}", cell.ToDot());
                    continue;
                }
                builder.Append(cell.ToDot());
                flag = true;
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToDot();
        }

        public GraphvizRecordCellCollection Cells
        {
            get
            {
                return this.cells;
            }
        }
    }
}

