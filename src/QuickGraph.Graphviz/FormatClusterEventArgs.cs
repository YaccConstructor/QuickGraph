using System;
using QuickGraph;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;


namespace QuickGraph.Graphviz
{
    /// <summary>
    /// A clustered graph event argument.
    /// </summary>
    public class FormatClusterEventArgs<TVertex, TEdge> : EventArgs where TEdge : IEdge<TVertex>
    {
        private IVertexAndEdgeListGraph<TVertex,TEdge> cluster;
        private GraphvizGraph graphFormat;

        public FormatClusterEventArgs(IVertexAndEdgeListGraph<TVertex,TEdge> cluster, GraphvizGraph graphFormat)
        {
            if (cluster == null)
                throw new ArgumentNullException("cluster");
            this.cluster = cluster;
            this.graphFormat = graphFormat;
        }

        public IVertexAndEdgeListGraph<TVertex,TEdge> Cluster
        {
            get
            {
                return cluster;
            }
        }

        public GraphvizGraph GraphFormat
        {
            get
            {
                return graphFormat;
            }
        }
    }

    public delegate void FormatClusterEventHandler<TVertex, TEdge>(
        Object sender,
        FormatClusterEventArgs<TVertex,TEdge> e)
        where TEdge: IEdge<TVertex>;

}
