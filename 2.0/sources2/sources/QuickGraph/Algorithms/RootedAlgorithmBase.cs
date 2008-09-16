using System;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public abstract class RootedAlgorithmBase<TVertex,TGraph> : 
        AlgorithmBase<TGraph>
    {
        private TVertex rootVertex;
        private bool hasRootVertex;

        protected RootedAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph)
            :base(host, visitedGraph)
        {}

        public bool TryGetRootVertex(out TVertex rootVertex)
        {
            if (this.hasRootVertex)
            {
                rootVertex = this.rootVertex;
                return true;
            }
            else
            {
                rootVertex = default(TVertex);
                return false;
            }
        }

        public void SetRootVertex(TVertex rootVertex)
        {
            GraphContracts.AssumeNotNull(rootVertex, "rootVertex");
            // GraphContracts.AssumeInVertexSet(this.VisitedGraph, rootVertex, "rootVertex");

            bool changed = !Comparison<TVertex>.Equals(this.rootVertex, rootVertex);
            this.rootVertex = rootVertex;
            if (changed)
                this.OnRooVertexChanged(EventArgs.Empty);
            this.hasRootVertex = true;
        }

        public void ClearRootVertex()
        {
            this.rootVertex = default(TVertex);
            this.hasRootVertex = false;
        }

        public event EventHandler RootVertexChanged;
        protected virtual void OnRooVertexChanged(EventArgs e)
        {
            if (this.RootVertexChanged != null)
                this.RootVertexChanged(this, e);
        }

        public void Compute(TVertex rootVertex)
        {
            GraphContracts.AssumeNotNull(rootVertex, "rootVertex");
            // GraphContracts.AssumeInVertexSet(this.VisitedGraph, rootVertex, "rootVertex");

            this.SetRootVertex(rootVertex);
            this.Compute();
        }
    }
}
