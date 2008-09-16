using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [Serializable]
    public sealed class ReversedEdgeAugmentorAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IMutableVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph;
        private IEdgeFactory<TVertex, TEdge> edgeFactory;
        private IList<TEdge> augmentedEgdes = new List<TEdge>();
        private IDictionary<TEdge,TEdge> reversedEdges = new Dictionary<TEdge,TEdge>();
        private bool augmented = false;

        public ReversedEdgeAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IEdgeFactory<TVertex, TEdge> edgeFactory)
            : this(null, visitedGraph, edgeFactory)
        { }

        public ReversedEdgeAugmentorAlgorithm(
            IAlgorithmComponent host,
            IMutableVertexAndEdgeListGraph<TVertex,TEdge> visitedGraph,
            IEdgeFactory<TVertex,TEdge> edgeFactory)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");
            this.visitedGraph = visitedGraph;
            this.edgeFactory = edgeFactory;
        }

        public IMutableVertexAndEdgeListGraph<TVertex,TEdge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public IEdgeFactory<TVertex, TEdge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public ICollection<TEdge> AugmentedEdges
        {
            get
            {
                return this.augmentedEgdes;
            }
        }

        public IDictionary<TEdge,TEdge> ReversedEdges
        {
            get
            {
                return this.reversedEdges;
            }
        }

        public bool Augmented
        {
            get
            {
                return this.augmented;
            }
        }

        public event EdgeEventHandler<TVertex,TEdge> ReversedEdgeAdded;
        private void OnReservedEdgeAdded(EdgeEventArgs<TVertex,TEdge> e)
        {
            if (this.ReversedEdgeAdded != null)
                this.ReversedEdgeAdded(this, e);
        }

        public void AddReversedEdges()
        {
            if (this.Augmented)
                throw new InvalidOperationException("Graph already augmented");
            // step 1, find edges that need reversing
            IList<TEdge> notReversedEdges = new List<TEdge>();
            foreach (var edge in this.VisitedGraph.Edges)
            {
                // if reversed already found, continue
                if (this.reversedEdges.ContainsKey(edge))
                    continue;

                TEdge reversedEdge = this.FindReversedEdge(edge);
                if (reversedEdge != null)
                {
                    // setup edge
                    this.reversedEdges[edge] = reversedEdge;
                    // setup reversed if needed
                    if (!this.reversedEdges.ContainsKey(reversedEdge))
                        this.reversedEdges[reversedEdge] = edge;
                    continue;
                }

                // this edge has no reverse
                notReversedEdges.Add(edge);
            }

            // step 2, go over each not reversed edge, add reverse
            foreach (var edge in notReversedEdges)
            {
                if (this.reversedEdges.ContainsKey(edge))
                    continue;

                // already been added
                TEdge reversedEdge = this.FindReversedEdge(edge);
                if (reversedEdge != null)
                {
                    this.reversedEdges[edge] = reversedEdge;
                    continue;
                }

                // need to create one
                reversedEdge = this.edgeFactory.CreateEdge(edge.Target, edge.Source);
                if (!this.VisitedGraph.AddEdge(reversedEdge))
                    throw new InvalidOperationException("We should not be here");
                this.augmentedEgdes.Add(reversedEdge);
                this.reversedEdges[edge] = reversedEdge;
                this.reversedEdges[reversedEdge] = edge;
                this.OnReservedEdgeAdded(new EdgeEventArgs<TVertex,TEdge>(reversedEdge));
            }

            this.augmented = true;
        }

        public void RemoveReversedEdges()
        {
            if (!this.Augmented)
                throw new InvalidOperationException("Graph is not yet augmented");

            foreach (var edge in this.augmentedEgdes)
                this.VisitedGraph.RemoveEdge(edge);

            this.augmentedEgdes.Clear();
            this.reversedEdges.Clear();

            this.augmented = false;
        }

        private TEdge FindReversedEdge(TEdge edge)
        {
            foreach (var redge in this.VisitedGraph.OutEdges(edge.Target))
                if (redge.Target.Equals(edge.Source))
                    return redge;
            return default(TEdge);
        }
    }
}
