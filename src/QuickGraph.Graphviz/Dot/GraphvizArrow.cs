namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.IO;

    public class GraphvizArrow
    {
        private GraphvizArrowClipping clipping;
        private GraphvizArrowFilling filling;
        private GraphvizArrowShape shape;

        public GraphvizArrow(GraphvizArrowShape shape)
        {
            this.shape = shape;
            this.clipping = GraphvizArrowClipping.None;
            this.filling = GraphvizArrowFilling.Close;
        }

        public GraphvizArrow(GraphvizArrowShape shape, GraphvizArrowClipping clip, GraphvizArrowFilling fill)
        {
            this.shape = shape;
            this.clipping = clip;
            this.filling = fill;
        }

        public string ToDot()
        {
            using (StringWriter writer = new StringWriter())
            {
                if (this.filling == GraphvizArrowFilling.Open)
                {
                    writer.Write('o');
                }
                switch (this.clipping)
                {
                    case GraphvizArrowClipping.Left:
                        writer.Write('l');
                        break;

                    case GraphvizArrowClipping.Right:
                        writer.Write('r');
                        break;
                }
                writer.Write(this.shape.ToString().ToLower());
                return writer.ToString();
            }
        }

        public override string ToString()
        {
            return this.ToDot();
        }

        public GraphvizArrowClipping Clipping
        {
            get
            {
                return this.clipping;
            }
            set
            {
                this.clipping = value;
            }
        }

        public GraphvizArrowFilling Filling
        {
            get
            {
                return this.filling;
            }
            set
            {
                this.filling = value;
            }
        }

        public GraphvizArrowShape Shape
        {
            get
            {
                return this.shape;
            }
            set
            {
                this.shape = value;
            }
        }
    }
}

