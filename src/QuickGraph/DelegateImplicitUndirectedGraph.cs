using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A functional implicit undirected graph
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateImplicitUndirectedGraph<TVertex, TEdge>
        : IImplicitUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly TryFunc<TVertex, IEnumerable<TEdge>> tryGetAdjacentEdges;
        readonly bool allowParallelEdges;
        readonly EdgeEqualityComparer<TVertex, TEdge> edgeEquality =
            EdgeExtensions.GetUndirectedVertexEquality<TVertex, TEdge>();

        public DelegateImplicitUndirectedGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetAdjacenyEdges,
            bool allowParallelEdges)
        {
            Contract.Requires(tryGetAdjacenyEdges != null);

            this.tryGetAdjacentEdges = tryGetAdjacenyEdges;
            this.allowParallelEdges = allowParallelEdges;
        }

        public EdgeEqualityComparer<TVertex, TEdge> EdgeEqualityComparer
        {
            get { return this.edgeEquality; }
        }

        public TryFunc<TVertex, IEnumerable<TEdge>> TryGetAdjacencyEdgesFunc
        {
            get { return this.tryGetAdjacentEdges; }
        }

        public bool IsAdjacentEdgesEmpty(TVertex v)
        {
            foreach (var edge in this.AdjacentEdges(v))
                return false;
            return true;
        }

        public int AdjacentDegree(TVertex v)
        {
            return Enumerable.Count(this.AdjacentEdges(v));
        }

        public IEnumerable<TEdge> AdjacentEdges(TVertex v)
        {
            IEnumerable<TEdge> result;
            if (!this.tryGetAdjacentEdges(v, out result))
                return Enumerable.Empty<TEdge>();
            return result;
        }

        public bool TryGetAdjacentEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            return this.tryGetAdjacentEdges(v, out edges);
        }

        public TEdge AdjacentEdge(TVertex v, int index)
        {
            return Enumerable.ElementAt(this.AdjacentEdges(v), index);
        }

        public bool IsDirected
        {
            get { return false; }
        }

        public bool AllowParallelEdges
        {
            get { return this.allowParallelEdges; }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            IEnumerable<TEdge> edges;
            return
                this.tryGetAdjacentEdges(vertex, out edges);
        }

        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            IEnumerable<TEdge> edges;
            if (this.TryGetAdjacentEdges(source, out edges))
                foreach (var e in edges)
                    if (this.edgeEquality(e, source, target))
                    {
                        edge = e;
                        return true;
                    }

            edge = default(TEdge);
            return false;
        }

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            TEdge edge;
            return this.TryGetEdge(source, target, out edge);
        }
    }
}
