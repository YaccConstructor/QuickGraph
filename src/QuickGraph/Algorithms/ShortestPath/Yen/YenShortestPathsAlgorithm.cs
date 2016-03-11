using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class YenShortestPathsAlgorithm <TVertex>
  {
    private TVertex sourceVertix;
    private TVertex targetVertix;
    private int k;
    private InputModel<TVertex> input;

    public YenShortestPathsAlgorithm(InputModel<TVertex> input, TVertex s, TVertex t, int k)
    {
      sourceVertix = s;
      targetVertix = t;
      this.k = k;
      this.input = input;
    }

    public IEnumerable<IEnumerable<Edge<TVertex>>> Execute()
    {
      var listShortestWays = new List<IEnumerable<Edge<TVertex>>>();
      // нашли первый самый короткий путь
      var shortestWay = GetShortestPathFromInput(input);
      listShortestWays.Add(shortestWay);

      for (var i = 0; i < k - 1; i++)
      {
        var minDistance = double.MaxValue;
        IEnumerable<Edge<TVertex>> pathSlot = null;
        // запомним версию графа без какого-то ребра
        InputModel<TVertex> inputSlot = null;
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

    private double GetPathDistance(IEnumerable<Edge<TVertex>> edges, InputModel<TVertex> input) =>
      edges.Sum(edge => input.Distances[edge]);

    private IEnumerable<Edge<TVertex>> GetShortestPathFromInput(InputModel<TVertex> input)
    {
      // нашли кратчайший путь для стартовой вершины
      var dij = new DijkstraShortestPathAlgorithm<TVertex, Edge<TVertex>>(input.Graph, e => input.Distances[e]);
      var vis = new VertexPredecessorRecorderObserver<TVertex, Edge<TVertex>>();
      using (vis.Attach(dij))
        dij.Compute(sourceVertix);

      // первый самый короткий путь
      IEnumerable<Edge<TVertex>> path;

      return vis.TryGetPath(targetVertix, out path) ? path : null;
    }

    private InputModel<TVertex> RemoveEdge(InputModel<TVertex> old, Edge<TVertex> edgeRemoving)
    {
      // получил копию графа
      var copyGraph = ObjectCopier.Clone(old.Graph);
      
      // удалил из нее ребро
      var foundEdge = copyGraph.Edges.First(x => x.Source.Equals(edgeRemoving.Source) &&
                                                 x.Target.Equals(edgeRemoving.Target) );
      copyGraph.RemoveEdge(foundEdge);

      // скопировал расстояния ребер
      var newDistances = new Dictionary<Edge<TVertex>, double>();
      var index = 0;
      // взять все ребра старого без удаляемого
      var oldEdges = old.Graph.Edges.Where(x => !(x.Source.Equals(edgeRemoving.Source) &&
                                                  x.Target.Equals(edgeRemoving.Target)) ).ToArray();
      foreach (var edge in copyGraph.Edges)
      {
        newDistances[edge] = old.Distances[oldEdges[index++]];
      }
      
      return new InputModel<TVertex>
      {
        Distances = newDistances,
        Graph = copyGraph
      };
    }

  }
}
