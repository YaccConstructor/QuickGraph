using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class DelegateIncidenceGraph<TVertex, TEdge>
        : DelegateImplicitGraph<TVertex, TEdge>
        , IIncidenceGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>, IEquatable<TEdge>
    {
        public DelegateIncidenceGraph(
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
            :base(tryGetOutEdges) {}

        public bool ContainsEdge(TVertex source, TVertex target)
        {
            TEdge edge;
            return this.TryGetEdge(source, target, out edge);
        }

        public bool TryGetEdges(TVertex source, TVertex target, out IEnumerable<TEdge> edges)
        {
            IEnumerable<TEdge> outEdges;
            List<TEdge> result = null;
            if (this.TryGetOutEdges(source, out outEdges))
                foreach (var e in outEdges)
                    if (e.Target.Equals(target))
                    {
                        if (result == null)
                            result = new List<TEdge>();
                        result.Add(e);
                    }

            edges = result == null ? null : result.ToArray();
            return edges != null;
        }

        public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge)
        {
            IEnumerable<TEdge> edges;
            if (this.TryGetOutEdges(source, out edges))
                foreach (var e in edges)
                    if (e.Target.Equals(target))
                    {
                        edge = e;
                        return true;
                    }

            edge = default(TEdge);
            return false;
        }
    }
}
