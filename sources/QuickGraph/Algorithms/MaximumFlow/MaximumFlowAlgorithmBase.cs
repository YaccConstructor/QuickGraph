using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.MaximumFlow
{
    /// <summary>
    /// Abstract base class for maximum flow algorithms.
    /// </summary>
    [Serializable]
    public abstract class MaximumFlowAlgorithm<Vertex, Edge> :
        AlgorithmBase<IVertexListGraph<Vertex, Edge>>,
        IVertexColorizerAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex,Edge> predecessors;
        private IDictionary<Edge,double> capacities;
        private IDictionary<Edge,double> residualCapacities;
        private IDictionary<Edge,Edge> reversedEdges;
        private IDictionary<Vertex,GraphColor> vertexColors;
        private Vertex source;
        private Vertex sink;
        private double maxFlow = 0;

        public MaximumFlowAlgorithm(
            IVertexListGraph<Vertex,Edge> visitedGraph,
            IDictionary<Edge, double> capacities,
            IDictionary<Edge,Edge> reversedEdges
            )
            :base(visitedGraph)
        {
            if (capacities == null)
                throw new ArgumentNullException("capacities");
            if (reversedEdges == null)
                throw new ArgumentNullException("reversedEdges");

            this.capacities = capacities;
            this.reversedEdges = reversedEdges;

            this.predecessors = new Dictionary<Vertex,Edge>();
            this.residualCapacities = new Dictionary<Edge,double>();
            this.vertexColors = new Dictionary<Vertex, GraphColor>();
        }

        public IDictionary<Vertex,Edge> Predecessors
        {
            get
            {
                return predecessors;
            }
        }

        public IDictionary<Edge,double> Capacities
        {
            get
            {
                return capacities;
            }
        }

        public IDictionary<Edge,double> ResidualCapacities
        {
            get
            {
                return residualCapacities;
            }
        }

        public IDictionary<Edge,Edge> ReversedEdges
        {
            get
            {
                return reversedEdges;
            }
        }

        public IDictionary<Vertex,GraphColor> VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        public Vertex Source
        {
            get { return this.source; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("source");
                this.source = value; 
            }
        }

        public Vertex Sink
        {
            get { return this.sink; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("sink");
                this.sink = value; 
            }
        }

        public double MaxFlow
        {
            get { return this.maxFlow; }
            set { this.maxFlow = value; }
        }

        public double Compute(Vertex source, Vertex sink)
        {
            this.Source = source;
            this.Sink = sink;
            this.Compute();
            return this.MaxFlow;
        }
    }

}
