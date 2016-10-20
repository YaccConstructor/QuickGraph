using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph.Algorithms.ConnectedComponents
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class StronglyConnectedComponentsAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>,
        IConnectedComponentAlgorithm<TVertex, TEdge, IVertexListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, int> components;
        private readonly Dictionary<TVertex, int> discoverTimes;
        private readonly Dictionary<TVertex, TVertex> roots;
        private Stack<TVertex> stack;
        int componentCount;
        int dfsTime;
        private List<int> diffBySteps;
        private int step;
        private List<TVertex> vertices;
        List<BidirectionalGraph<TVertex, TEdge>> graphs;

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<TVertex, TEdge> g)
            : this(g, new Dictionary<TVertex, int>())
        { }

        public StronglyConnectedComponentsAlgorithm(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            : this(null, g, components)
        { }

        public StronglyConnectedComponentsAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            : base(host, g)
        {
            Contract.Requires(components != null);

            this.components = components;
            this.roots = new Dictionary<TVertex, TVertex>();
            this.discoverTimes = new Dictionary<TVertex, int>();
            this.stack = new Stack<TVertex>();
            this.componentCount = 0;
            this.dfsTime = 0;
        }

        public IDictionary<TVertex, int> Components
        {
            get
            {
                return this.components;
            }
        }

        public IDictionary<TVertex, TVertex> Roots
        {
            get
            {
                return this.roots;
            }
        }

        public IDictionary<TVertex, int> DiscoverTimes
        {
            get
            {
                return this.discoverTimes;
            }
        }

        public int ComponentCount
        {
            get
            {
                return this.componentCount;
            }
        }

        public List<TVertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public int Steps
        {
            get
            {
                return step;
            }
        }
        public List<int> DiffBySteps
        {
            get
            {
                return diffBySteps;
            }
        }

        private void DiscoverVertex(TVertex v)
        {
            this.Roots[v] = v;
            this.Components[v] = int.MaxValue;

            // this.diffBySteps[step] = componentCount;
            this.diffBySteps.Add(componentCount);
            this.vertices.Add(v);
            this.step++;

            this.DiscoverTimes[v] = dfsTime++;
            this.stack.Push(v);
        }

        private void FinishVertex(TVertex v)
        {
            var roots = this.Roots;

            foreach (var e in this.VisitedGraph.OutEdges(v))
            {
                var w = e.Target;
                if (this.Components[w] == int.MaxValue)
                    roots[v] = this.MinDiscoverTime(roots[v], roots[w]);
            }

            if (this.roots[v].Equals(v))
            {
                var w = default(TVertex);
                do
                {
                    w = this.stack.Pop();
                    this.Components[w] = componentCount;


                    this.diffBySteps.Add(componentCount);
                    this.vertices.Add(w);
                    this.step++;
                }
                while (!w.Equals(v));
                ++componentCount;

            }
        }

        private TVertex MinDiscoverTime(TVertex u, TVertex v)
        {
            Contract.Requires(u != null);
            Contract.Requires(v != null);
            Contract.Ensures(this.DiscoverTimes[u] < this.DiscoverTimes[v]
                ? Contract.Result<TVertex>().Equals(u)
                : Contract.Result<TVertex>().Equals(v)
                );

            TVertex minVertex = this.discoverTimes[u] < this.discoverTimes[v] ? u : v;
            return minVertex;
        }

        public List<BidirectionalGraph<TVertex, TEdge>> Graphs
        {
            get
            {
                int i;
                graphs = new List<BidirectionalGraph<TVertex, TEdge>>(componentCount + 1);
                for (i = 0; i < componentCount; i++)
                {
                    graphs.Add(new BidirectionalGraph<TVertex, TEdge>());
                }
                foreach (TVertex componentName in components.Keys)
                {
                    graphs[components[componentName]].AddVertex(componentName);
                }

                foreach (TVertex vertex in VisitedGraph.Vertices)
                {
                    foreach (TEdge edge in VisitedGraph.OutEdges(vertex))
                    {

                        if (components[vertex] == components[edge.Target])
                        {
                            graphs[components[vertex]].AddEdge(edge);
                        }
                    }
                }
                return graphs;
            }

        }

        protected override void InternalCompute()
        {
            Contract.Ensures(this.ComponentCount >= 0);
            Contract.Ensures(this.VisitedGraph.VertexCount == 0 || this.ComponentCount > 0);
            Contract.Ensures(Enumerable.All(this.VisitedGraph.Vertices, v => this.Components.ContainsKey(v)));
            Contract.Ensures(this.VisitedGraph.VertexCount == this.Components.Count);
            Contract.Ensures(Enumerable.All(this.Components.Values, c => c <= this.ComponentCount));

            diffBySteps = new List<int>();
            vertices = new List<TVertex>();

            this.Components.Clear();
            this.Roots.Clear();
            this.DiscoverTimes.Clear();
            this.stack.Clear();
            this.componentCount = 0;
            this.dfsTime = 0;

            DepthFirstSearchAlgorithm<TVertex, TEdge> dfs = null;
            try
            {
                dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(
                    this,
                    VisitedGraph,
                    new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount)
                    );
                dfs.DiscoverVertex += DiscoverVertex;
                dfs.FinishVertex += FinishVertex;

                dfs.Compute();
            }
            finally
            {
                if (dfs != null)
                {
                    dfs.DiscoverVertex -= DiscoverVertex;
                    dfs.FinishVertex -= FinishVertex;
                }
            }
        }
    }
}