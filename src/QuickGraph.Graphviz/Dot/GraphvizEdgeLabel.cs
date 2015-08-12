namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class GraphvizEdgeLabel
    {
        private double angle = -25;
        private double distance = 1;
        private bool @float = true;
        private GraphvizFont font = null;
        private GraphvizColor fontColor = GraphvizColor.Black;
        private string value = null;

        public void AddParameters(IDictionary<string, object> dic)
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

        public GraphvizFont Font
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

        public GraphvizColor FontColor
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

