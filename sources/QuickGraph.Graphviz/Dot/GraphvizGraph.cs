namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;

    public class GraphvizGraph
    {
        private Color m_BackgroundColor = Color.White;
        private GraphvizClusterMode m_ClusterRank = GraphvizClusterMode.Local;
        private string m_Comment = null;
        private System.Drawing.Font m_Font = null;
        private Color m_FontColor = Color.Black;
        private bool m_IsCentered = false;
        private bool m_IsCompounded = false;
        private bool m_IsConcentrated = false;
        private bool m_IsLandscape = false;
        private bool m_IsNormalized = false;
        private bool m_IsReMinCross = false;
        private string m_Label = null;
        private GraphvizLabelJustification m_LabelJustification = GraphvizLabelJustification.C;
        private GraphvizLabelLocation m_LabelLocation = GraphvizLabelLocation.B;
        private GraphvizLayerCollection m_Layers = new GraphvizLayerCollection();
        private double m_McLimit = 1;
        private double m_NodeSeparation = 0.25;
        private int m_NsLimit = -1;
        private int m_NsLimit1 = -1;
        private GraphvizOutputMode m_OutputOrder = GraphvizOutputMode.BreadthFirst;
        private GraphvizPageDirection m_PageDirection = GraphvizPageDirection.BL;
        private System.Drawing.Size m_PageSize = new System.Drawing.Size(0, 0);
        private double m_Quantum = 0;
        private GraphvizRankDirection m_RankDirection = GraphvizRankDirection.TB;
        private double m_RankSeparation = 0.5;
        private GraphvizRatioMode m_Ratio = GraphvizRatioMode.Auto;
        private double m_Resolution = 0.96;
        private int m_Rotate = 0;
        private int m_SamplePoints = 8;
        private int m_SearchSize = 30;
        private System.Drawing.Size m_Size = new System.Drawing.Size(0, 0);
        private string m_StyleSheet = null;
        private string m_Url = null;

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
                if (entry.Value is Color)
                {
                    Color color = (Color) entry.Value;
                    writer.Write("{0}=\"#{1}{2}{3}{4}\"", new object[] { entry.Key.ToString(), color.R.ToString("x2").ToUpper(), color.G.ToString("x2").ToUpper(), color.B.ToString("x2").ToUpper(), color.A.ToString("x2").ToUpper() });
                    continue;
                }
                if ((entry.Value is GraphvizRankDirection) || (entry.Value is GraphvizPageDirection))
                {
                    writer.Write("{0}={1};", entry.Key.ToString(), entry.Value.ToString());
                    continue;
                }
                writer.Write(" {0}={1}", entry.Key.ToString(), entry.Value.ToString().ToLower());
            }
            return writer.ToString();
        }

        public string ToDot()
        {
            Hashtable pairs = new Hashtable();
            if (this.Url != null)
            {
                pairs["URL"] = this.Url;
            }
            if (this.BackgroundColor != Color.White)
            {
                pairs["bgcolor"] = this.BackgroundColor;
            }
            if (this.IsCentered)
            {
                pairs["center"] = true;
            }
            if (this.ClusterRank != GraphvizClusterMode.Local)
            {
                pairs["clusterrank"] = this.ClusterRank.ToString().ToLower();
            }
            if (this.Comment != null)
            {
                pairs["comment"] = this.Comment;
            }
            if (this.IsCompounded)
            {
                pairs["compound"] = this.IsCompounded;
            }
            if (this.IsConcentrated)
            {
                pairs["concentrated"] = this.IsConcentrated;
            }
            if (this.Font != null)
            {
                pairs["fontname"] = this.Font.Name;
                pairs["fontsize"] = this.Font.SizeInPoints;
            }
            if (this.FontColor != Color.Black)
            {
                pairs["fontcolor"] = this.FontColor;
            }
            if (this.Label != null)
            {
                pairs["label"] = this.Label;
            }
            if (this.LabelJustification != GraphvizLabelJustification.C)
            {
                pairs["labeljust"] = this.LabelJustification.ToString().ToLower();
            }
            if (this.LabelLocation != GraphvizLabelLocation.B)
            {
                pairs["labelloc"] = this.LabelLocation.ToString().ToLower();
            }
            if (this.Layers.Count != 0)
            {
                pairs["layers"] = this.Layers.ToDot();
            }
            if (this.McLimit != 1)
            {
                pairs["mclimit"] = this.McLimit;
            }
            if (this.NodeSeparation != 0.25)
            {
                pairs["nodesep"] = this.NodeSeparation;
            }
            if (this.IsNormalized)
            {
                pairs["normalize"] = this.IsNormalized;
            }
            if (this.NsLimit > 0)
            {
                pairs["nslimit"] = this.NsLimit;
            }
            if (this.NsLimit1 > 0)
            {
                pairs["nslimit1"] = this.NsLimit1;
            }
            if (this.OutputOrder != GraphvizOutputMode.BreadthFirst)
            {
                pairs["outputorder"] = this.OutputOrder.ToString().ToLower();
            }
            if (!this.PageSize.IsEmpty)
            {
                pairs["page"] = string.Format("({0},{1})", this.PageSize.Width, this.PageSize.Height);
            }
            if (this.PageDirection != GraphvizPageDirection.BL)
            {
                pairs["pagedir"] = this.PageDirection.ToString().ToLower();
            }
            if (this.Quantum > 0)
            {
                pairs["quantum"] = this.Quantum;
            }
            if (this.RankSeparation != 0.5)
            {
                pairs["ranksep"] = this.RankSeparation;
            }
            if (this.Ratio != GraphvizRatioMode.Auto)
            {
                pairs["ratio"] = this.Ratio.ToString().ToLower();
            }
            if (this.IsReMinCross)
            {
                pairs["remincross"] = this.IsReMinCross;
            }
            if (this.Resolution != 0.96)
            {
                pairs["resolution"] = this.Resolution;
            }
            if (this.Rotate != 0)
            {
                pairs["rotate"] = this.Rotate;
            }
            else if (this.IsLandscape)
            {
                pairs["orientation"] = "[1L]*";
            }
            if (this.SamplePoints != 8)
            {
                pairs["samplepoints"] = this.SamplePoints;
            }
            if (this.SearchSize != 30)
            {
                pairs["searchsize"] = this.SearchSize;
            }
            if (!this.Size.IsEmpty)
            {
                pairs["size"] = string.Format("({0},{1})", this.Size.Width, this.Size.Height);
            }
            if (this.StyleSheet != null)
            {
                pairs["stylesheet"] = this.StyleSheet;
            }
            if (this.RankDirection != GraphvizRankDirection.TB)
            {
                pairs["rankdir"] = this.RankDirection;
            }
            return this.GenerateDot(pairs);
        }

        public override string ToString()
        {
            return this.ToDot();
        }

        public Color BackgroundColor
        {
            get
            {
                return this.m_BackgroundColor;
            }
            set
            {
                this.m_BackgroundColor = value;
            }
        }

        public GraphvizClusterMode ClusterRank
        {
            get
            {
                return this.m_ClusterRank;
            }
            set
            {
                this.m_ClusterRank = value;
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

        public bool IsCentered
        {
            get
            {
                return this.m_IsCentered;
            }
            set
            {
                this.m_IsCentered = value;
            }
        }

        public bool IsCompounded
        {
            get
            {
                return this.m_IsCompounded;
            }
            set
            {
                this.m_IsCompounded = value;
            }
        }

        public bool IsConcentrated
        {
            get
            {
                return this.m_IsConcentrated;
            }
            set
            {
                this.m_IsConcentrated = value;
            }
        }

        public bool IsLandscape
        {
            get
            {
                return this.m_IsLandscape;
            }
            set
            {
                this.m_IsLandscape = value;
            }
        }

        public bool IsNormalized
        {
            get
            {
                return this.m_IsNormalized;
            }
            set
            {
                this.m_IsNormalized = value;
            }
        }

        public bool IsReMinCross
        {
            get
            {
                return this.m_IsReMinCross;
            }
            set
            {
                this.m_IsReMinCross = value;
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

        public GraphvizLabelJustification LabelJustification
        {
            get
            {
                return this.m_LabelJustification;
            }
            set
            {
                this.m_LabelJustification = value;
            }
        }

        public GraphvizLabelLocation LabelLocation
        {
            get
            {
                return this.m_LabelLocation;
            }
            set
            {
                this.m_LabelLocation = value;
            }
        }

        public GraphvizLayerCollection Layers
        {
            get
            {
                return this.m_Layers;
            }
            set
            {
                this.m_Layers = value;
            }
        }

        public double McLimit
        {
            get
            {
                return this.m_McLimit;
            }
            set
            {
                this.m_McLimit = value;
            }
        }

        public double NodeSeparation
        {
            get
            {
                return this.m_NodeSeparation;
            }
            set
            {
                this.m_NodeSeparation = value;
            }
        }

        public int NsLimit
        {
            get
            {
                return this.m_NsLimit;
            }
            set
            {
                this.m_NsLimit = value;
            }
        }

        public int NsLimit1
        {
            get
            {
                return this.m_NsLimit1;
            }
            set
            {
                this.m_NsLimit1 = value;
            }
        }

        public GraphvizOutputMode OutputOrder
        {
            get
            {
                return this.m_OutputOrder;
            }
            set
            {
                this.m_OutputOrder = value;
            }
        }

        public GraphvizPageDirection PageDirection
        {
            get
            {
                return this.m_PageDirection;
            }
            set
            {
                this.m_PageDirection = value;
            }
        }

        public System.Drawing.Size PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }

        public double Quantum
        {
            get
            {
                return this.m_Quantum;
            }
            set
            {
                this.m_Quantum = value;
            }
        }

        public GraphvizRankDirection RankDirection
        {
            get
            {
                return this.m_RankDirection;
            }
            set
            {
                this.m_RankDirection = value;
            }
        }

        public double RankSeparation
        {
            get
            {
                return this.m_RankSeparation;
            }
            set
            {
                this.m_RankSeparation = value;
            }
        }

        public GraphvizRatioMode Ratio
        {
            get
            {
                return this.m_Ratio;
            }
            set
            {
                this.m_Ratio = value;
            }
        }

        public double Resolution
        {
            get
            {
                return this.m_Resolution;
            }
            set
            {
                this.m_Resolution = value;
            }
        }

        public int Rotate
        {
            get
            {
                return this.m_Rotate;
            }
            set
            {
                this.m_Rotate = value;
            }
        }

        public int SamplePoints
        {
            get
            {
                return this.m_SamplePoints;
            }
            set
            {
                this.m_SamplePoints = value;
            }
        }

        public int SearchSize
        {
            get
            {
                return this.m_SearchSize;
            }
            set
            {
                this.m_SearchSize = value;
            }
        }

        public System.Drawing.Size Size
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

        public string StyleSheet
        {
            get
            {
                return this.m_StyleSheet;
            }
            set
            {
                this.m_StyleSheet = value;
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
    }
}

