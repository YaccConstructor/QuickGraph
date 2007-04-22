using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QuickGraph.Algorithms.Layout
{
    public class GdiGraphLayoutRenderer<Vertex,Edge,Graph,LayoutAlgorithm> : 
        GraphLayoutRendererBase<Vertex,Edge,Graph,LayoutAlgorithm>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
        where LayoutAlgorithm : LayoutAlgorithmBase<Vertex, Edge, Graph>
    {
        private Graphics canvas;
        private Color backgroundColor = Color.White;
        private Brush backgroundBrush = null;

        private Color edgeColor = Color.Black;
        private float edgeWidth = 0.5F;
        private Pen edgePen = null;

        private Font edgeLabelFont = new Font("Tahoma", 8.25F);
        private Color edgeLabelColor = Color.Black;
        private Brush edgeLabelBrush = null;

        private Color vertexColor = Color.Black;
        private float vertexRadius = 3;
        private Brush vertexBrush = null;

        private Font vertexLabelFont = new Font("Tahoma", 8.25F);
        private Color vertexLabelColor = Color.Black;
        private Brush vertexLabelBrush = null;

        public GdiGraphLayoutRenderer(LayoutAlgorithm algorithm, Graphics canvas)
            : base(algorithm)
        {
            this.canvas = canvas;
        }

        public Graphics Canvas
        {
            get { return this.canvas; }
        }

        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set
            {
                this.backgroundColor = value;
                this.backgroundBrush = null;
            }
        }

        public Brush BackgroundBrush
        {
            get
            {
                if (this.backgroundBrush == null)
                    this.backgroundBrush = new SolidBrush(this.backgroundColor);
                return this.backgroundBrush;
            }
        }

        public Pen EdgePen
        {
            get
            {
                if (this.edgePen == null)
                {
                    this.edgePen = new Pen(this.edgeColor, this.edgeWidth);
                    this.edgePen.EndCap |= LineCap.ArrowAnchor;
                }
                return this.edgePen;
            }
        }


        public Brush VertexBrush
        {
            get
            {
                if (this.vertexBrush == null)
                    this.vertexBrush = new SolidBrush(this.vertexColor);
                return this.vertexBrush;
            }
        }

        public Brush VertexLabelBrush
        {
            get
            {
                if (this.vertexLabelBrush == null)
                    this.vertexLabelBrush = new SolidBrush(this.vertexLabelColor);
                return this.vertexLabelBrush;
            }
        }

        public Brush EdgeLabelBrush
        {
            get
            {
                if (this.edgeLabelBrush == null)
                    this.edgeLabelBrush = new SolidBrush(this.edgeLabelColor);
                return this.edgeLabelBrush;
            }
        }

        protected override void PreRender()
        {
            this.Canvas.FillRectangle(
                this.BackgroundBrush,
                this.Canvas.ClipBounds
                );
        }

        protected override void PostRender()
        {
        }

        protected override void DrawVertex(Vertex vertex, PointF position)
        {
            this.Canvas.FillEllipse(
                this.VertexBrush,
                position.X - this.vertexRadius,
                position.Y - this.vertexRadius,
                2 * this.vertexRadius,
                2 * this.vertexRadius);

            this.Canvas.DrawString(
                vertex.ToString(),
                this.vertexLabelFont,
                this.VertexLabelBrush,
                position
                );
        }

        protected override void DrawEdge(
            Edge edge,
            PointF sourcePosition,
            PointF targetPosition
            )
        {
            this.Canvas.DrawLine(
                this.EdgePen,
                sourcePosition,
                targetPosition);
        }
    }
}
