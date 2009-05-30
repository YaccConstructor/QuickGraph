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
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateImplicitUndirectedGraph<TVertex, TEdge>
        : IImplicitUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly TryFunc<TVertex, IEnumerable<TEdge>> tryGetAdjacentEdges;
        readonly bool allowParallelEdges;

        public DelegateImplicitUndirectedGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetAdjacenyEdges,
            bool allowParallelEdges)
        {
            Contract.Requires(tryGetAdjacenyEdges != null);

            this.tryGetAdjacentEdges = tryGetAdjacenyEdges;
            this.allowParallelEdges = allowParallelEdges;
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
                throw new ArgumentOutOfRangeException("v");
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

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            IEnumerable<TEdge> edges;
            if (this.TryGetAdjacentEdges(source, out edges))
                foreach (var edge in edges)
                    if (EdgeExtensions.GetOtherVertex(edge, source).Equals(target))
                        return true;
            return false;
        }
    }
}
