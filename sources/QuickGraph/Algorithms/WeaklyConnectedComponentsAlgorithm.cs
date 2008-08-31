using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class WeaklyConnectedComponentsAlgorithm<TVertex,TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex,TEdge>>,
        IConnectedComponentAlgorithm<TVertex,TEdge,IVertexListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, int> components;
        private readonly Dictionary<int, int> componentEquivalences = new Dictionary<int, int>();
        private readonly DepthFirstSearchAlgorithm<TVertex, TEdge> dfs;
        private int componentCount = 0;
        private int currentComponent = 0;

        public WeaklyConnectedComponentsAlgorithm(IVertexListGraph<TVertex, TEdge> visitedGraph)
            : this(visitedGraph, new Dictionary<TVertex, int>())
        { }

        public WeaklyConnectedComponentsAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, int> components)
            : this(null, visitedGraph, components)
        { }

        public WeaklyConnectedComponentsAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IDictionary<TVertex, int> components)
            : base(host, visitedGraph)
        {
            if (components == null)
                throw new ArgumentNullException("components");
            this.components = components;

            this.dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(this.VisitedGraph);
            this.dfs.StartVertex += new VertexEventHandler<TVertex>(dfs_StartVertex);
            this.dfs.TreeEdge += new EdgeEventHandler<TVertex, TEdge>(dfs_TreeEdge);
            this.dfs.ForwardOrCrossEdge += new EdgeEventHandler<TVertex, TEdge>(dfs_ForwardOrCrossEdge);
        }

        public IDictionary<TVertex, int> Components
        {
            get { return this.components; }
        }

        public int ComponentCount
        {
            get { return this.componentCount; }
        }

        protected override void  InternalCompute()
        {
            this.componentCount = 0;
            this.currentComponent = 0;
            this.componentEquivalences.Clear();
            this.components.Clear();
            this.dfs.Compute();

            // updating component numbers
            foreach (var v in this.VisitedGraph.Vertices)
            {
                int component = this.Components[v];
                int equivalent = this.componentEquivalences[component];
                if (component!=equivalent)
                    this.Components[v] =equivalent;
            }
        }

        void dfs_StartVertex(object sender, VertexEventArgs<TVertex> e)
        {
            // we are looking on a new tree
            this.currentComponent = this.componentEquivalences.Count;
            this.componentEquivalences.Add(this.currentComponent, this.currentComponent);
            this.componentCount++;
            this.components.Add(e.Vertex, this.currentComponent);
        }

        void dfs_TreeEdge(object sender, EdgeEventArgs<TVertex, TEdge> e)
        {
            // new edge, we store with the current component number
            this.components.Add(e.Edge.Target, this.currentComponent);
        }

        void dfs_ForwardOrCrossEdge(object sender, EdgeEventArgs<TVertex, TEdge> e)
        {
            // we have touched another tree, updating count and current component
            int otherComponent = this.componentEquivalences[this.components[e.Edge.Target]];
            if (otherComponent != this.currentComponent)
            {
                this.componentCount--;
                this.componentEquivalences[this.currentComponent] = otherComponent;
                this.currentComponent = otherComponent;
            }
        }
    }
}
