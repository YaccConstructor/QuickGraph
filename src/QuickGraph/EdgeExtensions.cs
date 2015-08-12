using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;
using System.Linq;

namespace QuickGraph
{
    public static class EdgeExtensions
    {
        public const string DebuggerDisplayEdgeFormatString = "{Source}->{Target}";
        public const string DebuggerDisplayTaggedEdgeFormatString = "{Source}->{Target}:{Tag}";
        public const string DebuggerDisplayUndirectedEdgeFormatString = "{Source}<->{Target}";
        public const string DebuggerDisplayTaggedUndirectedEdgeFormatString = "{Source}<->{Target}:{Tag}";
        public const string EdgeFormatString = "{0}->{1}";
        public const string TaggedEdgeFormatString = "{0}->{1}:{2}";
        public const string UndirectedEdgeFormatString = "{0}<->{1}";
        public const string TaggedUndirectedEdgeFormatString = "{0}<->{1}:{2}";

        /// <summary>
        /// Gets a value indicating if the edge is a self edge.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsSelfEdge<TVertex, TEdge>(
#if !NET20
this 
#endif
            TEdge edge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Ensures(Contract.Result<bool>() == (edge.Source.Equals(edge.Target)));

            return edge.Source.Equals(edge.Target);
        }

        /// <summary>
        /// Given a source vertex, returns the other vertex in the edge
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge">must not be a self-edge</param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        [Pure]
        public static TVertex GetOtherVertex<TVertex, TEdge>(
#if !NET20
this 
#endif
            TEdge edge, TVertex vertex)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Requires(vertex != null);
            Contract.Requires(!edge.Source.Equals(edge.Target));
            Contract.Requires(edge.Source.Equals(vertex) || edge.Target.Equals(vertex));
            Contract.Ensures(Contract.Result<TVertex>() != null);
            Contract.Ensures(Contract.Result<TVertex>().Equals(edge.Source.Equals(vertex) ? edge.Target : edge.Source));
            
            return edge.Source.Equals(vertex) ? edge.Target : edge.Source;
        }

        /// <summary>
        /// Gets a value indicating if <paramref name="vertex"/> is adjacent to <paramref name="edge"/>
        /// (is the source or target).
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsAdjacent<TVertex, TEdge>(
#if !NET20
this 
#endif
            TEdge edge, TVertex vertex)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Requires(vertex != null);
            //Contract.Ensures(Contract.Result<bool>() ==
            //    (edge.Source.Equals(vertex) || edge.Target.Equals(vertex))
            //    );

            return edge.Source.Equals(vertex) 
                || edge.Target.Equals(vertex);
        }

        [Pure]
        public static bool IsPath<TVertex, TEdge>(
#if !NET20
this 
#endif            
            IEnumerable<TEdge> path)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(path != null);
            Contract.Requires(typeof(TEdge).IsValueType || Enumerable.All(path, e => e != null));

            bool first = true;
            TVertex lastTarget = default(TVertex);
            foreach (var edge in path)
            {
                if (first)
                {
                    lastTarget = edge.Target;
                    first = false;
                }
                else
                {
                    if (!lastTarget.Equals(edge.Source))
                        return false;
                    lastTarget = edge.Target;
                }
            }

            return true;
        }

        [Pure]
        public static bool HasCycles<TVertex, TEdge>(
#if !NET20
this 
#endif            
            IEnumerable<TEdge> path)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(path != null);
            Contract.Requires(typeof(TEdge).IsValueType || Enumerable.All(path, e => e != null));

            var vertices = new Dictionary<TVertex, int>();
            bool first = true;
            foreach (var edge in path)
            {
                if (first)
                {
                    if (edge.Source.Equals(edge.Target)) // self-edge
                        return true;
                    vertices.Add(edge.Source, 0);
                    vertices.Add(edge.Target, 0);
                    first = false;
                }
                else
                {
                    if (vertices.ContainsKey(edge.Target))
                        return true;
                    vertices.Add(edge.Target, 0);
                }
            }

            return false;
        }

        [Pure]
        public static bool IsPathWithoutCycles<TVertex, TEdge>(
#if !NET20
this 
#endif            
            IEnumerable<TEdge> path)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(path != null);
            Contract.Requires(typeof(TEdge).IsValueType || Enumerable.All(path, e => e != null));
            Contract.Requires(IsPath<TVertex, TEdge>(path));

            var vertices = new Dictionary<TVertex, int>();
            bool first = true;
            TVertex lastTarget = default(TVertex);
            foreach (var edge in path)
            {
                if (first)
                {
                    lastTarget = edge.Target;
                    if (IsSelfEdge<TVertex, TEdge>(edge)) 
                        return false;
                    vertices.Add(edge.Source, 0);
                    vertices.Add(lastTarget, 0);
                    first = false;
                }
                else
                {
                    if (!lastTarget.Equals(edge.Source))
                        return false;
                    if (vertices.ContainsKey(edge.Target))
                        return false;

                    lastTarget = edge.Target;
                    vertices.Add(edge.Target, 0);
                }
            }

            return true;
        }

        /// <summary>
        /// Creates a vertex pair (source, target) from the edge
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static SEquatableEdge<TVertex> ToVertexPair<TVertex, TEdge>(
#if !NET20
this 
#endif            
            TEdge edge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Ensures(Contract.Result<SEquatableEdge<TVertex>>().Source.Equals(edge.Source));
            Contract.Ensures(Contract.Result<SEquatableEdge<TVertex>>().Target.Equals(edge.Target));

            return new SEquatableEdge<TVertex>(edge.Source, edge.Target);
        }

        /// <summary>
        /// Checks that <paramref name="root"/> is a predecessor of <paramref name="vertex"/>
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="predecessors"></param>
        /// <param name="root"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static bool IsPredecessor<TVertex, TEdge>(
#if !NET20
this 
#endif            
            IDictionary<TVertex, TEdge> predecessors, 
            TVertex root, 
            TVertex vertex)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(predecessors != null);
            Contract.Requires(root != null);
            Contract.Requires(vertex != null);
            Contract.Requires(
                typeof(TEdge).IsValueType || 
                Enumerable.All(predecessors.Values, e => e != null));

            var current = vertex;
            if (root.Equals(current)) 
                return true;

            TEdge predecessor;
            while(predecessors.TryGetValue(current, out predecessor))
            {
                var source = GetOtherVertex(predecessor, current);
                if (current.Equals(source))
                    return false;
                if (source.Equals(root))
                    return true;
                current = source;
            }

            return false;
        }

        /// <summary>
        /// Tries to get the predecessor path, if reachable.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="predecessors"></param>
        /// <param name="v"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetPath<TVertex, TEdge>(
#if !NET20
this 
#endif
            IDictionary<TVertex, TEdge> predecessors,
            TVertex v,
            out IEnumerable<TEdge> result)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(predecessors != null);
            Contract.Requires(v != null);
            Contract.Requires(
                typeof(TEdge).IsValueType ||
                Enumerable.All(predecessors.Values, e => e != null));
            Contract.Ensures(
                !Contract.Result<bool>() ||
                (Contract.ValueAtReturn<IEnumerable<TEdge>>(out result) != null &&
                 (typeof(TEdge).IsValueType ||
                 Enumerable.All(
                    Contract.ValueAtReturn<IEnumerable<TEdge>>(out result),
                    e => e != null))
                )
            );

            var path = new List<TEdge>();

            TVertex vc = v;
            TEdge edge;
            while (predecessors.TryGetValue(vc, out edge))
            {
                path.Add(edge);
                vc = GetOtherVertex(edge, vc);
            }

            if (path.Count > 0)
            {
                path.Reverse();
                result = path.ToArray();
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Returns the most efficient comporer for the particular type of TEdge.
        /// If TEdge implements IUndirectedEdge, then only the (source,target) pair
        /// has to be compared; if not, (source, target) and (target, source) have to be compared.
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <returns></returns>
        public static EdgeEqualityComparer<TVertex, TEdge> GetUndirectedVertexEquality<TVertex, TEdge>()
            where TEdge : IEdge<TVertex>
        {
            if (typeof(IUndirectedEdge<TVertex>).IsAssignableFrom(typeof(TEdge)))
                return new EdgeEqualityComparer<TVertex, TEdge>(SortedVertexEquality<TVertex, TEdge>);
            else
                return new EdgeEqualityComparer<TVertex, TEdge>(UndirectedVertexEquality<TVertex, TEdge>);
        }

        /// <summary>
        /// Gets a value indicating if the vertices of edge match (source, target) or
        /// (target, source)
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool UndirectedVertexEquality<TVertex, TEdge>(
#if !NET20
this 
#endif
            TEdge edge,
            TVertex source,
            TVertex target)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Requires(source != null);
            Contract.Requires(target != null);

            return (edge.Source.Equals(source) && edge.Target.Equals(target)) ||
                (edge.Target.Equals(source) && edge.Source.Equals(target));
        }

        /// <summary>
        /// Gets a value indicating if the vertices of edge match (source, target)
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        /// <typeparam name="TEdge">type of the edges</typeparam>
        /// <param name="edge"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SortedVertexEquality<TVertex, TEdge>(
#if !NET20
this 
#endif
TEdge edge,
            TVertex source,
            TVertex target)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(Comparer<TVertex>.Default.Compare(source, target) <= 0);

            return edge.Source.Equals(source) && edge.Target.Equals(target);
        }

        /// <summary>
        /// Returns a reversed edge enumeration
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static IEnumerable<SReversedEdge<TVertex, TEdge>> ReverseEdges<TVertex, TEdge>(IEnumerable<TEdge> edges)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(Enumerable.All(edges, e => e != null));
            Contract.Ensures(Contract.Result<IEnumerable<SReversedEdge<TVertex, TEdge>>>() != null);

            foreach (var edge in edges)
                yield return new SReversedEdge<TVertex, TEdge>(edge);
        }

    }
}
