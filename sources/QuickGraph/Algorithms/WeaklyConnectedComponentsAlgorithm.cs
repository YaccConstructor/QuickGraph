using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class WeaklyConnectedComponentsAlgorithm<Vertex,Edge> :
        AlgorithmBase<IVertexListGraph<Vertex,Edge>>,
        IConnectedComponentAlgorithm<Vertex,Edge,IVertexListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private readonly IDictionary<Vertex, int> components;
        private readonly Dictionary<int, int> componentEquivalences = new Dictionary<int, int>();
        private readonly DepthFirstSearchAlgorithm<Vertex, Edge> dfs;
        private int componentCount = 0;
        private int currentComponent = 0;

        public WeaklyConnectedComponentsAlgorithm(
            IVertexListGraph<Vertex, Edge> visitedGraph
            )
            : this(visitedGraph, new Dictionary<Vertex, int>()
            )
        { }

        public WeaklyConnectedComponentsAlgorithm(
            IVertexListGraph<Vertex, Edge> visitedGraph,
            IDictionary<Vertex,int> components
            )
            :base(visitedGraph)
        {
            if (components == null)
                throw new ArgumentNullException("components");
            this.components = components;

            this.dfs = new DepthFirstSearchAlgorithm<Vertex, Edge>(this.VisitedGraph);
            this.dfs.StartVertex += new VertexEventHandler<Vertex>(dfs_StartVertex);
            this.dfs.TreeEdge += new EdgeEventHandler<Vertex, Edge>(dfs_TreeEdge);
            this.dfs.ForwardOrCrossEdge += new EdgeEventHandler<Vertex, Edge>(dfs_ForwardOrCrossEdge);
        }

        public IDictionary<Vertex, int> Components
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
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                int component = this.Components[v];
                int equivalent = this.componentEquivalences[component];
                if (component!=equivalent)
                    this.Components[v] =equivalent;
            }
        }

        void dfs_StartVertex(object sender, VertexEventArgs<Vertex> e)
        {
            // we are looking on a new tree
            this.currentComponent = this.componentEquivalences.Count;
            this.componentEquivalences.Add(this.currentComponent, this.currentComponent);
            this.componentCount++;
            this.components.Add(e.Vertex, this.currentComponent);
        }

        void dfs_TreeEdge(object sender, EdgeEventArgs<Vertex, Edge> e)
        {
            // new edge, we store with the current component number
            this.components.Add(e.Edge.Target, this.currentComponent);
        }

        void dfs_ForwardOrCrossEdge(object sender, EdgeEventArgs<Vertex, Edge> e)
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
