using System;
using System.Collections.Generic;
namespace QuickGraph.Predicates
{
    [Serializable]
    public class FilteredVertexListGraph<Vertex, Edge, Graph> :
        FilteredIncidenceGraph<Vertex,Edge,Graph>,
        IVertexListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
        where Graph : IVertexListGraph<Vertex,Edge>
    {
        public FilteredVertexListGraph(
            Graph baseGraph,
            VertexPredicate<Vertex> vertexPredicate,
            EdgePredicate<Vertex, Edge> edgePredicate
            )
            :base(baseGraph,vertexPredicate,edgePredicate)
        { }

        public bool IsVerticesEmpty
        {
            get 
            {
                foreach (Vertex v in this.Vertices)
                        return false;
                return true;
            }
        }

        public int VertexCount
        {
            get 
            {
                int count = 0;
                foreach (Vertex v in this.Vertices)
                        count++;
                return count;
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get 
            {
                foreach (Vertex v in this.BaseGraph.Vertices)
                    if (this.VertexPredicate(v))
                        yield return v;
            }
        }

        public bool ContainsVertex(Vertex vertex)
        {
            if (!this.VertexPredicate(vertex))
                return false;
            return this.ContainsVertex(vertex);
        }
    }
}
