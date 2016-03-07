using System;
using System.Collections.Generic;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

namespace ConsoleApplication1
{
  internal static class Program
  {
    private static void Main()
    {
      // нашли кратчайший путь для стартовой вершины
      var dij = GetDijkstraSampleGraph();
      var vis = new VertexPredecessorRecorderObserver<char, Edge<char>>();
      using (vis.Attach(dij))
        dij.Compute('1');

      var m = dij.VisitedGraph;
      Console.WriteLine(m.GetType());

      // первый самый короткий путь
      IEnumerable<Edge<char>> path;
      vis.TryGetPath('5', out path);

      // хотим использовать Йена для 1ой и 5ой вершины и k = 3;
      var k = 3;
      var listShortestWays = new List<IEnumerable<Edge<char>>>();
      var shortestWay = path;
      var graph = dij.VisitedGraph; // изначальный граф с картинки

      for (var i = 0; i < k - 1; i++)
      {
        var min = int.MaxValue;
        IEnumerable<Edge<char>> newPath; // мб инициализировать?
        // запомним версию графа без какого-то ребра
        AdjacencyGraph<char, Edge<char>> graphSlot;
        foreach (var edge in shortestWay)
        {
          
        }
      }

    }

    private static DijkstraShortestPathAlgorithm<char, Edge<char>> GetDijkstraSampleGraph()
    {
      var g = new AdjacencyGraph<char, Edge<char>>(true);
      var distances = new Dictionary<Edge<char>, double>();
      g.AddVertexRange("123456");

      AddEdge(g, distances, '1', '2', 7);
      AddEdge(g, distances, '1', '3', 9);
      AddEdge(g, distances, '1', '6', 14);
      AddEdge(g, distances, '2', '3', 10);
      AddEdge(g, distances, '2', '4', 15);
      AddEdge(g, distances, '3', '4', 11);
      AddEdge(g, distances, '3', '6', 2);
      AddEdge(g, distances, '4', '5', 6);
      AddEdge(g, distances, '5', '6', 9);

      AddEdge(g, distances, '2', '1', 7);
      AddEdge(g, distances, '3', '1', 9);
      AddEdge(g, distances, '6', '1', 14);
      AddEdge(g, distances, '3', '2', 10);
      AddEdge(g, distances, '4', '2', 15);
      AddEdge(g, distances, '4', '3', 11);
      AddEdge(g, distances, '6', '3', 2);
      AddEdge(g, distances, '5', '4', 6);
      AddEdge(g, distances, '6', '5', 9);

      return new DijkstraShortestPathAlgorithm<char, Edge<char>>(g, e => distances[e]);
    }

    private static void AddEdge(
           AdjacencyGraph<char, Edge<char>> g,
           Dictionary<Edge<char>, double> distances,
           char source, char target, double weight)
    {
      var ac = new Edge<char>(source, target); distances[ac] = weight; g.AddEdge(ac);
    }
  }
}
