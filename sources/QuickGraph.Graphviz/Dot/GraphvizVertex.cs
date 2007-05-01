namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;

    public class GraphvizVertex
    {
        private string m_BottomLabel = null;
        private string m_Comment = null;
        private double m_Distorsion = 0;
        private Color m_FillColor = Color.White;
        private bool m_FixedSize = false;
        private System.Drawing.Font m_Font = null;
        private Color m_FontColor = Color.Black;
        private string m_Group = null;
        private string m_Label = null;
        private GraphvizLayer m_Layer = null;
        private double m_Orientation = 0;
        private int m_Peripheries = -1;
        private GraphvizRecord m_Record = new GraphvizRecord();
        private bool m_Regular = false;
        private GraphvizVertexShape m_Shape = GraphvizVertexShape.Unspecified;
        private int m_Sides = 4;
        private SizeF m_Size = new SizeF(0f, 0f);
        private double m_Skew = 0;
        private Color m_StrokeColor = Color.Black;
        private GraphvizVertexStyle m_Style = GraphvizVertexStyle.Unspecified;
        private string m_ToolTip = null;
        private string m_TopLabel = null;
        private string m_Url = null;
        private double m_Z = -1;

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
                if (entry.Value is GraphvizVertexShape)
                {
                    writer.Write("{0}={1}", entry.Key.ToString(), ((GraphvizVertexShape) entry.Value).ToString().ToLower());
                    continue;
                }
                if (entry.Value is GraphvizVertexStyle)
                {
                    writer.Write("{0}={1}", entry.Key.ToString(), ((GraphvizVertexStyle) entry.Value).ToString().ToLower());
                    continue;
                }
                if (entry.Value is Color)
                {
                    Color color = (Color) entry.Value;
                    writer.Write("{0}=\"#{1}{2}{3}{4}\"", new object[] { entry.Key.ToString(), color.R.ToString("x2").ToUpper(), color.G.ToString("x2").ToUpper(), color.B.ToString("x2").ToUpper(), color.A.ToString("x2").ToUpper() });
                    continue;
                }
                if (entry.Value is GraphvizRecord)
                {
                    writer.WriteLine("{0}=\"{1}\"", entry.Key.ToString(), ((GraphvizRecord) entry.Value).ToDot());
                    continue;
                }
                writer.Write(" {0}={1}", entry.Key.ToString(), entry.Value.ToString().ToLower());
            }
            return writer.ToString();
        }

        public string ToDot()
        {
            Hashtable pairs = new Hashtable();
            if (this.Font != null)
            {
                pairs["fontname"] = this.Font.Name;
                pairs["fontsize"] = this.Font.SizeInPoints;
            }
            if (this.FontColor != Color.Black)
            {
                pairs["fontcolor"] = this.FontColor;
            }
            if (this.Shape != GraphvizVertexShape.Unspecified)
            {
                pairs["shape"] = this.Shape;
            }
            if (this.Style != GraphvizVertexStyle.Unspecified)
            {
                pairs["style"] = this.Style;
            }
            if (this.Shape == GraphvizVertexShape.Record)
            {
                pairs["label"] = this.Record;
            }
            else if (this.Label != null)
            {
                pairs["label"] = this.Label;
            }
            if (this.FixedSize)
            {
                pairs["fixedsize"] = true;
                if (this.Size.Height > 0f)
                {
                    pairs["height"] = this.Size.Height;
                }
                if (this.Size.Width > 0f)
                {
                    pairs["width"] = this.Size.Width;
                }
            }
            if (this.StrokeColor != Color.Black)
            {
                pairs["color"] = this.StrokeColor;
            }
            if (this.FillColor != Color.White)
            {
                pairs["fillcolor"] = this.FillColor;
            }
            if (this.Regular)
            {
                pairs["regular"] = this.Regular;
            }
            if (this.Url != null)
            {
                pairs["URL"] = this.Url;
            }
            if (this.ToolTip != null)
            {
                pairs["tooltip"] = this.ToolTip;
            }
            if (this.Comment != null)
            {
                pairs["comment"] = this.Comment;
            }
            if (this.Group != null)
            {
                pairs["group"] = this.Group;
            }
            if (this.Layer != null)
            {
                pairs["layer"] = this.Layer.Name;
            }
            if (this.Orientation > 0)
            {
                pairs["orientation"] = this.Orientation;
            }
            if (this.Peripheries >= 0)
            {
                pairs["peripheries"] = this.Peripheries;
            }
            if (this.Z > 0)
            {
                pairs["z"] = this.Z;
            }
            if (((this.Style == GraphvizVertexStyle.Diagonals) || (this.Shape == GraphvizVertexShape.MCircle)) || ((this.Shape == GraphvizVertexShape.MDiamond) || (this.Shape == GraphvizVertexShape.MSquare)))
            {
                if (this.TopLabel != null)
                {
                    pairs["toplabel"] = this.TopLabel;
                }
                if (this.BottomLabel != null)
                {
                    pairs["bottomlable"] = this.BottomLabel;
                }
            }
            if (this.Shape == GraphvizVertexShape.Polygon)
            {
                if (this.Sides != 0)
                {
                    pairs["sides"] = this.Sides;
                }
                if (this.Skew != 0)
                {
                    pairs["skew"] = this.Skew;
                }
                if (this.Distorsion != 0)
                {
                    pairs["distorsion"] = this.Distorsion;
                }
            }
            return this.GenerateDot(pairs);
        }

        public override string ToString()
        {
            return this.ToDot();
        }

        public string BottomLabel
        {
            get
            {
                return this.m_BottomLabel;
            }
            set
            {
                this.m_BottomLabel = value;
            }
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

        public double Distorsion
        {
            get
            {
                return this.m_Distorsion;
            }
            set
            {
                this.m_Distorsion = value;
            }
        }

        public Color FillColor
        {
            get
            {
                return this.m_FillColor;
            }
            set
            {
                this.m_FillColor = value;
            }
        }

        public bool FixedSize
        {
            get
            {
                return this.m_FixedSize;
            }
            set
            {
                this.m_FixedSize = value;
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

        public string Group
        {
            get
            {
                return this.m_Group;
            }
            set
            {
                this.m_Group = value;
            }
        }

        public string Label
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

        public double Orientation
        {
            get
            {
                return this.m_Orientation;
            }
            set
            {
                this.m_Orientation = value;
            }
        }

        public int Peripheries
        {
            get
            {
                return this.m_Peripheries;
            }
            set
            {
                this.m_Peripheries = value;
            }
        }

        public GraphvizRecord Record
        {
            get
            {
                return this.m_Record;
            }
            set
            {
                this.m_Record = value;
            }
        }

        public bool Regular
        {
            get
            {
                return this.m_Regular;
            }
            set
            {
                this.m_Regular = value;
            }
        }

        public GraphvizVertexShape Shape
        {
            get
            {
                return this.m_Shape;
            }
            set
            {
                this.m_Shape = value;
            }
        }

        public int Sides
        {
            get
            {
                return this.m_Sides;
            }
            set
            {
                this.m_Sides = value;
            }
        }

        public SizeF Size
        {
            get
            {
                return this.m_Size;
            }
            set
            {
                this.m_Size = value;
            }
        }

        public double Skew
        {
            get
            {
                return this.m_Skew;
            }
            set
            {
                this.m_Skew = value;
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

        public GraphvizVertexStyle Style
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

        public string TopLabel
        {
            get
            {
                return this.m_TopLabel;
            }
            set
            {
                this.m_TopLabel = value;
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

        public double Z
        {
            get
            {
                return this.m_Z;
            }
            set
            {
                this.m_Z = value;
            }
        }
    }
}

