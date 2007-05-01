namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Text;

    public class GraphvizRecordCell
    {
        private GraphvizRecordCellCollection m_Cells = new GraphvizRecordCellCollection();
        private GraphvizRecordEscaper m_Escaper = new GraphvizRecordEscaper();
        private string m_Port = null;
        private string m_Text = null;

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
                return this.m_Cells;
            }
        }

        protected GraphvizRecordEscaper Escaper
        {
            get
            {
                return this.m_Escaper;
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
                if (this.m_Text != null)
                {
                    return (this.m_Text.Length > 0);
                }
                return false;
            }
        }

        public string Port
        {
            get
            {
                return this.m_Port;
            }
            set
            {
                this.m_Port = value;
            }
        }

        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
            }
        }
    }
}

