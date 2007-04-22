using System;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public abstract class RootedAlgorithmBase<Vertex,Graph> : 
        AlgorithmBase<Graph>
    {
        private Vertex rootVertex;

        public RootedAlgorithmBase(Graph visitedGraph)
            :base(visitedGraph)
        {}

        public Vertex RootVertex
        {
            get { return this.rootVertex; }
            set 
            {
                bool changed = !Comparison<Vertex>.Equals(this.rootVertex, value);
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

        public void Compute(Vertex rootVertex)
        {
            if (rootVertex == null)
                throw new ArgumentNullException("rootVertex");
            this.RootVertex = rootVertex;
            this.Compute();
        }
    }
}
