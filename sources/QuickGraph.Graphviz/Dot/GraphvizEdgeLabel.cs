namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class GraphvizEdgeLabel
    {
        private double m_Angle = -25;
        private double m_Distance = 1;
        private bool m_Float = true;
        private System.Drawing.Font m_Font = null;
        private Color m_FontColor = Color.Black;
        private string m_Value = null;

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
                return this.m_Angle;
            }
            set
            {
                this.m_Angle = value;
            }
        }

        public double Distance
        {
            get
            {
                return this.m_Distance;
            }
            set
            {
                this.m_Distance = value;
            }
        }

        public bool Float
        {
            get
            {
                return this.m_Float;
            }
            set
            {
                this.m_Float = value;
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

        public string Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }
    }
}

