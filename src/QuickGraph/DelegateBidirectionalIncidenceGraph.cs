using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A delegate based bidirectional implicit graph
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DelegateBidirectionalIncidenceGraph<TVertex, TEdge>
        : DelegateIncidenceGraph<TVertex, TEdge>
        , IBidirectionalIncidenceGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>, IEquatable<TEdge>
    {
        readonly TryFunc<TVertex, IEnumerable<TEdge>> tryGetInEdges;

        public DelegateBidirectionalIncidenceGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges,
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetInEdges)
            :base(tryGetOutEdges)
        {
            Contract.Requires(tryGetInEdges != null);

            this.tryGetInEdges = tryGetInEdges;
        }

        public TryFunc<TVertex, IEnumerable<TEdge>> TryGetInEdgesFunc
        {
            get { return this.tryGetInEdges; }
        }

        #region IBidirectionalImplicitGraph<TVertex,TEdge> Members

        public bool IsInEdgesEmpty(TVertex v)
        {
            foreach (var edge in this.InEdges(v))
                return false;
            return true;
        }

        public int InDegree(TVertex v)
        {
            return Enumerable.Count(this.InEdges(v));
        }

        public IEnumerable<TEdge> InEdges(TVertex v)
        {
            IEnumerable<TEdge> result;
            if (!this.tryGetInEdges(v, out result))
                return Enumerable.Empty<TEdge>();
            return result;
        }

        public bool TryGetInEdges(TVertex v, out IEnumerable<TEdge> edges)
        {
            return this.tryGetInEdges(v, out edges);
        }

        public TEdge InEdge(TVertex v, int index)
        {
            return Enumerable.ElementAt(this.InEdges(v), index);
        }

        public int Degree(TVertex v)
        {
            return this.InDegree(v) + this.OutDegree(v);
        }
        #endregion
    }
}
