using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ConnectedComponents
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class ConnectedComponentsAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>,
        IConnectedComponentAlgorithm<TVertex,TEdge,IUndirectedGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> components;
        private int componentCount=0;

        public ConnectedComponentsAlgorithm(IUndirectedGraph<TVertex, TEdge> g)
            :this(g, new Dictionary<TVertex, int>())
        { }

        public ConnectedComponentsAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, int> components)
            : this(null, visitedGraph, components)
        { }

        public ConnectedComponentsAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, int> components)
            :base(host, visitedGraph)
        {
            Contract.Requires(components != null);

            this.components = components;
        }

        public IDictionary<TVertex,int> Components
        {
            get
            {
                return this.components;
            }
        }

        public int ComponentCount
        {
            get { return this.componentCount; }
        }

        private void StartVertex(TVertex v)
        {
            ++this.componentCount;
        }

        private void DiscoverVertex(TVertex v)
        {
            this.Components[v] = this.componentCount;
        }

        protected override void InternalCompute()
        {
            this.components.Clear();
            if (this.VisitedGraph.VertexCount == 0)
            {
                this.componentCount = 0;
                return;
            }

            this.componentCount = -1;
            UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge> dfs = null;
            try
            {
                dfs = new UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge>(
                    this,
                    this.VisitedGraph,
                    new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount)
                    );

                dfs.StartVertex += new VertexAction<TVertex>(this.StartVertex);
                dfs.DiscoverVertex += new VertexAction<TVertex>(this.DiscoverVertex);
                dfs.Compute();
                ++this.componentCount;
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.StartVertex -= new VertexAction<TVertex>(this.StartVertex);
                    dfs.DiscoverVertex -= new VertexAction<TVertex>(this.DiscoverVertex);
                }
            }
        }
    }
}
