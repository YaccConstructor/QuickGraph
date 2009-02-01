using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;
using QuickGraph.Algorithms.RandomWalks;
using QuickGraph.Collections;
using System.Linq;
using QuickGraph.Algorithms.MinimumSpanningTree;
using QuickGraph.Algorithms.RankedShortestPath;
using System.Reflection;
using QuickGraph.Algorithms.ConnectedComponents;
using System.Diagnostics;

namespace QuickGraph.Algorithms
{
    public static class AlgorithmExtensions
    {
        /// <summary>
        /// Returns the method that implement the access indexer.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static Func<TKey, TValue> GetIndexer<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            Contract.Requires(dictionary != null);
            Contract.Ensures(Contract.Result<Func<TKey, TValue>>() != null);

            var method = dictionary.GetType().GetProperty("Item").GetGetMethod();
            return (Func<TKey, TValue>)Delegate.CreateDelegate(typeof(Func<TKey, TValue>), dictionary, method, true);
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> TreeBreadthFirstSearch<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            TVertex root)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(root != null);
            Contract.Requires(visitedGraph.ContainsVertex(root));
            Contract.Ensures(Contract.Result<TryFunc<TVertex, IEnumerable<TEdge>>>() != null);

            var algo = new BreadthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algo, predecessorRecorder))
                algo.Compute(root);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> TreeDepthFirstSearch<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            TVertex root)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(root != null);
            Contract.Requires(visitedGraph.ContainsVertex(root));
            Contract.Ensures(Contract.Result<TryFunc<TVertex, IEnumerable<TEdge>>>() != null);

            var algo = new DepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algo, predecessorRecorder))
                algo.Compute(root);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> TreeCyclePoppingRandom<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            TVertex root)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(root != null);
            Contract.Requires(visitedGraph.ContainsVertex(root));
            Contract.Ensures(Contract.Result<TryFunc<TVertex, IEnumerable<TEdge>>>() != null);

            return TreeCyclePoppingRandom(visitedGraph, root, new NormalizedMarkovEdgeChain<TVertex, TEdge>());
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> TreeCyclePoppingRandom<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            TVertex root,
            IMarkovEdgeChain<TVertex, TEdge> edgeChain)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(root != null);
            Contract.Requires(visitedGraph.ContainsVertex(root));
            Contract.Ensures(Contract.Result<TryFunc<TVertex, IEnumerable<TEdge>>>() != null);

            var algo = new CyclePoppingRandomTreeAlgorithm<TVertex, TEdge>(visitedGraph, edgeChain);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using(ObserverScope.Create(algo, predecessorRecorder))
                algo.Compute(root);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        #region shortest paths
        public static TryFunc<TVertex, IEnumerable<TEdge>> ShortestPathsDijkstra<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            TVertex source
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(source != null);

            var algorithm = new UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights);
            var predecessorRecorder = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> ShortestPathsAStar<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            Func<TVertex, double> costHeuristic,
            TVertex source
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(costHeuristic != null);
            Contract.Requires(source != null);

            var algorithm = new AStarShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights, costHeuristic);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> ShortestPathsDijkstra<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            TVertex source
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(source != null);

            var algorithm = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> ShortestPathsBellmanFord<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            TVertex source
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(source != null);

            var algorithm = new BellmanFordShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        public static TryFunc<TVertex, IEnumerable<TEdge>> ShortestPathsDag<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            TVertex source
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(source != null);

            var algorithm = new DagShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights);
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return EdgeExtensions.TryGetPath(predecessors, v, out edges);
            };
        }

        #endregion

        #region K-Shortest path
        /// <summary>
        /// Computes the k-shortest path from <paramref name="source"/>
        /// <paramref name="target"/> using Hoffman-Pavley algorithm.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="edgeWeights"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="pathCount"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<TEdge>> RankedShortestPathHoffmanPavley<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            TVertex source,
            TVertex target,
            int pathCount)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeWeights != null);
            Contract.Requires(source != null && visitedGraph.ContainsVertex(source));
            Contract.Requires(target != null && visitedGraph.ContainsVertex(target));
            Contract.Requires(pathCount > 1);

            var algo = new HoffmanPavleyRankedShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeights);
            algo.ShortestPathCount = pathCount;
            algo.Compute(source, target);

            return algo.ComputedShortestPaths;
        }

        #endregion

        /// <summary>
        /// Gets the list of sink vertices
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> Sinks<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph) 
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            return SinksIterator<TVertex, TEdge>(visitedGraph);
        }

        [DebuggerHidden]
        private static IEnumerable<TVertex> SinksIterator<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.IsOutEdgesEmpty(v))
                    yield return v;
        }


        /// <summary>
        /// Gets the list of root vertices
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> Roots<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            return RootsIterator(visitedGraph);
        }

        [DebuggerHidden]
        private static IEnumerable<TVertex> RootsIterator<TVertex, TEdge>(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.IsInEdgesEmpty(v))
                    yield return v;
        }

        /// <summary>
        /// Gets the list of isolated vertices (no incoming or outcoming vertices)
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> IsolatedVertices<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            return IsolatedVerticesIterator(visitedGraph);
        }

        [DebuggerHidden]
        private static IEnumerable<TVertex> IsolatedVerticesIterator<TVertex, TEdge>(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.Degree(v) == 0)
                    yield return v;
        }

        /// <summary>
        /// Gets the list of root  vertices
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> Roots<TVertex, TEdge>(
#if !NET20
this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            return RootsIterator<TVertex, TEdge>(visitedGraph);
        }

        [DebuggerHidden]
        private static IEnumerable<TVertex> RootsIterator<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            var dfs = new UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var vis = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            using(ObserverScope.Create(dfs, vis))
                dfs.Compute();

            foreach (var predecessor in vis.VertexPredecessors)
            {
                if (predecessor.Value.Equals(default(TEdge)))
                    yield return predecessor.Key;
            }
        }

        /// <summary>
        /// Gets the list of roots
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> Roots<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            return RootsIterator<TVertex, TEdge>(visitedGraph);
        }

        [DebuggerHidden]
        private static IEnumerable<TVertex> RootsIterator<TVertex,TEdge>(
            IVertexListGraph<TVertex,TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var vis = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(dfs, vis))
                dfs.Compute();

            foreach (var predecessor in vis.VertexPredecessors)
            {
                if (object.Equals(predecessor.Value, default(TEdge)))
                    yield return predecessor.Key;
            }
        }

        /// <summary>
        /// Creates a topological sort of a undirected
        /// acyclic graph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        /// <exception cref="NonAcyclicGraphException">the input graph
        /// has a cycle</exception>
        public static IEnumerable<TVertex> TopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }


        /// <summary>
        /// Creates a topological sort of a undirected
        /// acyclic graph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        /// <exception cref="NonAcyclicGraphException">the input graph
        /// has a cycle</exception>
        public static void TopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertices != null);

            var topo = new UndirectedTopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        /// <summary>
        /// Creates a topological sort of a directed
        /// acyclic graph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        /// <exception cref="NonAcyclicGraphException">the input graph
        /// has a cycle</exception>
        public static IEnumerable<TVertex> TopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        /// <summary>
        /// Creates a topological sort of a directed
        /// acyclic graph.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        /// <exception cref="NonAcyclicGraphException">the input graph
        /// has a cycle</exception>
        public static void TopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertices != null);

            var topo = new TopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static IEnumerable<TVertex> SourceFirstTopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            SourceFirstTopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void SourceFirstTopologicalSort<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(vertices != null);

            var topo = new SourceFirstTopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        /// <summary>
        /// Computes the connected components of a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <param name="startVertex"></param>
        /// <param name="components"></param>
        /// <returns>number of components</returns>
        public static int ConnectedComponents<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IUndirectedGraph<TVertex, TEdge> g,
            TVertex startVertex,
            IDictionary<TVertex,int> components)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);
            Contract.Requires(components != null);
            Contract.Requires(GraphContract.InVertexSet(g, startVertex));

            var conn = new ConnectedComponentsAlgorithm<TVertex,TEdge>(g, components);
            conn.Compute(startVertex);
            return conn.ComponentCount;
        }

        /// <summary>
        /// Computes the incremental connected components for a growing graph (edge added only).
        /// Each call to the delegate re-computes the component dictionary. The returned dictionary
        /// is shared accross multiple calls of the method.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Func<KeyValuePair<int, IDictionary<TVertex, int>>> IncrementalConnectedComponents<TVertex, TEdge>(
#if !NET20
this 
#endif
            IMutableVertexAndEdgeSet<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);

            var incrementalComponents = new IncrementalConnectedComponentsAlgorithm<TVertex, TEdge>(g);
            incrementalComponents.Compute();

            return () => incrementalComponents.GetComponents();
        }

        /// <summary>
        /// Computes the weakly connected components of a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <param name="components"></param>
        /// <returns>number of components</returns>
        public static int WeaklyConnectedComponents<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);
            Contract.Requires(components != null);

            var conn = new WeaklyConnectedComponentsAlgorithm<TVertex, TEdge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        /// <summary>
        /// Computes the strongly connected components of a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <param name="components"></param>
        /// <returns>number of components</returns>
        public static int StronglyConnectedComponents<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> g,
            out IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);
            Contract.Ensures(Contract.ValueAtReturn(out components) != null);

            components = new Dictionary<TVertex, int>();
            var conn = new StronglyConnectedComponentsAlgorithm<TVertex, TEdge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        /// <summary>
        /// Clones a graph to another graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <param name="clone"></param>
        public static void Clone<TVertex,TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> g,
            IMutableVertexAndEdgeSet<TVertex, TEdge> clone)
            where TVertex : ICloneable
            where TEdge : ICloneableEdge<TVertex>
        {
            Contract.Requires(g != null);
            Contract.Requires(clone != null);

            var vertexClones = new Dictionary<TVertex, TVertex>(g.VertexCount);
            foreach (var v in g.Vertices)
            {
                var vc = (TVertex)v.Clone();
                clone.AddVertex(vc);
                vertexClones.Add(v, vc);
            }

            foreach (var edge in g.Edges)
            {
                var ec = (TEdge)edge.Clone(
                    vertexClones[edge.Source],
                    vertexClones[edge.Target]);
                clone.AddEdge(ec);
            }
        }

        /// <summary>
        /// Condensates the strongly connected components of a directed graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static IMutableBidirectionalGraph<TGraph,CondensatedEdge<TVertex, TEdge,TGraph>> 
            CondensateStronglyConnected<TVertex, TEdge, TGraph>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex,TEdge> g
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeSet<TVertex,TEdge>, new()
        {
            Contract.Requires(g != null);

            var condensator = new CondensationGraphAlgorithm<TVertex, TEdge, TGraph>(g);
            condensator.Compute();
            return condensator.CondensatedGraph;
        }

        /// <summary>
        /// Condensates the weakly connected components of a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static IMutableBidirectionalGraph<TGraph, CondensatedEdge<TVertex, TEdge, TGraph>>
            CondensateWeaklyConnected<TVertex, TEdge, TGraph>(
#if !NET20
this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> g
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeSet<TVertex, TEdge>, new()
        {
            Contract.Requires(g != null);

            var condensator = new CondensationGraphAlgorithm<TVertex, TEdge, TGraph>(g);
            condensator.StronglyConnected = false;
            condensator.Compute();
            return condensator.CondensatedGraph;
        }

        public static IMutableBidirectionalGraph<TVertex, MergedEdge<TVertex, TEdge>>
            CondensateEdges<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
            VertexPredicate<TVertex> vertexPredicate
            ) where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (vertexPredicate == null)
                throw new ArgumentNullException("vertexPredicate");

            var condensated = new BidirectionalGraph<TVertex, MergedEdge<TVertex, TEdge>>();
            var condensator = new EdgeMergeCondensationGraphAlgorithm<TVertex, TEdge>(
                visitedGraph,
                condensated,
                vertexPredicate);
            condensator.Compute();

            return condensated;
        }


        /// <summary>
        /// Create a collection of odd vertices
        /// </summary>
        /// <param name="g">graph to visit</param>
        /// <returns>colleciton of odd vertices</returns>
        /// <exception cref="ArgumentNullException">g is a null reference</exception>
        public static List<TVertex> OddVertices<TVertex,TEdge>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");

            var counts = new Dictionary<TVertex,int>(g.VertexCount);
            foreach (var v in g.Vertices)
                counts.Add(v,0);

            foreach (var e in g.Edges)
            {
                ++counts[e.Source];
                --counts[e.Target];
            }

            var odds = new List<TVertex>();
            foreach (var de in counts)
            {
                if (de.Value % 2 != 0)
                    odds.Add(de.Key);
            }

            return odds;
        }

        /// <summary>
        /// Gets a value indicating whether the graph is acyclic
        /// </summary>
        /// <remarks>
        /// Performs a depth first search to look for cycles.
        /// </remarks>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool IsDirectedAcyclicGraph<TVertex, TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);

            return new DagTester<TVertex,TEdge>().IsDag(g);
        }

        class DagTester<TVertex,TEdge>
                where TEdge : IEdge<TVertex>
        {
            private bool isDag = true;

            public bool IsDag(IVertexListGraph<TVertex, TEdge> g)
            {
                var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(g);
                try
                {
                    dfs.BackEdge += new EdgeAction<TVertex, TEdge>(dfs_BackEdge);
                    isDag = true;
                    dfs.Compute();
                    return isDag;
                }
                finally
                {
                    dfs.BackEdge -= new EdgeAction<TVertex, TEdge>(dfs_BackEdge);
                }
            }

            void dfs_BackEdge(TEdge e)
            {
                isDag = false;
            }
        }

        /// <summary>
        /// Given a edge cost map, computes 
        /// the predecessor cost.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="predecessors"></param>
        /// <param name="edgeCosts"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double ComputePredecessorCost<TVertex, TEdge>(
            IDictionary<TVertex, TEdge> predecessors,         
            IDictionary<TEdge, double> edgeCosts,
            TVertex target
            ) 
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(predecessors != null);
            Contract.Requires(edgeCosts != null);

            double cost = 0;
            TVertex current = target;
            TEdge edge;

            while (predecessors.TryGetValue(current, out edge)) {
                cost += edgeCosts[edge];
                current = edge.Source;
            }

            return cost;
        }

        public static IDisjointSet<TVertex> ComputeDisjointSet<TVertex, TEdge>(
#if !NET20
this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            var ds = new ForestDisjointSet<TVertex>(visitedGraph.VertexCount);
            foreach (var v in visitedGraph.Vertices)
                ds.MakeSet(v);
            foreach(var e in visitedGraph.Edges)
                ds.Union(e.Source, e.Target);

            return ds;
        }

        /// <summary>
        /// Computes the minimum spanning tree using Prim's algorithm.
        /// Prim's algorithm is simply implemented by calling Dijkstra shortest path.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static IEnumerable<TEdge> MinimumSpanningTreePrim<TVertex, TEdge>(
#if !NET20
this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(weights != null);

            if (visitedGraph.VertexCount == 0)
                return new TEdge[0];

            var distanceRelaxer = PrimRelaxer.Instance;
            var dijkstra = new UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, weights, distanceRelaxer);
            var edgeRecorder = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(dijkstra, edgeRecorder))
                dijkstra.Compute();

            return edgeRecorder.VertexPredecessors.Values;
        }

        class PrimRelaxer
            : IDistanceRelaxer
        {
            public static readonly IDistanceRelaxer Instance = new PrimRelaxer();

            public double InitialDistance
            {
                get { return double.MaxValue; }
            }

            public bool Compare(double a, double b)
            {
                return a < b;
            }

            public double Combine(double distance, double weight)
            {
                return weight;
            }
        }

        /// <summary>
        /// Computes the minimum spanning tree using Kruskal's algorithm.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static IEnumerable<TEdge> MinimumSpanningTreeKruskal<TVertex, TEdge>(
#if !NET20
this 
#endif
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(weights != null);

            if (visitedGraph.VertexCount == 0)
                return new TEdge[0];

            var kruskal = new KruskalMinimumSpanningTreeAlgorithm<TVertex, TEdge>(visitedGraph, weights);
            var edgeRecorder = new EdgeRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(kruskal, edgeRecorder))
                kruskal.Compute();

            return edgeRecorder.Edges;
        }

        /// <summary>
        /// Computes the offline least common ancestor between pairs of vertices in a rooted tree
        /// using Tarjan algorithm.
        /// </summary>
        /// <remarks>
        /// Reference:
        /// Gabow, H. N. and Tarjan, R. E. 1983. A linear-time algorithm for a special case of disjoint set union. In Proceedings of the Fifteenth Annual ACM Symposium on theory of Computing STOC '83. ACM, New York, NY, 246-251. DOI= http://doi.acm.org/10.1145/800061.808753 
        /// </remarks>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <param name="root"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static TryFunc<VertexPair<TVertex>, TVertex> OfflineLeastCommonAncestorTarjan<TVertex, TEdge>(
#if !NET20
this 
#endif
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            TVertex root,
            IEnumerable<VertexPair<TVertex>> pairs
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(root != null);
            Contract.Requires(pairs != null);
            Contract.Requires(visitedGraph.ContainsVertex(root));
            Contract.Requires(Contract.ForAll(pairs, p => visitedGraph.ContainsVertex(p.Source)));
            Contract.Requires(Contract.ForAll(pairs, p => visitedGraph.ContainsVertex(p.Target)));

            var algo = new TarjanOfflineLeastCommonAncestorAlgorithm<TVertex, TEdge>(visitedGraph);
            algo.Compute(root, pairs);
            var ancestors = algo.Ancestors;

            return delegate(VertexPair<TVertex> pair, out TVertex value)
            {
                return ancestors.TryGetValue(pair, out value);
            };
        }
    }
}
