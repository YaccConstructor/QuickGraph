using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;
using QuickGraph.Algorithms;
using System.Linq;

namespace QuickGraph
{
    /// <summary>
    /// Extension methods for populating graph datastructures
    /// </summary>
    public static class GraphExtensions
    {
        /// <summary>
        /// Wraps a dictionary into a vertex and edge list graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static DelegateVertexAndEdgeListGraph<TVertex, TEdge> ToVertexAndEdgeListGraph<TVertex, TEdge, TValue>(
#if !NET20
this 
#endif
            IDictionary<TVertex, TValue> dictionary)
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
            where TValue : IEnumerable<TEdge>
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(Contract.ForAll(dictionary.Values, v => v != null));

            return ToVertexAndEdgeListGraph<TVertex, TEdge, TValue>(dictionary, kv => kv.Value);
        }

        /// <summary>
        /// Wraps a dictionary into a vertex and edge list graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="keyValueToOutEdges"></param>
        /// <returns></returns>
        public static DelegateVertexAndEdgeListGraph<TVertex, TEdge> ToVertexAndEdgeListGraph<TVertex, TEdge, TValue>(
#if !NET20
this 
#endif
            IDictionary<TVertex, TValue> dictionary,
            Converter<KeyValuePair<TVertex,TValue>, IEnumerable<TEdge>> keyValueToOutEdges
            )
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(keyValueToOutEdges != null);

            return new DelegateVertexAndEdgeListGraph<TVertex, TEdge>(
                dictionary.Keys,
                delegate(TVertex key, out IEnumerable<TEdge> edges) {
                    TValue value;
                    if (dictionary.TryGetValue(key, out value))
                    {
                        edges = keyValueToOutEdges(new KeyValuePair<TVertex, TValue>(key, value));
                        return true;
                    }

                    edges = null;
                    return false;
                });
        }

        /// <summary>
        /// Creates an instance of DelegateIncidenceGraph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="tryGetOutEdges"></param>
        /// <returns></returns>
        public static DelegateIncidenceGraph<TVertex, TEdge> ToDelegateIncidenceGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
        {
            Contract.Requires(tryGetOutEdges != null);
            return new DelegateIncidenceGraph<TVertex, TEdge>(tryGetOutEdges);
        }

        /// <summary>
        /// Creates an instance of DelegateIncidenceGraph.
        /// </summary>
        /// <param name="vertices"></param>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="tryGetOutEdges"></param>
        /// <returns></returns>
        public static DelegateVertexAndEdgeListGraph<TVertex, TEdge> ToDelegateVertexAndEdgeListGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEnumerable<TVertex> vertices,
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetOutEdges)
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
        {
            Contract.Requires(vertices != null);
            Contract.Requires(tryGetOutEdges != null);
            Contract.Requires(Contract.ForAll(vertices, v =>
            {
                IEnumerable<TEdge> edges;
                return tryGetOutEdges(v, out edges);
            }));

            return new DelegateVertexAndEdgeListGraph<TVertex, TEdge>(vertices, tryGetOutEdges);
        }

        /// <summary>
        /// Wraps a dictionary into an undirected list graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static DelegateVertexAndEdgeListGraph<TVertex, TEdge> ToDelegateUndirectedGraph<TVertex, TEdge, TValue>(
#if !NET20
this 
#endif
            IDictionary<TVertex, TValue> dictionary)
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
            where TValue : IEnumerable<TEdge>
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(Contract.ForAll(dictionary.Values, v => v != null));

            return ToDelegateUndirectedGraph<TVertex, TEdge, TValue>(dictionary, kv => kv.Value);
        }

        /// <summary>
        /// Wraps a dictionary into an undirected graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="keyValueToOutEdges"></param>
        /// <returns></returns>
        public static DelegateVertexAndEdgeListGraph<TVertex, TEdge> ToDelegateUndirectedGraph<TVertex, TEdge, TValue>(
#if !NET20
this 
#endif
            IDictionary<TVertex, TValue> dictionary,
            Converter<KeyValuePair<TVertex, TValue>, IEnumerable<TEdge>> keyValueToOutEdges
            )
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(keyValueToOutEdges != null);

            return new DelegateVertexAndEdgeListGraph<TVertex, TEdge>(
                dictionary.Keys,
                delegate(TVertex key, out IEnumerable<TEdge> edges)
                {
                    TValue value;
                    if (dictionary.TryGetValue(key, out value))
                    {
                        edges = keyValueToOutEdges(new KeyValuePair<TVertex, TValue>(key, value));
                        return true;
                    }

                    edges = null;
                    return false;
                });
        }

        /// <summary>
        /// Creates an instance of DelegateIncidenceGraph.
        /// </summary>
        /// <param name="vertices"></param>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="tryGetAdjacentEdges"></param>
        /// <returns></returns>
        public static DelegateUndirectedGraph<TVertex, TEdge> ToDelegateUndirectedGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEnumerable<TVertex> vertices,
            TryFunc<TVertex, IEnumerable<TEdge>> tryGetAdjacentEdges)
            where TEdge : IEdge<TVertex>, IEquatable<TEdge>
        {
            Contract.Requires(vertices != null);
            Contract.Requires(tryGetAdjacentEdges != null);
            Contract.Requires(Contract.ForAll(vertices, v =>
            {
                IEnumerable<TEdge> edges;
                return tryGetAdjacentEdges(v, out edges);
            }));

            return new DelegateUndirectedGraph<TVertex, TEdge>(vertices, tryGetAdjacentEdges, true);
        }

        /// <summary>
        /// Creates an immutable array adjacency graph from the input graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static ArrayAdjacencyGraph<TVertex, TEdge> ToArrayAdjacencyGraph<TVertex, TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> graph
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);
            return new ArrayAdjacencyGraph<TVertex, TEdge>(graph);
        }

        /// <summary>
        /// Wraps a adjacency graph (out-edge only) into a bidirectional graph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static IBidirectionalGraph<TVertex, TEdge> ToBidirectionalGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> graph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);

            var self = graph as IBidirectionalGraph<TVertex, TEdge>;
            if (self != null)
                return self;

            return new BidirectionAdapterGraph<TVertex,TEdge>(graph);
        }

        /// <summary>
        /// Converts a sequence of edges into an undirected graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="edges"></param>
        /// <returns></returns>
        public static UndirectedGraph<TVertex, TEdge> ToUndirectedGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEnumerable<TEdge> edges)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(Contract.ForAll(edges, e => e != null));

            return ToUndirectedGraph<TVertex, TEdge>(edges, true);
        }

        /// <summary>
        /// Converts a sequence of edges into an undirected graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="edges"></param>
        /// <param name="allowParralelEdges"></param>
        /// <returns></returns>
        public static UndirectedGraph<TVertex, TEdge> ToUndirectedGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEnumerable<TEdge> edges,
            bool allowParralelEdges)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(Contract.ForAll(edges, e => e != null));

            var g = new UndirectedGraph<TVertex, TEdge>(allowParralelEdges);
            g.AddVerticesAndEdgeRange(edges);
            return g;
        }

        /// <summary>
        /// Converts a set of edges into a bidirectional graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="edges">The edges.</param>
        /// <param name="allowParallelEdges">if set to <c>true</c>, the graph allows parallel edges.</param>
        /// <returns></returns>
        public static BidirectionalGraph<TVertex, TEdge> ToBidirectionalGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TEdge> edges,
            bool allowParallelEdges
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(edges));

            var g = new BidirectionalGraph<TVertex, TEdge>(allowParallelEdges);
            g.AddVerticesAndEdgeRange(edges);
            return g;
        }

        /// <summary>
        /// Converts a set of edges into a bidirectional graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="edges">The edges.</param>
        /// <returns></returns>
        public static BidirectionalGraph<TVertex, TEdge> ToBidirectionalGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TEdge> edges
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(edges));

            return ToBidirectionalGraph<TVertex, TEdge>(edges, true);
        }

        /// <summary>
        /// Converts a set of edges into an adjacency graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="edges">The edges.</param>
        /// <param name="allowParallelEdges">if set to <c>true</c>, the graph allows parallel edges.</param>
        /// <returns></returns>
        public static AdjacencyGraph<TVertex, TEdge> ToAdjacencyGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TEdge> edges,
            bool allowParallelEdges
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(edges));

            var g = new AdjacencyGraph<TVertex, TEdge>(allowParallelEdges);
            g.AddVerticesAndEdgeRange(edges);
            return g;
        }

        /// <summary>
        /// Converts a set of edges into an adjacency graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="edges">The edges.</param>
        /// <returns></returns>
        public static AdjacencyGraph<TVertex, TEdge> ToAdjacencyGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TEdge> edges
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edges != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(edges));

            return ToAdjacencyGraph<TVertex, TEdge>(edges, true);
        }

        /// <summary>
        /// Converts a set of vertices into an adjacency graph,
        /// using an edge factory.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outEdgesFactory">The out edges factory.</param>
        /// <param name="allowParallelEdges">if set to <c>true</c>, the graph allows parallel edges.</param>
        /// <returns></returns>
        public static AdjacencyGraph<TVertex, TEdge> ToAdjacencyGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TVertex> vertices,
            Func<TVertex, IEnumerable<TEdge>> outEdgesFactory,
            bool allowParallelEdges
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(vertices != null);
            Contract.Requires(outEdgesFactory != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(vertices));

            var g = new AdjacencyGraph<TVertex, TEdge>(allowParallelEdges);
            g.AddVertexRange(vertices);
            foreach (var vertex in g.Vertices)
                g.AddEdgeRange(outEdgesFactory(vertex));

            return g;
        }

        /// <summary>
        /// Converts a set of ver.tices into an adjacency graph,
        /// using an edge factory.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outEdgesFactory">The out edges factory.</param>
        /// <returns></returns>
        public static AdjacencyGraph<TVertex, TEdge> ToAdjacencyGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TVertex> vertices,
            Func<TVertex, IEnumerable<TEdge>> outEdgesFactory
            )
            where TEdge : IEdge<TVertex>
        {
            return ToAdjacencyGraph(vertices, outEdgesFactory, true);
        }

        /// <summary>
        /// Converts a set of ver.tices into a bidirectional graph,
        /// using an edge factory.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outEdgesFactory">The out edges factory.</param>
        /// <param name="allowParallelEdges">if set to <c>true</c>, the graph allows parallel edges.</param>
        /// <returns></returns>
        public static BidirectionalGraph<TVertex, TEdge> ToBidirectionalGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TVertex> vertices,
            Func<TVertex, IEnumerable<TEdge>> outEdgesFactory,
            bool allowParallelEdges
            ) 
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(vertices != null);
            Contract.Requires(outEdgesFactory != null);
            Contract.Requires(EnumerableContract.ElementsNotNull(vertices));

            var g = new BidirectionalGraph<TVertex, TEdge>(allowParallelEdges);
            g.AddVertexRange(vertices);
            foreach (var vertex in g.Vertices)
                g.AddEdgeRange(outEdgesFactory(vertex));

            return g;
        }

        /// <summary>
        /// Converts a set of ver.tices into a bidirectional graph,
        /// using an edge factory
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="vertices">The vertices.</param>
        /// <param name="outEdgesFactory">The out edges factory.</param>
        /// <returns></returns>
        public static BidirectionalGraph<TVertex, TEdge> ToBidirectionalGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IEnumerable<TVertex> vertices,
            Func<TVertex, IEnumerable<TEdge>> outEdgesFactory
            )
            where TEdge : IEdge<TVertex>
        {
            return ToBidirectionalGraph(vertices, outEdgesFactory, true);
        }

        /// <summary>
        /// Converts a sequence of vertex pairs into an adjancency graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="vertexPairs"></param>
        /// <returns></returns>
        public static AdjacencyGraph<TVertex, SEquatableEdge<TVertex>> ToAdjacencyGraph<TVertex>(
#if !NET20
this 
#endif
            IEnumerable<SEquatableEdge<TVertex>> vertexPairs)
        {
            Contract.Requires(vertexPairs != null);

            var g = new AdjacencyGraph<TVertex, SEquatableEdge<TVertex>>();
            g.AddVerticesAndEdgeRange(vertexPairs);
            return g;
        }

        /// <summary>
        /// Converts a sequence of vertex pairs into an bidirectional graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="vertexPairs"></param>
        /// <returns></returns>
        public static BidirectionalGraph<TVertex, SEquatableEdge<TVertex>> ToBidirectionalGraph<TVertex>(
#if !NET20
this 
#endif
            IEnumerable<SEquatableEdge<TVertex>> vertexPairs)
        {
            Contract.Requires(vertexPairs != null);

            var g = new BidirectionalGraph<TVertex, SEquatableEdge<TVertex>>();
            g.AddVerticesAndEdgeRange(vertexPairs);
            return g;
        }

        /// <summary>
        /// Converts a sequence of vertex pairs into an bidirectional graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="vertexPairs"></param>
        /// <returns></returns>
        public static UndirectedGraph<TVertex, SEquatableEdge<TVertex>> ToUndirectedGraph<TVertex>(
#if !NET20
this 
#endif
            IEnumerable<SEquatableEdge<TVertex>> vertexPairs)
        {
            Contract.Requires(vertexPairs != null);

            var g = new UndirectedGraph<TVertex, SEquatableEdge<TVertex>>();
            g.AddVerticesAndEdgeRange(vertexPairs);
            return g;
        }

        /// <summary>
        /// Creates an immutable compressed row graph representation of the visited graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static CompressedSparseRowGraph<TVertex> ToCompressedRowGraph<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            return CompressedSparseRowGraph<TVertex>.FromGraph(visitedGraph);
        }
    }
}
