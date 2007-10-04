using System;

namespace QuickGraph
{
    [Serializable]
    public class Edge<TVertex> : IEdge<TVertex>
    {
        private readonly TVertex source;
        private readonly TVertex target;

        public Edge(TVertex source, TVertex target)
        {
            GraphContracts.AssumeNotNull(source, "source");
            GraphContracts.AssumeNotNull(target, "target");

            this.source = source;
            this.target = target;
        }

        public static Type VertexType
        {
            get { return typeof(TVertex); }
        }

        public TVertex Source
        {
            get { return this.source; }
        }

        public TVertex Target
        {
            get { return this.target; }
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", this.Source, this.Target);
        }
    }
}
