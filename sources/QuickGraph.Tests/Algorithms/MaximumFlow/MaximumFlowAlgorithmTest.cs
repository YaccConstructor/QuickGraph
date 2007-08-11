using System;
using System.Collections.Generic;
using QuickGraph.Collections;
using QuickGraph.Predicates;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.MaximumFlow
{
    [TestFixture]
    public class MaximumFlowAlgorithmTest
    {
        private Dictionary<Edge<int>,double> capacities;
        private Dictionary<Edge<int>,Edge<int>> reversedEdges;
        private AdjacencyGraph<int,Edge<int>> graph;
        private int source;
        private int sink;

        [SetUp]
        public void Init()
        {
            this.capacities = new Dictionary<Edge<int>,double>();
            this.reversedEdges = new Dictionary<Edge<int>,Edge<int>>();
            this.graph = new AdjacencyGraph<int, Edge<int>>(true);

            this.source = 0; graph.AddVertex(this.source);
            this.sink = 1;  graph.AddVertex(this.sink);

            BuildSimpleGraph(source, sink);
        }

        [Test]
        public void EdmundsKarp()
        {
            Assert.IsNotNull(this.graph);
            Assert.IsNotNull(this.capacities);
            Assert.IsNotNull(reversedEdges);
            EdmondsKarpMaximumFlowAlgorithm<int, Edge<int>> maxFlow = new EdmondsKarpMaximumFlowAlgorithm<int, Edge<int>>(
                this.graph,
                capacities,
                reversedEdges
                );

            this.TestFlow(maxFlow);
        }

        private void TestFlow(MaximumFlowAlgorithm<int, Edge<int>> maxFlow)
        {
            Assert.IsNotNull(maxFlow);
            Assert.IsNotNull(maxFlow.VisitedGraph);

            double flow = maxFlow.Compute(source, sink);
            Assert.AreEqual(23, flow, double.Epsilon);
            Assert.IsTrue(IsFlow(maxFlow));
            Assert.IsTrue(IsOptimal(maxFlow));
        }

        private void BuildSimpleGraph(int source, int sink)
        {
            int v1, v2, v3, v4;
            v1 = graph.VertexCount+1; graph.AddVertex(v1);
            v2 = graph.VertexCount + 1; graph.AddVertex(v2);
            v3 = graph.VertexCount + 1; graph.AddVertex(v3);
            v4 = graph.VertexCount + 1; graph.AddVertex(v4);

            AddLink(source, v1, 16);
            AddLink(source, v2, 13);
            AddLink(v1, v2, 10);
            AddLink(v2, v1, 4);
            AddLink(v1, v3, 12);
            AddLink(v2, v4, 14);
            AddLink(v3, v2, 9);
            AddLink(v4, v3, 7);
            AddLink(v3, sink, 20);
            AddLink(v4, sink, 4);
        }

        private void AddLink(int u, int v, double capacity)
        {
            Edge<int> edge, reverseEdge;

            edge = new Edge<int>(u, v);  graph.AddEdge(edge);
            capacities[edge] = capacity;

            reverseEdge = new Edge<int>(v, u); graph.AddEdge(reverseEdge);
            capacities[reverseEdge] = 0;

            reversedEdges[edge] = reverseEdge;
            reversedEdges[reverseEdge] = edge;
        }

        private bool IsFlow(MaximumFlowAlgorithm<int,Edge<int>> maxFlow)
        {
            // check edge flow values
            foreach (int u in maxFlow.VisitedGraph.Vertices)
            {
                foreach (Edge<int> a in maxFlow.VisitedGraph.OutEdges(u))
                {
                    if (maxFlow.Capacities[a] > 0)
                        if ((maxFlow.ResidualCapacities[a] + maxFlow.ResidualCapacities[maxFlow.ReversedEdges[a]]
                            != maxFlow.Capacities[a])
                            || (maxFlow.ResidualCapacities[a] < 0)
                            || (maxFlow.ResidualCapacities[maxFlow.ReversedEdges[a]] < 0))
                            return false;
                }
            }

            // check conservation
            Dictionary<int,double> inFlows = new Dictionary<int,double>();
            Dictionary<int,double> outFlows = new Dictionary<int,double>();
            foreach (int u in maxFlow.VisitedGraph.Vertices)
            {
                inFlows[u] = 0;
                outFlows[u] = 0;
            }

            foreach (int u in maxFlow.VisitedGraph.Vertices)
            {
                foreach (Edge<int> e in maxFlow.VisitedGraph.OutEdges(u))
                {
                    if (maxFlow.Capacities[e] > 0)
                    {
                        double flow = maxFlow.Capacities[e] - maxFlow.ResidualCapacities[e];

                        inFlows[e.Target] += flow;
                        outFlows[e.Source] += flow;
                    }
                }
            }

            foreach (int u in maxFlow.VisitedGraph.Vertices)
            {
                if (u != source && u != sink)
                    if (inFlows[u] != outFlows[u])
                        return false;
            }

            return true;
        }

        private bool IsOptimal(MaximumFlowAlgorithm<int,Edge<int>> maxFlow)
        {
            // check if mincut is saturated...
            FilteredVertexListGraph<int, Edge<int>, IVertexListGraph<int, Edge<int>>> residualGraph =
                new FilteredVertexListGraph<int, Edge<int>, IVertexListGraph<int, Edge<int>>>(
                maxFlow.VisitedGraph,
                new AnyVertexPredicate<int>().Test,
                new ReversedResidualEdgePredicate<int,Edge<int>>(maxFlow.ResidualCapacities, maxFlow.ReversedEdges).Test
                );
            BreadthFirstSearchAlgorithm<int, Edge<int>> bfs = new BreadthFirstSearchAlgorithm<int, Edge<int>>(residualGraph);

            Dictionary<int,int> distances = new Dictionary<int,int>();
            VertexDistanceRecorderObserver<int, Edge<int>> vis = new VertexDistanceRecorderObserver<int, Edge<int>>(distances);
            try
            {
                vis.Attach(bfs);
                bfs.Compute(sink);
            }
            finally
            {
                vis.Detach(bfs);
            }

            return distances[source] >= maxFlow.VisitedGraph.VertexCount;
        }

    }
}
