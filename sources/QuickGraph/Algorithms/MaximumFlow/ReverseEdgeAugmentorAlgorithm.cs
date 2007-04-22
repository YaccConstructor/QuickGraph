using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [Serializable]
    public sealed class ReversedEdgeAugmentorAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IMutableVertexAndEdgeListGraph<Vertex,Edge> visitedGraph;
        private IEdgeFactory<Vertex, Edge> edgeFactory;
        private IList<Edge> augmentedEgdes = new List<Edge>();
        private IDictionary<Edge,Edge> reversedEdges = new Dictionary<Edge,Edge>();
        private bool augmented = false;

        public ReversedEdgeAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex,Edge> visitedGraph,
            IEdgeFactory<Vertex,Edge> edgeFactory
            )
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");
            this.visitedGraph = visitedGraph;
            this.edgeFactory = edgeFactory;
        }

        public IMutableVertexAndEdgeListGraph<Vertex,Edge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public IEdgeFactory<Vertex, Edge> EdgeFactory
        {
            get { return this.edgeFactory; }
        }

        public ICollection<Edge> AugmentedEdges
        {
            get
            {
                return this.augmentedEgdes;
            }
        }

        public IDictionary<Edge,Edge> ReversedEdges
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

        public event EdgeEventHandler<Vertex,Edge> ReversedEdgeAdded;
        private void OnReservedEdgeAdded(EdgeEventArgs<Vertex,Edge> e)
        {
            if (this.ReversedEdgeAdded != null)
                this.ReversedEdgeAdded(this, e);
        }

        public void AddReversedEdges()
        {
            if (this.Augmented)
                throw new InvalidOperationException("Graph already augmented");
            // step 1, find edges that need reversing
            IList<Edge> notReversedEdges = new List<Edge>();
            foreach (Edge edge in this.VisitedGraph.Edges)
            {
                // if reversed already found, continue
                if (this.reversedEdges.ContainsKey(edge))
                    continue;

                Edge reversedEdge = this.FindReversedEdge(edge);
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
            foreach (Edge edge in notReversedEdges)
            {
                if (this.reversedEdges.ContainsKey(edge))
                    continue;

                // already been added
                Edge reversedEdge = this.FindReversedEdge(edge);
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
                this.OnReservedEdgeAdded(new EdgeEventArgs<Vertex,Edge>(reversedEdge));
            }

            this.augmented = true;
        }

        public void RemoveReversedEdges()
        {
            if (!this.Augmented)
                throw new InvalidOperationException("Graph is not yet augmented");

            foreach (Edge edge in this.augmentedEgdes)
                this.VisitedGraph.RemoveEdge(edge);

            this.augmentedEgdes.Clear();
            this.reversedEdges.Clear();

            this.augmented = false;
        }

        private Edge FindReversedEdge(Edge edge)
        {
            foreach (Edge redge in this.VisitedGraph.OutEdges(edge.Target))
                if (redge.Target.Equals(edge.Source))
                    return redge;
            return default(Edge);
        }
    }
}
