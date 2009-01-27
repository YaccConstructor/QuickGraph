using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ConnectedComponents
{
    [Serializable]
    public sealed class ConnectedComponentsAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IUndirectedGraph<TVertex, TEdge>>,
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
            Components[v] = this.componentCount;
        }

        protected override void InternalCompute()
        {
            this.componentCount = -1;
            this.components.Clear();

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

                if (this.VisitedGraph.VertexCount != 0)
                {
                    TVertex rootVertex;
                    if (!this.TryGetRootVertex(out rootVertex))
                    {
                        foreach (var v in this.VisitedGraph.Vertices)
                        {
                            rootVertex = v;
                            break;
                        }
                    }
                    dfs.Compute(rootVertex);
                }

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
