using System;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public abstract class RootedAlgorithmBase<TVertex,TGraph> : 
        AlgorithmBase<TGraph>
    {
        private TVertex rootVertex;

        public RootedAlgorithmBase(TGraph visitedGraph)
            :base(visitedGraph)
        {}

        public TVertex RootVertex
        {
            get { return this.rootVertex; }
            set 
            {
                bool changed = !Comparison<TVertex>.Equals(this.rootVertex, value);
                this.rootVertex = value;
                if (changed)
                    this.OnRooVertexChanged(EventArgs.Empty);
            }
        }

        public event EventHandler RootVertexChanged;
        protected virtual void OnRooVertexChanged(EventArgs e)
        {
            if (this.RootVertexChanged != null)
                this.RootVertexChanged(this, e);
        }

        public void Compute(TVertex rootVertex)
        {
            if (rootVertex == null)
                throw new ArgumentNullException("rootVertex");
            this.RootVertex = rootVertex;
            this.Compute();
        }
    }
}
