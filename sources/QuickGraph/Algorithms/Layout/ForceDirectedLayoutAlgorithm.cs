using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuickGraph.Algorithms.Layout
{
    public sealed class ForceDirectedLayoutAlgorithm<Vertex, Edge, Graph> : 
        LayoutAlgorithmBase<Vertex,Edge,Graph>
        where Edge : IEdge<Vertex>
        where Graph : IVertexAndEdgeListGraph<Vertex, Edge>
    {
        private float springFactor = 0.2F;
        private PointF magneticDirection = new PointF(1, 0);
        private float gravitationFactor = 0.1F;

        public ForceDirectedLayoutAlgorithm(Graph visitedGraph)
            :base(visitedGraph)
        {}

        public float SpringFactor
        {
            get { return this.springFactor; }
            set { this.springFactor = value; }
        }

        public PointF MagneticDirection
        {
            get { return this.magneticDirection; }
            set { this.magneticDirection = value; }
        }

        public float GravitationFactor
        {
            get { return this.gravitationFactor; }
            set { this.gravitationFactor = value; }
        }

        protected override void InternalCompute()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
