using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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
            IEdge<TVertex> edge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);

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
            IEdge<TVertex> edge, TVertex vertex)
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
            IEdge<TVertex> edge, TVertex vertex)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);
            Contract.Requires(vertex != null);

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

            Dictionary<TVertex, int> vertices = new Dictionary<TVertex, int>();
            bool first = true;
            foreach (var edge in path)
            {
                if (first)
                {
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

            Dictionary<TVertex, int> vertices = new Dictionary<TVertex, int>();
            bool first = true;
            TVertex lastTarget = default(TVertex);
            foreach (var edge in path)
            {
                if (first)
                {
                    lastTarget = edge.Target;
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
    }
}
