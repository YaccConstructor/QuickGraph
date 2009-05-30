using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A functional implicit graph
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
//    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class FunctionalImplicitGraph<TVertex, TEdge>
        : IImplicitGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges;
        readonly bool allowParallelEdges;

        public FunctionalImplicitGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges,
            bool allowParallelEdges)
        {
            Contract.Requires(tryGetOutEdges != null);

            this.tryGetOutEdges = tryGetOutEdges;
            this.allowParallelEdges = allowParallelEdges;
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
            if(!this.tryGetOutEdges(v, out result))
                throw new ArgumentOutOfRangeException("v");
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
            get { return this.allowParallelEdges; }
        }

        public bool ContainsVertex(TVertex vertex)
        {
            IEnumerable<TEdge> edges;
            return
                this.tryGetOutEdges(vertex, out edges);
        }
    }
}
