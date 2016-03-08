using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

/*
Todo:

*/

namespace ConsoleApplication1
{
  internal static class Program
  {
    // это нам дано на входе
    private static char sourceVertix = '1';
    private static char targetVertix = '5';

    private static void Main()
    {

      // вход будет в таком виде + целевые вершины + k:
      var input = GenerateInput();
      var k = 100;

      var listShortestWays = new List<IEnumerable<Edge<char>>>();
      // нашли первый самый короткий путь
      var shortestWay = GetShortestPathFromInput(input);
      listShortestWays.Add(shortestWay);

      for (var i = 0; i < k - 1; i++)
      {
        var minDistance = double.MaxValue;
        IEnumerable<Edge<char>> pathSlot = null;
        // запомним версию графа без какого-то ребра
        InputModel inputSlot = null;
        foreach (var edge in shortestWay)
        {
          // новые параметры для алгоритма Дейскстры: взяли старые и выкинули ребро
          var newInput = RemoveEdge(input, edge);

          //Найти кратчайший путь в перестроенном графе.
          var newPath = GetShortestPathFromInput(newInput);
          if (newPath == null)
          {
            continue;
          }
          var pathWeight = GetPathDistance(newPath, newInput);
          if (pathWeight >= minDistance)
          {
            continue;
          }
          minDistance = pathWeight;
          pathSlot = newPath;
          inputSlot = newInput;
        }
        if (pathSlot == null)
        {
          break;
        }
        listShortestWays.Add(pathSlot);
        shortestWay = pathSlot;
        input = inputSlot;
      }
    }

    private static double GetPathDistance(IEnumerable<Edge<char>> edges, InputModel input) =>
      edges.Sum(edge => input.Distances[edge]);

    private static IEnumerable<Edge<char>> GetShortestPathFromInput(InputModel input)
    {
      // нашли кратчайший путь для стартовой вершины
      var dij = new DijkstraShortestPathAlgorithm<char, Edge<char>>(input.Graph, e => input.Distances[e]);
      var vis = new VertexPredecessorRecorderObserver<char, Edge<char>>();
      using (vis.Attach(dij))
        dij.Compute(sourceVertix);

      // первый самый короткий путь
      IEnumerable<Edge<char>> path;

      return vis.TryGetPath(targetVertix, out path) ? path : null;
    }

    private static InputModel RemoveEdge(InputModel old, Edge<char> edgeRemoving)
    {
      /*
      Создаем новый граф и расстония на основе старого, без одного ребра
      */
      var newGraph = new AdjacencyGraph<char, Edge<char>>(true);
      var newDistances = new Dictionary<Edge<char>, double>();
      newGraph.AddVertexRange(old.Graph.Vertices);
      foreach (var edge in old.Graph.Edges.Where(x => x.Source != edgeRemoving.Source ||
                                                 x.Target != edgeRemoving.Target))
      {
        AddEdgeWithDistance(newGraph, newDistances, edge.Source, edge.Target, old.Distances[edge]);
      }
      return new InputModel
      {
        Distances = newDistances,
        Graph = newGraph
      };
    } 

    private static InputModel GenerateInput()
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

      return new InputModel
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
