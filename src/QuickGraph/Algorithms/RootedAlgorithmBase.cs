using System;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class RootedAlgorithmBase<TVertex,TGraph> 
        : AlgorithmBase<TGraph>
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
            Contract.Requires(rootVertex != null);

            bool changed = Comparer<TVertex>.Default.Compare(this.rootVertex, rootVertex) != 0;
            this.rootVertex = rootVertex;
            if (changed)
                this.OnRootVertexChanged(EventArgs.Empty);
            this.hasRootVertex = true;
        }

        public void ClearRootVertex()
        {
            this.rootVertex = default(TVertex);
            this.hasRootVertex = false;
        }

        public event EventHandler RootVertexChanged;
        protected virtual void OnRootVertexChanged(EventArgs e)
        {
            Contract.Requires(e != null);

            var eh = this.RootVertexChanged;
            if (eh != null)
                eh(this, e);
        }

        public void Compute(TVertex rootVertex)
        {
            Contract.Requires(rootVertex != null);

            this.SetRootVertex(rootVertex);
            this.Compute();
        }
    }
}
