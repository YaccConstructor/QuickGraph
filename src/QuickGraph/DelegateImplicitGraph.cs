using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;

namespace QuickGraph
{
    /// <summary>
    /// A delegate-based implicit graph
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateImplicitGraph<TVertex, TEdge>
        : IImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>, IEquatable<TEdge>
    {
        readonly TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges;

        public DelegateImplicitGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
        {
            Contract.Requires(tryGetOutEdges != null);

            this.tryGetOutEdges = tryGetOutEdges;
        }

        public TryFunc<TVertex, IEnumerable<TEdge>> TryGetOutEdgesFunc
        {
            get { return this.tryGetOutEdges; }
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            foreach (var edge in this.OutEdges(v))
                return false;
            return true;
        }

        public int OutDegree(TVertex v)
        {
            return Enumerable.Count(this.OutEdges(v));
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            IEnumerable<TEdge> result;
            if (!this.tryGetOutEdges(v, out result))
                return Enumerable.Empty<TEdge>();
            return result;
        }

        public bool TryGetOutEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            return this.tryGetOutEdges(v, out edges);
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            return Enumerable.ElementAt(this.OutEdges(v), index);
        }

        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return true; }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            IEnumerable<TEdge> edges;
            return
                this.tryGetOutEdges(vertex, out edges);
        }
    }
}
