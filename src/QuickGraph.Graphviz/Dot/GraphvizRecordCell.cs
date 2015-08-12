namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Text;

    public class GraphvizRecordCell
    {
        private readonly GraphvizRecordCellCollection cells = new GraphvizRecordCellCollection();
        private GraphvizRecordEscaper escaper = new GraphvizRecordEscaper();
        private string port = null;
        private string text = null;

        public string ToDot()
        {
            StringBuilder builder = new StringBuilder();
            if (this.HasPort)
            {
                builder.AppendFormat("<{0}> ", this.Escaper.Escape(this.Port));
            }
            if (this.HasText)
            {
                builder.AppendFormat("{0}", this.Escaper.Escape(this.Text));
            }
            if (this.Cells.Count > 0)
            {
                builder.Append(" { ");
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
                builder.Append(" } ");
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

        protected GraphvizRecordEscaper Escaper
        {
            get
            {
                return this.escaper;
            }
        }

        public bool HasPort
        {
            get
            {
                if (this.Port != null)
                {
                    return (this.Port.Length > 0);
                }
                return false;
            }
        }

        public bool HasText
        {
            get
            {
                if (this.text != null)
                {
                    return (this.text.Length > 0);
                }
                return false;
            }
        }

        public string Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

