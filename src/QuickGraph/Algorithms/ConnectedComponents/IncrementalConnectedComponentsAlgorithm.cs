using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Algorithms.Services;
using QuickGraph.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ConnectedComponents
{
    public sealed class IncrementalConnectedComponentsAlgorithm<TVertex,TEdge>
        : AlgorithmBase<IMutableVertexAndEdgeSet<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        ForestDisjointSet<TVertex> ds;

        public IncrementalConnectedComponentsAlgorithm(IMutableVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            : this(null, visitedGraph)
        { }

        public IncrementalConnectedComponentsAlgorithm(IAlgorithmComponent host, IMutableVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            : base(host, visitedGraph)
        { }

        protected override void InternalCompute()
        {
            this.ds = new ForestDisjointSet<TVertex>(this.VisitedGraph.VertexCount);
            // initialize 1 set per vertex
            foreach (var v in this.VisitedGraph.Vertices)
                this.ds.MakeSet(v);

            // join existing edges
            foreach (var e in this.VisitedGraph.Edges)
                this.ds.Union(e.Source, e.Target);

            // unhook/hook to graph event
            this.VisitedGraph.EdgeAdded += new EdgeAction<TVertex, TEdge>(VisitedGraph_EdgeAdded);
            this.VisitedGraph.EdgeRemoved += new EdgeAction<TVertex, TEdge>(VisitedGraph_EdgeRemoved);
            this.VisitedGraph.VertexAdded += new VertexAction<TVertex>(VisitedGraph_VertexAdded);
            this.VisitedGraph.VertexRemoved += new VertexAction<TVertex>(VisitedGraph_VertexRemoved);
        }

        public int ComponentCount
        {
            get
            {
                Contract.Assert(this.ds != null);
                return this.ds.SetCount;
            }
        }

        Dictionary<TVertex, int> components;
        /// <summary>
        /// Gets a copy of the connected components. Key is the number of components,
        /// Value contains the vertex -> component index map.
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int, IDictionary<TVertex, int>> GetComponents()
        {
            Contract.Ensures(
                Contract.Result<KeyValuePair<int, IDictionary<TVertex, int>>>().Key == this.ComponentCount);
            Contract.Ensures(
                Contract.Result<KeyValuePair<int, IDictionary<TVertex, int>>>().Value.Count == this.VisitedGraph.VertexCount);
            // TODO: more contracts
            Contract.Assert(this.ds != null);
            
            var representatives = new Dictionary<TVertex, int>(this.ds.SetCount);
            if (this.components == null)
                this.components = new Dictionary<TVertex, int>(this.VisitedGraph.VertexCount);
            foreach (var v in this.VisitedGraph.Vertices)
            {
                var representative = this.ds.FindSet(v);
                int index;
                if (!representatives.TryGetValue(representative, out index))
                    representatives[representative] = index = representatives.Count;
                components[v] = index;
            }

            return new KeyValuePair<int, IDictionary<TVertex, int>>(this.ds.SetCount, components);
        }

        void VisitedGraph_VertexAdded(TVertex v)
        {
            this.ds.MakeSet(v);
        }

        void VisitedGraph_EdgeAdded(TEdge e)
        {
            this.ds.Union(e.Source, e.Target);
        }

        void VisitedGraph_VertexRemoved(TVertex e)
        {
            throw new InvalidOperationException("vertex removal not supported for incremental connected components");
        }

        void VisitedGraph_EdgeRemoved(TEdge e)
        {
            throw new InvalidOperationException("edge removal not supported for incremental connected components");
        }
    }
}
