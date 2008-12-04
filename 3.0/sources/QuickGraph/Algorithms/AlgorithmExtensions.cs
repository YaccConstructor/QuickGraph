using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph.Algorithms
{
    public static class AlgorithmExtensions
    {
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
                vc = e.Source;
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
            var predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessorRecorder))
                algorithm.Compute(source);

            var predecessors = predecessorRecorder.VertexPredecessors;
            return delegate(TVertex v, out IEnumerable<TEdge> edges)
            {
                return AlgorithmExtensions.TryGetPath(predecessors, v, out edges);
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
                return AlgorithmExtensions.TryGetPath(predecessors, v, out edges);
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
                return AlgorithmExtensions.TryGetPath(predecessors, v, out edges);
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
                return AlgorithmExtensions.TryGetPath(predecessors, v, out edges);
            };
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

            var dfs = new UndirectedDepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var vis = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            vis.Attach(dfs);

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
        public static IEnumerable<TVertex> Roots<TVertex,TEdge>(
#if !NET20
            this 
#endif
            IVertexListGraph<TVertex,TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(visitedGraph != null);

            var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(visitedGraph);
            var vis = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            vis.Attach(dfs);

            foreach (var predecessor in vis.VertexPredecessors)
            {
                if (predecessor.Value.Equals(default(TEdge)))
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
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(g != null);
            Contract.Requires(components != null);

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
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> clone)
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
        /// Condensates a graph
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <typeparam name="TGraph"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static IMutableBidirectionalGraph<TGraph,CondensatedEdge<TVertex, TEdge,TGraph>> 
            Condensate<TVertex, TEdge, TGraph>(
#if !NET20
            this 
#endif
            IVertexAndEdgeListGraph<TVertex,TEdge> g
            )
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex,TEdge>, new()
        {
            if (g == null)
                throw new ArgumentNullException("g");

            var condensator = new CondensationGraphAlgorithm<TVertex, TEdge, TGraph>(g);
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

            return new DagTester().IsDag(g);
        }

        class DagTester
        {
            private bool isDag = true;

            public bool IsDag<TVertex,TEdge>(IVertexListGraph<TVertex, TEdge> g)
                where TEdge : IEdge<TVertex>
            {
                var dfs = new DepthFirstSearchAlgorithm<TVertex, TEdge>(g);
                try
                {
                    dfs.BackEdge += new EdgeEventHandler<TVertex, TEdge>(dfs_BackEdge);
                    isDag = true;
                    dfs.Compute();
                    return isDag;
                }
                finally
                {
                    dfs.BackEdge -= new EdgeEventHandler<TVertex, TEdge>(dfs_BackEdge);
                }
            }

            void dfs_BackEdge<Vertex,Edge>(object sender, EdgeEventArgs<Vertex, Edge> e)
                where Edge : IEdge<Vertex>
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
    }
}
