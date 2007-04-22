using System;

namespace QuickGraph
{
    [Serializable]
    public class MarkedEdge<V,M> :
        Edge<V>
    {
        private M marker;
        public MarkedEdge(V source, V target, M marker)
            :base(source,target)
        {
            this.marker = marker;
        }

        public M Marker
        {
            get { return this.marker; }
            set { this.marker = value; }
        }
    }
}
