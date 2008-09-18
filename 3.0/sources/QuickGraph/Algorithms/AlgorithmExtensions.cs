using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Algorithms
{
    public static class AlgorithmExtensions
    {
        public static IEnumerable<TEdge> ShortestPath<TVertex, TEdge>(
            this IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeight,
            TVertex source,
            TVertex target
            )
            where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (edgeWeight == null)
                throw new ArgumentNullException("edgeWeight");
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            var algorithm = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(visitedGraph, edgeWeight);
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using (ObserverScope.Create(algorithm, predecessors))
                algorithm.Compute(source);

            return predecessors.Path(target);
        }

        /// <summary>
        /// Gets the list of sink vertices
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="visitedGraph"></param>
        /// <returns></returns>
        public static IEnumerable<TVertex> Sinks<TVertex, TEdge>(
            this IVertexListGraph<TVertex, TEdge> visitedGraph) 
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IVertexListGraph<TVertex,TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices
            )
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

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
            this IVertexListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

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
            this IVertexListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

            var topo = new TopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static IEnumerable<TVertex> SourceFirstTopologicalSort<TVertex, TEdge>(
            this IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            SourceFirstTopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void SourceFirstTopologicalSort<TVertex, TEdge>(
            this IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

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
            this IUndirectedGraph<TVertex,TEdge> g,
            TVertex startVertex,
            IDictionary<TVertex,int> components)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.Assume(g.ContainsVertex(startVertex), "g.ContainsVertex(startVertex)"); 
            GraphContracts.AssumeNotNull(components, "components");

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
            this IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(components, "components");

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
            this IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(components, "components");

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
            this IVertexAndEdgeListGraph<TVertex, TEdge> g,
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> clone)
            where TVertex : ICloneable
            where TEdge : ICloneableEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(clone, "clone");

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
            this IVertexAndEdgeListGraph<TVertex,TEdge> g
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
                this IBidirectionalGraph<TVertex, TEdge> visitedGraph,
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
            this IVertexAndEdgeListGraph<TVertex,TEdge> g)
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
            this IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");

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
            GraphContracts.AssumeNotNull(predecessors, "predecessors");
            GraphContracts.AssumeNotNull(edgeCosts, "edgeCosts");

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
