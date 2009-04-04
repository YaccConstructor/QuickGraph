using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
    public static class EdgeExtensions
    {
        /// <summary>
        /// Gets a value indicating if the edge is a self edge.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
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
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="edge">must not be a self-edge</param>
        /// <param name="source"></param>
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
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
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
            Contract.Requires(Contract.ForAll(path, e => e != null));

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
            Contract.Requires(Contract.ForAll(path, e => e != null));

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
            Contract.Requires(Contract.ForAll(path, e => e != null));
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
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static VertexPair<TVertex> ToVertexPair<TVertex, TEdge>(
#if !NET20
this 
#endif            
            TEdge edge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Ensures(Contract.Result<VertexPair<TVertex>>().Source.Equals(edge.Source));
            Contract.Ensures(Contract.Result<VertexPair<TVertex>>().Target.Equals(edge.Target));

            return new VertexPair<TVertex>(edge.Source, edge.Target);
        }

        /// <summary>
        /// Checks that <paramref name="root"/> is a predecessor of <paramref name="vertex"/>
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="predecessors"></param>
        /// <param name="root"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static bool IsPredecessor<TVertex, TEdge>(
#if !NET20
this 
#endif            
            IDictionary<TVertex, TEdge> predecessors, TVertex root, TVertex vertex)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(predecessors != null);
            Contract.Requires(root != null);
            Contract.Requires(vertex != null);

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
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
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

            var path = new List<TEdge>();

            TVertex vc = v;
            TEdge e;
            while (predecessors.TryGetValue(vc, out e))
            {
                path.Add(e);
                vc = GetOtherVertex(e, vc);
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
    }
}
