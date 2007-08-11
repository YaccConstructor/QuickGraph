using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredImplicitGraph<Vertex, Edge, Graph> :
        FilteredGraph<Vertex, Edge, Graph>,
        IImplicitGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
        where Graph : IImplicitGraph<Vertex, Edge>
    {
        public FilteredImplicitGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex, Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsOutEdgesEmpty(Vertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(Vertex v)
        {
            int count =0;
            foreach (Edge edge in this.BaseGraph.OutEdges(v))
                if (this.TestEdge(edge))
                    count++;
            return count;
        }

        public IEnumerable<Edge> OutEdges(Vertex v)
        {
            foreach (Edge edge in this.BaseGraph.OutEdges(v))
                if (this.TestEdge(edge))
                    yield return edge;
        }

        public Edge OutEdge(Vertex v, int index)
        {
            throw new NotSupportedException();
        }
    }
}
