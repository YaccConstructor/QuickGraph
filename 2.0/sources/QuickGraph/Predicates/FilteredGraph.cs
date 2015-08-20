using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredGraph<TVertex, TEdge, TGraph> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IGraph<TVertex,TEdge>
    {
        private TGraph baseGraph;
        private EdgePredicate<TVertex,TEdge> edgePredicate;
        private VertexPredicate<TVertex> vertexPredicate;

        public FilteredGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
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
        public TGraph BaseGraph
        {
            get
            {
                return baseGraph;
            }
        }

        /// <summary>
        /// Edge predicate used to filter the edges
        /// </summary>
        public EdgePredicate<TVertex, TEdge> EdgePredicate
        {
            get
            {
                return edgePredicate;
            }
        }

        public VertexPredicate<TVertex> VertexPredicate
        {
            get
            {
                return vertexPredicate;
            }
        }

        protected bool TestEdge(TEdge edge)
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
