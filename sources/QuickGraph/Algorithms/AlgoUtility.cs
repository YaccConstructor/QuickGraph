using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Condensation;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public static class AlgoUtility
    {
        public static IDictionary<Edge, double> ConstantCapacities<Vertex, Edge>(IEdgeListGraph<Vertex, Edge> g, double value)
            where Edge : IEdge<Vertex>
        {
            Dictionary<Edge, double> capacities = new Dictionary<Edge, double>(g.EdgeCount);
            foreach (Edge e in g.Edges)
                capacities.Add(e, value);
            return capacities;
        }

        public static IEnumerable<Vertex> Sinks<Vertex, Edge>(
            IVertexListGraph<Vertex, Edge> visitedGraph) 
            where Edge : IEdge<Vertex>
        {
            foreach (Vertex v in visitedGraph.Vertices)
            {
                if (visitedGraph.IsOutEdgesEmpty(v))
                    yield return v;
            }
        }

        public static IEnumerable<Vertex> Roots<Vertex, Edge>(
            IBidirectionalGraph<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            foreach (Vertex v in visitedGraph.Vertices)
                if (visitedGraph.IsInEdgesEmpty(v))
                    yield return v;
        }

        public static IEnumerable<Vertex> IsolatedVertices<Vertex, Edge>(
            IBidirectionalGraph<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            foreach (Vertex v in visitedGraph.Vertices)
            {
                if (visitedGraph.Degree(v) == 0)
                    yield return v;
            }
        }

        public static IEnumerable<Vertex> Roots<Vertex,Edge>(
            IVertexListGraph<Vertex,Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            DepthFirstSearchAlgorithm<Vertex, Edge> dfs = new DepthFirstSearchAlgorithm<Vertex, Edge>(visitedGraph);
            VertexPredecessorRecorderObserver<Vertex, Edge> vis = new VertexPredecessorRecorderObserver<Vertex, Edge>();
            vis.Attach(dfs);

            foreach (KeyValuePair<Vertex, Edge> predecessor in vis.VertexPredecessors)
            {
                if (predecessor.Value.Equals(default(Edge)))
                    yield return predecessor.Key;
            }
        }

        public static ICollection<Vertex> TopologicalSort<Vertex, Edge>(
            IUndirectedGraph<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            List<Vertex> vertices = new List<Vertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void TopologicalSort<Vertex, Edge>(
            IUndirectedGraph<Vertex, Edge> visitedGraph,
            IList<Vertex> vertices
            )
            where Edge : IEdge<Vertex>
        {
            UndirectedTopologicalSortAlgorithm<Vertex, Edge> topo
                = new UndirectedTopologicalSortAlgorithm<Vertex, Edge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static ICollection<Vertex> TopologicalSort<Vertex, Edge>(
            IVertexListGraph<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            List<Vertex> vertices = new List<Vertex>(visitedGraph.VertexCount);
            TopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void TopologicalSort<Vertex, Edge>(
            IVertexListGraph<Vertex, Edge> visitedGraph,
            IList<Vertex> vertices)
            where Edge : IEdge<Vertex>
        {
            TopologicalSortAlgorithm<Vertex, Edge> topo = new TopologicalSortAlgorithm<Vertex, Edge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static ICollection<Vertex> SourceFirstTopologicalSort<Vertex, Edge>(
            IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            List<Vertex> vertices = new List<Vertex>(visitedGraph.VertexCount);
            SourceFirstTopologicalSort(visitedGraph, vertices);
            return vertices;
        }

        public static void SourceFirstTopologicalSort<Vertex, Edge>(
            IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph,
            IList<Vertex> vertices)
            where Edge : IEdge<Vertex>
        {
            SourceFirstTopologicalSortAlgorithm<Vertex, Edge> topo = new SourceFirstTopologicalSortAlgorithm<Vertex, Edge>(visitedGraph);
            topo.Compute(vertices);
        }

        public static int ConnectedComponents<Vertex,Edge>(
            IUndirectedGraph<Vertex,Edge> g,
            Vertex startVertex,
            IDictionary<Vertex,int> components)
            where Edge : IEdge<Vertex>
        {
            ConnectedComponentsAlgorithm<Vertex,Edge> conn =
                new ConnectedComponentsAlgorithm<Vertex,Edge>(g, components);
            conn.Compute(startVertex);
            return conn.ComponentCount;
        }

        public static int WeaklyConnectedComponents<Vertex, Edge>(
            IVertexListGraph<Vertex, Edge> g,
            IDictionary<Vertex, int> components)
            where Edge : IEdge<Vertex>
        {
            WeaklyConnectedComponentsAlgorithm<Vertex, Edge> conn =
                new WeaklyConnectedComponentsAlgorithm<Vertex, Edge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        public static int StronglyConnectedComponents<Vertex, Edge>(
            IVertexListGraph<Vertex, Edge> g,
            IDictionary<Vertex, int> components)
            where Edge : IEdge<Vertex>
        {
            StronglyConnectedComponentsAlgorithm<Vertex, Edge> conn =
                new StronglyConnectedComponentsAlgorithm<Vertex, Edge>(g, components);
            conn.Compute();
            return conn.ComponentCount;
        }

        public static void Clone<Vertex,Edge>(
            IVertexAndEdgeListGraph<Vertex, Edge> g,
            IMutableVertexAndEdgeListGraph<Vertex, Edge> clone)
            where Vertex : ICloneable
            where Edge : ICloneableEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (clone == null)
                throw new ArgumentNullException("clone");

            Dictionary<Vertex, Vertex> vertexClones = new Dictionary<Vertex, Vertex>();

            foreach (Vertex v in g.Vertices)
            {
                Vertex vc = (Vertex)v.Clone();
                clone.AddVertex(vc);
                vertexClones.Add(v, vc);
            }

            foreach (Edge edge in g.Edges)
            {
                Edge ec = (Edge)edge.Clone(
                    vertexClones[edge.Source],
                    vertexClones[edge.Target]);
                clone.AddEdge(ec);
            }
        }

        public static IMutableBidirectionalGraph<Graph,CondensatedEdge<Vertex, Edge,Graph>> Condensate<Vertex, Edge, Graph>(
            IVertexAndEdgeListGraph<Vertex,Edge> g)
            where Edge : IEdge<Vertex>
            where Graph : IMutableVertexAndEdgeListGraph<Vertex,Edge>, new()
        {
            if (g == null)
                throw new ArgumentNullException("g");

            CondensationGraphAlgorithm<Vertex, Edge, Graph> condensator = new CondensationGraphAlgorithm<Vertex, Edge, Graph>(g);
            condensator.Compute();
            return condensator.CondensatedGraph;
        }

        /// <summary>
        /// Create a collection of odd vertices
        /// </summary>
        /// <param name="g">graph to visit</param>
        /// <returns>colleciton of odd vertices</returns>
        /// <exception cref="ArgumentNullException">g is a null reference</exception>
        public static List<Vertex> OddVertices<Vertex,Edge>(IVertexAndEdgeListGraph<Vertex,Edge> g)
            where Edge : IEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");

            Dictionary<Vertex,int> counts = new Dictionary<Vertex,int>(g.VertexCount);
            foreach (Vertex v in g.Vertices)
            {
                counts.Add(v,0);
            }

            foreach (Edge e in g.Edges)
            {
                ++counts[e.Source];
                --counts[e.Target];
            }

            List<Vertex> odds = new List<Vertex>();
            foreach (KeyValuePair<Vertex,int> de in counts)
            {
                if (de.Value % 2 != 0)
                    odds.Add(de.Key);
            }

            return odds;
        }

        public static bool IsDirectedAcyclicGraph<Vertex, Edge>(IVertexListGraph<Vertex, Edge> g)
            where Edge : IEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");

            return new DagTester().IsDag(g);
        }

        private sealed class DagTester
        {
            private bool isDag = true;

            public bool IsDag<Vertex,Edge>(IVertexListGraph<Vertex, Edge> g)
                where Edge : IEdge<Vertex>
            {
                DepthFirstSearchAlgorithm<Vertex, Edge> dfs = new DepthFirstSearchAlgorithm<Vertex, Edge>(g);
                try
                {
                    dfs.BackEdge += new EdgeEventHandler<Vertex, Edge>(dfs_BackEdge);
                    isDag = true;
                    dfs.Compute();
                    return isDag;
                }
                finally
                {
                    dfs.BackEdge -= new EdgeEventHandler<Vertex, Edge>(dfs_BackEdge);
                }
            }

            void dfs_BackEdge<Vertex,Edge>(object sender, EdgeEventArgs<Vertex, Edge> e)
                where Edge : IEdge<Vertex>
            {
                isDag = false;
            }
        }
    }
}
