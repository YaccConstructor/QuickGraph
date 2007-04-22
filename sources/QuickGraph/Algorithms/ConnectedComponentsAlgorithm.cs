using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class ConnectedComponentsAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex,IUndirectedGraph<Vertex, Edge>>,
        IConnectedComponentAlgorithm<Vertex,Edge,IUndirectedGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, int> components;
        private UndirectedDepthFirstSearchAlgorithm<Vertex, Edge> dfs;
        private int componentCount=0;

        public ConnectedComponentsAlgorithm(IUndirectedGraph<Vertex, Edge> g)
            :this(g, new Dictionary<Vertex, int>())
        { }

        public ConnectedComponentsAlgorithm(
            IUndirectedGraph<Vertex, Edge> visitedGraph,
            IDictionary<Vertex, int> components)
            :base(visitedGraph)
        {
            if (components == null)
                throw new ArgumentNullException("components");

            this.components = components;
            dfs = new UndirectedDepthFirstSearchAlgorithm<Vertex, Edge>(visitedGraph);

            dfs.StartVertex += new VertexEventHandler<Vertex>(this.StartVertex);
            dfs.DiscoverVertex += new VertexEventHandler<Vertex>(this.DiscoverVertex);
        }

        public IDictionary<Vertex,int> Components
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

        private void StartVertex(Object sender, VertexEventArgs<Vertex> args)
        {
            ++this.componentCount;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<Vertex> args)
        {
            Components[args.Vertex] = this.componentCount;
        }

        protected override void InternalCompute()
        {
            this.componentCount = -1;
            this.components.Clear();

            if (this.VisitedGraph.VertexCount != 0)
            {
                if (this.RootVertex == null)
                    this.RootVertex = TraversalHelper.GetFirstVertex<Vertex, Edge>(this.VisitedGraph);
                dfs.Compute(this.RootVertex);
            }

            ++this.componentCount;
        }
    }
}
