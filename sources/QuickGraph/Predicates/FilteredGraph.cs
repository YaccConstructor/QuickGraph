using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredGraph<Vertex, Edge, Graph> : IGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
        where Graph : IGraph<Vertex,Edge>
    {
        private Graph baseGraph;
        private EdgePredicate<Vertex,Edge> edgePredicate;
        private VertexPredicate<Vertex> vertexPredicate;

        public FilteredGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex, Edge> edgePredicate
            )
        {
            if (baseGraph == null)
                throw new ArgumentNullException("baseGraph");
           if (vertexPredicate == null)
                throw new ArgumentNullException("vertexPredicate");
            if (edgePredicate == null)
                throw new ArgumentNullException("edgePredicate");
            this.baseGraph = baseGraph;
            this.vertexPredicate = vertexPredicate;
            this.edgePredicate = edgePredicate;
        }

        /// <summary>
        /// Underlying filtered graph
        /// </summary>
        public Graph BaseGraph
        {
            get
            {
                return baseGraph;
            }
        }

        /// <summary>
        /// Edge predicate used to filter the edges
        /// </summary>
        public EdgePredicate<Vertex, Edge> EdgePredicate
        {
            get
            {
                return edgePredicate;
            }
        }

        public VertexPredicate<Vertex> VertexPredicate
        {
            get
            {
                return vertexPredicate;
            }
        }

        protected bool TestEdge(Edge edge)
        {
            return this.VertexPredicate(edge.Source)
                    && this.VertexPredicate(edge.Target)
                    && this.EdgePredicate(edge);
        }

        public bool IsDirected
        {
            get { return this.BaseGraph.IsDirected; }
        }

        public bool AllowParallelEdges
        {
            get
            {
                return baseGraph.AllowParallelEdges;
            }
        }
    }
}
