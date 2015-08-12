using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class FilteredGraph<TVertex, TEdge, TGraph> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TGraph : IGraph<TVertex,TEdge>
    {
        private readonly TGraph baseGraph;
        private readonly EdgePredicate<TVertex,TEdge> edgePredicate;
        private readonly VertexPredicate<TVertex> vertexPredicate;

        public FilteredGraph(
            TGraph baseGraph,
            VertexPredicate<TVertex> vertexPredicate,
            EdgePredicate<TVertex, TEdge> edgePredicate
            )
        {
            Contract.Requires(baseGraph != null);
            Contract.Requires(vertexPredicate != null);
            Contract.Requires(edgePredicate != null);

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
