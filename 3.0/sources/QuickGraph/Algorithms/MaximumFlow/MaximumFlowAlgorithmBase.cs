using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MaximumFlow
{
    /// <summary>
    /// Abstract base class for maximum flow algorithms.
    /// </summary>
    [Serializable]
    public abstract class MaximumFlowAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex, TEdge>>,
        IVertexColorizerAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex,TEdge> predecessors;
        private IDictionary<TEdge,double> capacities;
        private IDictionary<TEdge,double> residualCapacities;
        private IDictionary<TEdge,TEdge> reversedEdges;
        private IDictionary<TVertex,GraphColor> vertexColors;
        private TVertex source;
        private TVertex sink;
        private double maxFlow = 0;

        protected MaximumFlowAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex,TEdge> visitedGraph,
            IDictionary<TEdge, double> capacities,
            IDictionary<TEdge,TEdge> reversedEdges
            )
            :base(host, visitedGraph)
        {
            if (capacities == null)
                throw new ArgumentNullException("capacities");
            if (reversedEdges == null)
                throw new ArgumentNullException("reversedEdges");

            this.capacities = capacities;
            this.reversedEdges = reversedEdges;

            this.predecessors = new Dictionary<TVertex,TEdge>();
            this.residualCapacities = new Dictionary<TEdge,double>();
            this.vertexColors = new Dictionary<TVertex, GraphColor>();
        }

        public IDictionary<TVertex,TEdge> Predecessors
        {
            get
            {
                return predecessors;
            }
        }

        public IDictionary<TEdge,double> Capacities
        {
            get
            {
                return capacities;
            }
        }

        public IDictionary<TEdge,double> ResidualCapacities
        {
            get
            {
                return residualCapacities;
            }
        }

        public IDictionary<TEdge,TEdge> ReversedEdges
        {
            get
            {
                return reversedEdges;
            }
        }

        public IDictionary<TVertex,GraphColor> VertexColors
        {
            get
            {
                return vertexColors;
            }
        }

        public TVertex Source
        {
            get { return this.source; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("source");
                this.source = value; 
            }
        }

        public TVertex Sink
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

        public double Compute(TVertex source, TVertex sink)
        {
            this.Source = source;
            this.Sink = sink;
            this.Compute();
            return this.MaxFlow;
        }
    }

}
