using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class YenShortestPathsAlgorithm<TVertex, TEdge> where TEdge : IEdge<TVertex>
  {
    private TVertex sourceVertix;
    private TVertex targetVertix;
    // limit for amount of paths
    private int k;
    private InputModel<TVertex, TEdge> input;

    public YenShortestPathsAlgorithm(InputModel<TVertex, TEdge> input, TVertex s, TVertex t, int k)
    {
      sourceVertix = s;
      targetVertix = t;
      this.k = k;
      this.input = input;
    }

    public IEnumerable<IEnumerable<TEdge>> Execute()
    {
      var listShortestWays = new List<IEnumerable<TEdge>>();
      // find the first shortest way
      var shortestWay = GetShortestPathFromInput(input);
      listShortestWays.Add(shortestWay);

      for (var i = 0; i < k - 1; i++)
      {
        var minDistance = double.MaxValue;
        IEnumerable<TEdge> pathSlot = null;
        // slote for graph state without some edge
        InputModel<TVertex, TEdge> inputSlot = null;
        foreach (var edge in shortestWay)
        {
          // get new state without the edge
          var newInput = RemoveEdge(input, edge);

          //find shortest way in the new graph
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

    private double GetPathDistance(IEnumerable<TEdge> edges, InputModel<TVertex, TEdge> input)
    {
      var pathSum = 0.0;
      foreach (var edge in edges)
      {
        pathSum += input.Distances[edge];
      }
      return pathSum;
    }

    private IEnumerable<TEdge> GetShortestPathFromInput(InputModel<TVertex, TEdge> input)
    {
      // calc distances beetween the start vertex and other
      var dij = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(input.Graph, e => input.Distances[e]);
      var vis = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
      using (vis.Attach(dij))
        dij.Compute(sourceVertix);

      // get shortest path from start (source) vertex to target
      IEnumerable<TEdge> path;

      return vis.TryGetPath(targetVertix, out path) ? path : null;
    }

    private InputModel<TVertex, TEdge> RemoveEdge(InputModel<TVertex, TEdge> old, TEdge edgeRemoving)
    {
      // get copy of the grapth using Serialization and Deserialization
      var copyGraph = old.Graph.Clone();

      // remove the edge
      foreach (var edge in copyGraph.Edges)
      {
        if (edge.Source.Equals(edgeRemoving.Source) && edge.Target.Equals(edgeRemoving.Target))
        {
          copyGraph.RemoveEdge(edge);
          break;
        }
      }

      // get all edges but the removing one
      List<TEdge> oldEdges = new List<TEdge>();
      foreach (var edge in old.Graph.Edges)
      {
        if (!(edge.Source.Equals(edgeRemoving.Source) && edge.Target.Equals(edgeRemoving.Target)))
        {
          oldEdges.Add(edge);
        }
      }
      var oldEdgesArray = oldEdges.ToArray();

      // get copy of the distancies
      var newDistances = new Dictionary<TEdge, double>();
      var index = 0;

      foreach (var edge in copyGraph.Edges)
      {
        newDistances[edge] = old.Distances[oldEdgesArray[index++]];
      }

      return new InputModel<TVertex, TEdge>
      {
        Distances = newDistances,
        Graph = copyGraph
      };
    }

  }
}