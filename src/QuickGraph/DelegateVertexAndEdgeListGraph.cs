using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;

namespace QuickGraph
{
    /// <summary>
    /// A delegate-based incidence graph
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateVertexAndEdgeListGraph<TVertex, TEdge>
        : DelegateIncidenceGraph<TVertex, TEdge>
        , IVertexAndEdgeListGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>, IEquatable<TEdge>
    {
        readonly IEnumerable<TVertex> vertices;
        int _vertexCount = -1;
        int _edgeCount = -1;

        public DelegateVertexAndEdgeListGraph(
            IEnumerable<TVertex> vertices,
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
            : base(tryGetOutEdges)
        {
            Contract.Requires(vertices != null);
            Contract.Requires(Enumerable.All(vertices, v =>
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
                // shortcut if count is already computed
                if (this._vertexCount > -1)
                    return this._vertexCount == 0;

                foreach (var vertex in this.vertices)
                    return false;
                return true;
            }
        }

        public int VertexCount
        {
            get
            {
                if (this._vertexCount < 0)
                    this._vertexCount = Enumerable.Count(this.vertices);
                return this._vertexCount;
            }
        }

        public virtual IEnumerable<TVertex> Vertices
        {
            get { return this.vertices; }
        }

        public bool IsEdgesEmpty
        {
            get 
            {
                // shortcut if edges is already computed
                if (this._edgeCount > -1)
                    return this._edgeCount == 0;

                foreach (var vertex in this.vertices)
                    foreach (var edge in this.OutEdges(vertex))
                        return false;
                return true;
            }
        }

        public int EdgeCount
        {
            get
            {
                if (this._edgeCount < 0)
                    this._edgeCount = Enumerable.Count(this.Edges);
                return this._edgeCount;
            }
        }

        public virtual IEnumerable<TEdge> Edges
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
