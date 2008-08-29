using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class ConnectedComponentsAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IUndirectedGraph<TVertex, TEdge>>,
        IConnectedComponentAlgorithm<TVertex,TEdge,IUndirectedGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> components;
        private UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge> dfs;
        private int componentCount=0;

        public ConnectedComponentsAlgorithm(IUndirectedGraph<TVertex, TEdge> g)
            :this(g, new Dictionary<TVertex, int>())
        { }

        public ConnectedComponentsAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, int> components)
            :base(visitedGraph)
        {
            if (components == null)
                throw new ArgumentNullException("components");

            this.components = components;
            dfs = new UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);

            dfs.StartVertex += new VertexEventHandler<TVertex>(this.StartVertex);
            dfs.DiscoverVertex += new VertexEventHandler<TVertex>(this.DiscoverVertex);
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

            if (this.VisitedGraph.VertexCount != 0)
            {
                TVertex rootVertex;
                if (!this.TryGetRootVertex(out rootVertex))
                    rootVertex = TraversalHelper.GetFirstVertex<TVertex, TEdge>(this.VisitedGraph);
                dfs.Compute(rootVertex);
            }

            ++this.componentCount;
        }
    }
}
