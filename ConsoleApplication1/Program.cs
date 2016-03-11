using System.Collections.Generic;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath.Yen;

/*
Todo:

*/

namespace ConsoleApplication1
{
  internal static class Program
  {
    private static void Main()
    {
      var input = GenerateInput();
      var yen = new YenShortestPathsAlgorithm<char, Edge<char>>(input, '1', '5', 10);
      var result = yen.Execute();
    }

    private static InputModel<char, Edge<char>> GenerateInput()
    {
      var g = new AdjacencyGraph<char, Edge<char>>(true);
      var distances = new Dictionary<Edge<char>, double>();
      g.AddVertexRange("123456");

      AddEdgeWithDistance(g, distances, '1', '2', 7);
      AddEdgeWithDistance(g, distances, '1', '3', 9);
      AddEdgeWithDistance(g, distances, '1', '6', 14);
      AddEdgeWithDistance(g, distances, '2', '3', 10);
      AddEdgeWithDistance(g, distances, '2', '4', 15);
      AddEdgeWithDistance(g, distances, '3', '4', 11);
      AddEdgeWithDistance(g, distances, '3', '6', 2);
      AddEdgeWithDistance(g, distances, '4', '5', 6);
      AddEdgeWithDistance(g, distances, '5', '6', 9);

      return new InputModel<char, Edge<char>>
      {
        Distances = distances,
        Graph = g
      };
    }
    private static void AddEdgeWithDistance(
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
