using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms
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
            if (components == null)
                throw new ArgumentNullException("components");

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

        private void StartVertex(Object sender, VertexEventArgs<TVertex> args)
        {
            ++this.componentCount;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<TVertex> args)
        {
            Components[args.Vertex] = this.componentCount;
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

                dfs.StartVertex += new VertexEventHandler<TVertex>(this.StartVertex);
                dfs.DiscoverVertex += new VertexEventHandler<TVertex>(this.DiscoverVertex);

                if (this.VisitedGraph.VertexCount != 0)
                {
                    TVertex rootVertex;
                    if (!this.TryGetRootVertex(out rootVertex))
                        rootVertex = TraversalHelper.GetFirstVertex<TVertex, TEdge>(this.VisitedGraph);
                    dfs.Compute(rootVertex);
                }

                ++this.componentCount;
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.StartVertex -= new VertexEventHandler<TVertex>(this.StartVertex);
                    dfs.DiscoverVertex -= new VertexEventHandler<TVertex>(this.DiscoverVertex);
                }
            }
        }
    }
}
