using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Algorithms;

namespace QuickGraph.Glee
{
    public abstract class GleeGraphPopulator<Vertex,Edge> : 
        AlgorithmBase<IVertexAndEdgeListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        protected GleeGraphPopulator(IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph)
            : base(visitedGraph)
        { }

        private Microsoft.Glee.Drawing.Graph gleeGraph;
        public Microsoft.Glee.Drawing.Graph GleeGraph
        {
            get { return this.gleeGraph; }
        }

        protected override void InternalCompute()
        {
            this.gleeGraph = new Microsoft.Glee.Drawing.Graph("");

            if (typeof(IIdentifiable).IsAssignableFrom(typeof(Vertex)))
            {
                foreach (IIdentifiable v in this.VisitedGraph.Vertices)
                {
                    Microsoft.Glee.Drawing.Node node = (Microsoft.Glee.Drawing.Node)this.GleeGraph.AddNode(v.ID);

                }
            }
        }
    }
}
