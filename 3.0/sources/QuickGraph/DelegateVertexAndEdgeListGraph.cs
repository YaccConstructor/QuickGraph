using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A delegate-based incidence graph
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateVertexAndEdgeListGraph<TVertex, TEdge>
        : DelegateIncidenceGraph<TVertex, TEdge>
        , IVertexAndEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>, IEquatable<TEdge>
    {
        readonly IEnumerable<TVertex> vertices;

        public DelegateVertexAndEdgeListGraph(
            IEnumerable<TVertex> vertices,
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
            : base(tryGetOutEdges)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(Contract.ForAll(vertices, v =>
            {
                IEnumerable<TEdge> edges;
                return tryGetOutEdges(v, out edges);
            }));

            this.vertices = vertices;
        }

        public bool IsVerticesEmpty
        {
            get
            {
                foreach (var vertex in this.vertices)
                    return false;
                return true;
            }
        }

        int vertexCount = -1;
        public int VertexCount
        {
            get
            {
                if (this.vertexCount < 0)
                    this.vertexCount = Enumerable.Count(this.vertices);
                return this.vertexCount;
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return this.vertices; }
        }

        public bool IsEdgesEmpty
        {
            get {
                foreach (var vertex in this.vertices)
                    foreach (var edge in this.OutEdges(vertex))
                        return false;
                return true;
            }
        }

        int edgeCount = -1;
        public int EdgeCount
        {
            get
            {
                if (this.edgeCount < 0)
                {
                    int count = 0;
                    foreach (var vertex in this.vertices)
                        foreach (var edge in this.OutEdges(vertex))
                            count++;
                    this.edgeCount = count;
                }
                return this.edgeCount;
            }
        }

        public IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var vertex in this.vertices)
                    foreach (var edge in this.OutEdges(vertex))
                        yield return edge;
            }
        }

        public bool ContainsEdge(TEdge edge)
        {
            IEnumerable<TEdge> edges;
            if (this.TryGetOutEdges(edge.Source, out edges))
                foreach(var e in edges)
                    if (e.Equals(edge))
                        return true;
            return false;
        }
    }
}
