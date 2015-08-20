namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class GraphvizEdgeLabel
    {
        private double angle = -25;
        private double distance = 1;
        private bool @float = true;
        private System.Drawing.Font font = null;
        private Color fontColor = Color.Black;
        private string value = null;

        public void AddParameters(IDictionary dic)
        {
            if (this.Value != null)
            {
                dic["label"] = this.Value;
                if (this.Angle != -25)
                {
                    dic["labelangle"] = this.Angle;
                }
                if (this.Distance != 1)
                {
                    dic["labeldistance"] = this.Distance;
                }
                if (!this.Float)
                {
                    dic["labelfloat"] = this.Float;
                }
                if (this.Font != null)
                {
                    dic["labelfontname"] = this.Font.Name;
                    dic["labelfontsize"] = this.Font.SizeInPoints;
                }
            }
        }

        public double Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle = value;
            }
        }

        public double Distance
        {
            get
            {
                return this.distance;
            }
            set
            {
                this.distance = value;
            }
        }

        public bool Float
        {
            get
            {
                return this.@float;
            }
            set
            {
                this.@float = value;
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

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}

