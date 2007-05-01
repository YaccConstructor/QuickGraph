namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;

    public class GraphvizEdge
    {
        private string m_Comment = null;
        private GraphvizEdgeDirection m_Dir = GraphvizEdgeDirection.Forward;
        private System.Drawing.Font m_Font = null;
        private Color m_FontColor = Color.Black;
        private GraphvizEdgeExtremity m_Head = new GraphvizEdgeExtremity(true);
        private GraphvizArrow m_HeadArrow = null;
        private bool m_IsConstrained = true;
        private bool m_IsDecorated = false;
        private GraphvizEdgeLabel m_Label = new GraphvizEdgeLabel();
        private GraphvizLayer m_Layer = null;
        private int m_MinLength = 1;
        private Color m_StrokeColor = Color.Black;
        private GraphvizEdgeStyle m_Style = GraphvizEdgeStyle.Unspecified;
        private GraphvizEdgeExtremity m_Tail = new GraphvizEdgeExtremity(false);
        private GraphvizArrow m_TailArrow = null;
        private string m_ToolTip = null;
        private string m_Url = null;
        private double m_Weight = 1;

        internal string GenerateDot(Hashtable pairs)
        {
            bool flag = false;
            StringWriter writer = new StringWriter();
            foreach (DictionaryEntry entry in pairs)
            {
                if (flag)
                {
                    writer.Write(", ");
                }
                else
                {
                    flag = true;
                }
                if (entry.Value is string)
                {
                    writer.Write("{0}=\"{1}\"", entry.Key.ToString(), entry.Value.ToString());
                    continue;
                }
                if (entry.Value is GraphvizEdgeDirection)
                {
                    writer.Write("{0}={1}", entry.Key.ToString(), ((GraphvizEdgeDirection) entry.Value).ToString().ToLower());
                    continue;
                }
                if (entry.Value is GraphvizEdgeStyle)
                {
                    writer.Write("{0}={1}", entry.Key.ToString(), ((GraphvizEdgeStyle) entry.Value).ToString().ToLower());
                    continue;
                }
                if (entry.Value is Color)
                {
                    Color color = (Color) entry.Value;
                    writer.Write("{0}=\"#{1}{2}{3}{4}\"", new object[] { entry.Key.ToString(), color.R.ToString("x2").ToUpper(), color.G.ToString("x2").ToUpper(), color.B.ToString("x2").ToUpper(), color.A.ToString("x2").ToUpper() });
                    continue;
                }
                writer.Write(" {0}={1}", entry.Key.ToString(), entry.Value.ToString().ToLower());
            }
            return writer.ToString();
        }

        public string ToDot()
        {
            Hashtable dic = new Hashtable();
            if (this.Comment != null)
            {
                dic["comment"] = this.Comment;
            }
            if (this.Dir != GraphvizEdgeDirection.Forward)
            {
                dic["dir"] = this.Dir.ToString().ToLower();
            }
            if (this.Font != null)
            {
                dic["fontname"] = this.Font.Name;
                dic["fontsize"] = this.Font.SizeInPoints;
            }
            if (this.FontColor != Color.Black)
            {
                dic["fontcolor"] = this.FontColor;
            }
            this.Head.AddParameters(dic);
            if (this.HeadArrow != null)
            {
                dic["arrowhead"] = this.HeadArrow.ToDot();
            }
            if (!this.IsConstrained)
            {
                dic["constraint"] = this.IsConstrained;
            }
            if (this.IsDecorated)
            {
                dic["decorate"] = this.IsDecorated;
            }
            this.Label.AddParameters(dic);
            if (this.Layer != null)
            {
                dic["layer"] = this.Layer.Name;
            }
            if (this.MinLength != 1)
            {
                dic["minlen"] = this.MinLength;
            }
            if (this.StrokeColor != Color.Black)
            {
                dic["color"] = this.StrokeColor;
            }
            if (this.Style != GraphvizEdgeStyle.Unspecified)
            {
                dic["style"] = this.Style.ToString().ToLower();
            }
            this.Tail.AddParameters(dic);
            if (this.TailArrow != null)
            {
                dic["arrowtail"] = this.TailArrow.ToDot();
            }
            if (this.ToolTip != null)
            {
                dic["tooltip"] = this.ToolTip;
            }
            if (this.Url != null)
            {
                dic["URL"] = this.Url;
            }
            if (this.Weight != 1)
            {
                dic["weight"] = this.Weight;
            }
            return this.GenerateDot(dic);
        }

        public override string ToString()
        {
            return this.ToDot();
        }

        public string Comment
        {
            get
            {
                return this.m_Comment;
            }
            set
            {
                this.m_Comment = value;
            }
        }

        public GraphvizEdgeDirection Dir
        {
            get
            {
                return this.m_Dir;
            }
            set
            {
                this.m_Dir = value;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.m_Font;
            }
            set
            {
                this.m_Font = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return this.m_FontColor;
            }
            set
            {
                this.m_FontColor = value;
            }
        }

        public GraphvizEdgeExtremity Head
        {
            get
            {
                return this.m_Head;
            }
            set
            {
                this.m_Head = value;
            }
        }

        public GraphvizArrow HeadArrow
        {
            get
            {
                return this.m_HeadArrow;
            }
            set
            {
                this.m_HeadArrow = value;
            }
        }

        public bool IsConstrained
        {
            get
            {
                return this.m_IsConstrained;
            }
            set
            {
                this.m_IsConstrained = value;
            }
        }

        public bool IsDecorated
        {
            get
            {
                return this.m_IsDecorated;
            }
            set
            {
                this.m_IsDecorated = value;
            }
        }

        public GraphvizEdgeLabel Label
        {
            get
            {
                return this.m_Label;
            }
            set
            {
                this.m_Label = value;
            }
        }

        public GraphvizLayer Layer
        {
            get
            {
                return this.m_Layer;
            }
            set
            {
                this.m_Layer = value;
            }
        }

        public int MinLength
        {
            get
            {
                return this.m_MinLength;
            }
            set
            {
                this.m_MinLength = value;
            }
        }

        public Color StrokeColor
        {
            get
            {
                return this.m_StrokeColor;
            }
            set
            {
                this.m_StrokeColor = value;
            }
        }

        public GraphvizEdgeStyle Style
        {
            get
            {
                return this.m_Style;
            }
            set
            {
                this.m_Style = value;
            }
        }

        public GraphvizEdgeExtremity Tail
        {
            get
            {
                return this.m_Tail;
            }
            set
            {
                this.m_Tail = value;
            }
        }

        public GraphvizArrow TailArrow
        {
            get
            {
                return this.m_TailArrow;
            }
            set
            {
                this.m_TailArrow = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.m_ToolTip;
            }
            set
            {
                this.m_ToolTip = value;
            }
        }

        public string Url
        {
            get
            {
                return this.m_Url;
            }
            set
            {
                this.m_Url = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.m_Weight;
            }
            set
            {
                this.m_Weight = value;
            }
        }
    }
}

