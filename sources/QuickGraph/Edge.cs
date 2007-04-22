using System;

namespace QuickGraph
{
    [Serializable]
    public class Edge<Vertex> : IEdge<Vertex>
    {
        private Vertex source;
        private Vertex target;

        public Edge(Vertex source, Vertex target)
        {
            this.source = source;
            this.target = target;
        }

        public static Type VertexType
        {
            get { return typeof(Vertex); }
        }

        public Vertex Source
        {
            get { return this.source; }
        }

        public Vertex Target
        {
            get { return this.target; }
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", this.Source, this.Target);
        }
    }
}
