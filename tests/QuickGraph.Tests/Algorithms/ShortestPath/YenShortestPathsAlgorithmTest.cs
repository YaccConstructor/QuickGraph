using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.ShortestPath.Yen;

namespace QuickGraph.Tests.Algorithms.ShortestPath
{
  [TestClass]
  public class YenShortestPathsAlgorithmTest
  {
    [TestMethod]
    public void TestCharEdges()
    {
      /* generate simple graph
        like this https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        but with directed edges
      */
      var input = GenerateInput();
      var yen = new YenShortestPathsAlgorithm<char, Edge<char>>(input, '1', '5', 10);
      var result = yen.Execute().ToList();

      /*
      Expecting to get 3 paths:
      1. 1-3-4-5
      2. 1-2-4-5
      3. 1-2-3-4-5
      Consistently checking the result
      */
      Assert.AreEqual(3, result.Count);
      // 1.
      Assert.AreEqual(Eq(result[0].ToArray()[0], input.Graph.Edges.ToArray()[1]), true);
      Assert.AreEqual(Eq(result[0].ToArray()[1], input.Graph.Edges.ToArray()[5]), true);
      Assert.AreEqual(Eq(result[0].ToArray()[2], input.Graph.Edges.ToArray()[7]), true);
      // 2.
      Assert.AreEqual(Eq(result[1].ToArray()[0], input.Graph.Edges.ToArray()[0]), true);
      Assert.AreEqual(Eq(result[1].ToArray()[1], input.Graph.Edges.ToArray()[4]), true);
      Assert.AreEqual(Eq(result[1].ToArray()[2], input.Graph.Edges.ToArray()[7]), true);
      // 3.
      Assert.AreEqual(Eq(result[2].ToArray()[0], input.Graph.Edges.ToArray()[0]), true);
      Assert.AreEqual(Eq(result[2].ToArray()[1], input.Graph.Edges.ToArray()[3]), true);
      Assert.AreEqual(Eq(result[2].ToArray()[2], input.Graph.Edges.ToArray()[5]), true);
      Assert.AreEqual(Eq(result[2].ToArray()[3], input.Graph.Edges.ToArray()[7]), true);
    }

    private bool Eq(Edge<char> a, Edge<char> b)
    {
      return a.Source == b.Source && a.Target == b.Target;
    }

    private InputModel<char, Edge<char>> GenerateInput()
    {
      var g = new AdjacencyGraph<char, Edge<char>>(true);
      var distances = new Dictionary<Edge<char>, double>();
      g.AddVertexRange("123456");

      AddEdgeWithDistance(g, distances, '1', '2', 7); // 0
      AddEdgeWithDistance(g, distances, '1', '3', 9); // 1
      AddEdgeWithDistance(g, distances, '1', '6', 14); // 2
      AddEdgeWithDistance(g, distances, '2', '3', 10); // 3
      AddEdgeWithDistance(g, distances, '2', '4', 15); // 4
      AddEdgeWithDistance(g, distances, '3', '4', 11); // 5
      AddEdgeWithDistance(g, distances, '3', '6', 2); // 6
      AddEdgeWithDistance(g, distances, '4', '5', 6); // 7
      AddEdgeWithDistance(g, distances, '5', '6', 9); // 8

      return new InputModel<char, Edge<char>>
      {
        Distances = distances,
        Graph = g
      };
    }
    private void AddEdgeWithDistance(
           AdjacencyGraph<char, Edge<char>> g,
           Dictionary<Edge<char>, double> distances,
           char source, char target, double weight)
    {
      var ac = new Edge<char>(source, target);
      distances[ac] = weight;
      g.AddEdge(ac);
    }

  }
}
