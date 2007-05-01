namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;

    public class GraphvizEdge
    {
        private string comment = null;
        private GraphvizEdgeDirection dir = GraphvizEdgeDirection.Forward;
        private System.Drawing.Font font = null;
        private Color fontColor = Color.Black;
        private GraphvizEdgeExtremity head = new GraphvizEdgeExtremity(true);
        private GraphvizArrow headArrow = null;
        private bool isConstrained = true;
        private bool isDecorated = false;
        private GraphvizEdgeLabel label = new GraphvizEdgeLabel();
        private GraphvizLayer layer = null;
        private int minLength = 1;
        private Color strokeColor = Color.Black;
        private GraphvizEdgeStyle style = GraphvizEdgeStyle.Unspecified;
        private GraphvizEdgeExtremity tail = new GraphvizEdgeExtremity(false);
        private GraphvizArrow tailArrow = null;
        private string tooltip = null;
        private string url = null;
        private double weight = 1;

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
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        public GraphvizEdgeDirection Dir
        {
            get
            {
                return this.dir;
            }
            set
            {
                this.dir = value;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return this.fontColor;
            }
            set
            {
                this.fontColor = value;
            }
        }

        public GraphvizEdgeExtremity Head
        {
            get
            {
                return this.head;
            }
            set
            {
                this.head = value;
            }
        }

        public GraphvizArrow HeadArrow
        {
            get
            {
                return this.headArrow;
            }
            set
            {
                this.headArrow = value;
            }
        }

        public bool IsConstrained
        {
            get
            {
                return this.isConstrained;
            }
            set
            {
                this.isConstrained = value;
            }
        }

        public bool IsDecorated
        {
            get
            {
                return this.isDecorated;
            }
            set
            {
                this.isDecorated = value;
            }
        }

        public GraphvizEdgeLabel Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
            }
        }

        public GraphvizLayer Layer
        {
            get
            {
                return this.layer;
            }
            set
            {
                this.layer = value;
            }
        }

        public int MinLength
        {
            get
            {
                return this.minLength;
            }
            set
            {
                this.minLength = value;
            }
        }

        public Color StrokeColor
        {
            get
            {
                return this.strokeColor;
            }
            set
            {
                this.strokeColor = value;
            }
        }

        public GraphvizEdgeStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        public GraphvizEdgeExtremity Tail
        {
            get
            {
                return this.tail;
            }
            set
            {
                this.tail = value;
            }
        }

        public GraphvizArrow TailArrow
        {
            get
            {
                return this.tailArrow;
            }
            set
            {
                this.tailArrow = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }
    }
}

