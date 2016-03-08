using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class YenShortestPathsAlgorithm
  {
    private char sourceVertix;
    private char targetVertix;
    private int k;
    private InputModel input;

    public YenShortestPathsAlgorithm(InputModel input, char s, char t, int k)
    {
      sourceVertix = s;
      targetVertix = t;
      this.k = k;
      this.input = input;
    }

    public IEnumerable<IEnumerable<Edge<char>>> Execute()
    {
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
      return listShortestWays;
    } 

    private double GetPathDistance(IEnumerable<Edge<char>> edges, InputModel input) =>
      edges.Sum(edge => input.Distances[edge]);

    private IEnumerable<Edge<char>> GetShortestPathFromInput(InputModel input)
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

    private InputModel RemoveEdge(InputModel old, Edge<char> edgeRemoving)
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
