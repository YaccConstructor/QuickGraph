using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Search;

namespace QuickGraph.Tests.Algorithms.Search
{
    [TypeFixture(typeof(IVertexAndEdgeListGraph<string, Edge<string>>))]
    [TypeFactory(typeof(AdjacencyGraphFactory))]
    public class SearchingExamplesTest
    {
        [Test]
        public void BreadthFirstSearch(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var bfs = new BreadthFirstSearchAlgorithm<string, Edge<string>>(g);
            bfs.Compute();
        }

        [Test]
        public void DepthFirstSearch(IVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            var bfs = new DepthFirstSearchAlgorithm<string, Edge<string>>(g);
            bfs.Compute();
        }
    }

}
