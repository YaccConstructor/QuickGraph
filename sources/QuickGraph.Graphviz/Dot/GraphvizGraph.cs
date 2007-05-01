namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;

    public class GraphvizGraph
    {
        private Color backgroundColor = Color.White;
        private GraphvizClusterMode clusterRank = GraphvizClusterMode.Local;
        private string comment = null;
        private System.Drawing.Font font = null;
        private Color fontColor = Color.Black;
        private bool isCentered = false;
        private bool isCompounded = false;
        private bool isConcentrated = false;
        private bool isLandscape = false;
        private bool isNormalized = false;
        private bool isReMinCross = false;
        private string label = null;
        private GraphvizLabelJustification labelJustification = GraphvizLabelJustification.C;
        private GraphvizLabelLocation labelLocation = GraphvizLabelLocation.B;
        private readonly GraphvizLayerCollection layers = new GraphvizLayerCollection();
        private double mcLimit = 1;
        private double nodeSeparation = 0.25;
        private int nsLimit = -1;
        private int nsLimit1 = -1;
        private GraphvizOutputMode outputOrder = GraphvizOutputMode.BreadthFirst;
        private GraphvizPageDirection pageDirection = GraphvizPageDirection.BL;
        private System.Drawing.Size pageSize = new System.Drawing.Size(0, 0);
        private double quantum = 0;
        private GraphvizRankDirection rankDirection = GraphvizRankDirection.TB;
        private double rankSeparation = 0.5;
        private GraphvizRatioMode ratio = GraphvizRatioMode.Auto;
        private double resolution = 0.96;
        private int rotate = 0;
        private int samplePoints = 8;
        private int searchSize = 30;
        private System.Drawing.Size size = new System.Drawing.Size(0, 0);
        private string styleSheet = null;
        private string url = null;

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
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }

        public GraphvizClusterMode ClusterRank
        {
            get
            {
                return this.clusterRank;
            }
            set
            {
                this.clusterRank = value;
            }
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

        public bool IsCentered
        {
            get
            {
                return this.isCentered;
            }
            set
            {
                this.isCentered = value;
            }
        }

        public bool IsCompounded
        {
            get
            {
                return this.isCompounded;
            }
            set
            {
                this.isCompounded = value;
            }
        }

        public bool IsConcentrated
        {
            get
            {
                return this.isConcentrated;
            }
            set
            {
                this.isConcentrated = value;
            }
        }

        public bool IsLandscape
        {
            get
            {
                return this.isLandscape;
            }
            set
            {
                this.isLandscape = value;
            }
        }

        public bool IsNormalized
        {
            get
            {
                return this.isNormalized;
            }
            set
            {
                this.isNormalized = value;
            }
        }

        public bool IsReMinCross
        {
            get
            {
                return this.isReMinCross;
            }
            set
            {
                this.isReMinCross = value;
            }
        }

        public string Label
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

        public GraphvizLabelJustification LabelJustification
        {
            get
            {
                return this.labelJustification;
            }
            set
            {
                this.labelJustification = value;
            }
        }

        public GraphvizLabelLocation LabelLocation
        {
            get
            {
                return this.labelLocation;
            }
            set
            {
                this.labelLocation = value;
            }
        }

        public GraphvizLayerCollection Layers
        {
            get
            {
                return this.layers;
            }
        }

        public double McLimit
        {
            get
            {
                return this.mcLimit;
            }
            set
            {
                this.mcLimit = value;
            }
        }

        public double NodeSeparation
        {
            get
            {
                return this.nodeSeparation;
            }
            set
            {
                this.nodeSeparation = value;
            }
        }

        public int NsLimit
        {
            get
            {
                return this.nsLimit;
            }
            set
            {
                this.nsLimit = value;
            }
        }

        public int NsLimit1
        {
            get
            {
                return this.nsLimit1;
            }
            set
            {
                this.nsLimit1 = value;
            }
        }

        public GraphvizOutputMode OutputOrder
        {
            get
            {
                return this.outputOrder;
            }
            set
            {
                this.outputOrder = value;
            }
        }

        public GraphvizPageDirection PageDirection
        {
            get
            {
                return this.pageDirection;
            }
            set
            {
                this.pageDirection = value;
            }
        }

        public System.Drawing.Size PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        public double Quantum
        {
            get
            {
                return this.quantum;
            }
            set
            {
                this.quantum = value;
            }
        }

        public GraphvizRankDirection RankDirection
        {
            get
            {
                return this.rankDirection;
            }
            set
            {
                this.rankDirection = value;
            }
        }

        public double RankSeparation
        {
            get
            {
                return this.rankSeparation;
            }
            set
            {
                this.rankSeparation = value;
            }
        }

        public GraphvizRatioMode Ratio
        {
            get
            {
                return this.ratio;
            }
            set
            {
                this.ratio = value;
            }
        }

        public double Resolution
        {
            get
            {
                return this.resolution;
            }
            set
            {
                this.resolution = value;
            }
        }

        public int Rotate
        {
            get
            {
                return this.rotate;
            }
            set
            {
                this.rotate = value;
            }
        }

        public int SamplePoints
        {
            get
            {
                return this.samplePoints;
            }
            set
            {
                this.samplePoints = value;
            }
        }

        public int SearchSize
        {
            get
            {
                return this.searchSize;
            }
            set
            {
                this.searchSize = value;
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public string StyleSheet
        {
            get
            {
                return this.styleSheet;
            }
            set
            {
                this.styleSheet = value;
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
    }
}

