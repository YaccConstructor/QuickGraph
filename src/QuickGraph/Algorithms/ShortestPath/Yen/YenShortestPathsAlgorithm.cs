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
      // получил копию графа
      var copyGraph = ObjectCopier.Clone(old.Graph);
      
      // удалил из нее ребро
      var foundEdge = copyGraph.Edges.First(x => x.Source == edgeRemoving.Source &&
                                                 x.Target == edgeRemoving.Target);
      copyGraph.RemoveEdge(foundEdge);

      // скопировал расстояния ребер
      var newDistances = new Dictionary<Edge<char>, double>();
      var index = 0;
      // взять все ребра старого без удаляемого
      var oldEdges = old.Graph.Edges.Where(x => !(x.Source == edgeRemoving.Source &&
                                                  x.Target == edgeRemoving.Target)).ToArray();
      foreach (var edge in copyGraph.Edges)
      {
        newDistances[edge] = old.Distances[oldEdges[index++]];
      }
      
      return new InputModel
      {
        Distances = newDistances,
        Graph = copyGraph
      };
    }

  }
}
