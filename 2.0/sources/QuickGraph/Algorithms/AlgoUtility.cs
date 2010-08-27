using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms
{
    public static class AlgoUtility
    {
        public static IDictionary<TEdge, double> ConstantCapacities<TVertex, TEdge>(
            IEdgeSet<TVertex, TEdge> g, double value)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");

            var capacities = new Dictionary<TEdge, double>(g.EdgeCount);
            foreach (var e in g.Edges)
                capacities.Add(e, value);
            return capacities;
        }

        public static IEnumerable<TVertex> Sinks<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> visitedGraph) 
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.IsOutEdgesEmpty(v))
                    yield return v;
        }

        public static IEnumerable<TVertex> Roots<TVertex, TEdge>(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.IsInEdgesEmpty(v))
                    yield return v;
        }

        public static IEnumerable<TVertex> IsolatedVertices<TVertex, TEdge>(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            foreach (var v in visitedGraph.Vertices)
                if (visitedGraph.Degree(v) == 0)
                    yield return v;
        }

        public static IEnumerable<TVertex> Roots<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
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

        public static IEnumerable<TVertex> Roots<TVertex,TEdge>(
            IVertexListGraph<TVertex,TEdge> visitedGraph)
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

        public static ICollection<TVertex> TopologicalSort<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void TopologicalSort<TVertex, TEdge>(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices
            )
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

            var topo = new UndirectedTopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static ICollection<TVertex> TopologicalSort<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void TopologicalSort<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

            var topo = new TopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static ICollection<TVertex> SourceFirstTopologicalSort<TVertex, TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");

            var vertices = new List<TVertex>(visitedGraph.VertexCount);
            SourceFirstTopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void SourceFirstTopologicalSort<TVertex, TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IList<TVertex> vertices)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(visitedGraph, "visitedGraph");
            GraphContracts.AssumeNotNull(vertices, "vertices");

            var topo = new SourceFirstTopologicalSortAlgorithm<TVertex, TEdge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static int ConnectedComponents<TVertex,TEdge>(
            IUndirectedGraph<TVertex,TEdge> g,
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

        public static int WeaklyConnectedComponents<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(components, "components");

            var conn = new WeaklyConnectedComponentsAlgorithm<TVertex, TEdge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        public static int StronglyConnectedComponents<TVertex, TEdge>(
            IVertexListGraph<TVertex, TEdge> g,
            IDictionary<TVertex, int> components)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(components, "components");

            var conn = new StronglyConnectedComponentsAlgorithm<TVertex, TEdge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        public static void Clone<TVertex,TEdge>(
            IVertexAndEdgeListGraph<TVertex, TEdge> g,
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> clone)
            where TVertex : ICloneable
            where TEdge : ICloneableEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");
            GraphContracts.AssumeNotNull(clone, "clone");

            var vertexClones = new Dictionary<TVertex, TVertex>();

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

        public static IMutableBidirectionalGraph<TGraph,CondensatedEdge<TVertex, TEdge,TGraph>> Condensate<TVertex, TEdge, TGraph>(
            IVertexAndEdgeListGraph<TVertex,TEdge> g)
            where TEdge : IEdge<TVertex>
            where TGraph : IMutableVertexAndEdgeListGraph<TVertex,TEdge>, new()
        {
            if (g == null)
                throw new ArgumentNullException("g");

            var condensator = new CondensationGraphAlgorithm<TVertex, TEdge, TGraph>(g);
            condensator.Compute();
            return condensator.CondensatedGraph;
        }

        /// <summary>
        /// Create a collection of odd vertices
        /// </summary>
        /// <param name="g">graph to visit</param>
        /// <returns>colleciton of odd vertices</returns>
        /// <exception cref="ArgumentNullException">g is a null reference</exception>
        public static List<TVertex> OddVertices<TVertex,TEdge>(IVertexAndEdgeListGraph<TVertex,TEdge> g)
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

        public static bool IsDirectedAcyclicGraph<TVertex, TEdge>(IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            GraphContracts.AssumeNotNull(g, "g");

            return new DagTester().IsDag(g);
        }

        private sealed class DagTester
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
