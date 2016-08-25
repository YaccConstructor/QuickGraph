using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class YenShortestPathsAlgorithm<TVertex>
  {
    private readonly TVertex _sourceVertix;
    private readonly TVertex _targetVertix;
    private readonly Func<TaggedEquatableEdge<TVertex, double>, double> _weights;
    private readonly Func<IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>>,
      IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>>> _filter;

    // limit for amount of paths
    private int _k;
    private AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> _graph;
    private readonly List<TaggedEquatableEdge<TVertex, double>> _removedEdges = new List<TaggedEquatableEdge<TVertex, double>>();

    /*
     * for access from visualisation code
     */

    public List<TaggedEquatableEdge<TVertex, double>> RemovedEdges()
    {
      return _removedEdges;
    }


    /*
      double type of tag comes from Dijkstra’s algorithm,
      which is used to get one shortest path.
    */

    public YenShortestPathsAlgorithm(AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph, TVertex s,
      TVertex t, int k, Func<TaggedEquatableEdge<TVertex, double>, double> weights = null,
      Func<IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>>,
        IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>>> filter = null)
    {
      _sourceVertix = s;
      _targetVertix = t;
      _k = k;
      _graph = graph.Clone();
      _weights = weights;
      _filter = filter;
      if (weights == null)
      {
        _weights = DefaultGetWeights;
      }
      if (filter == null)
      {
        _filter = DefaultFilter;
      }
    }

    private IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>> DefaultFilter(
      IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>> paths)
    {
      return paths;
    }

    private double DefaultGetWeights(TaggedEquatableEdge<TVertex, double> edge)
    {
      return edge.Tag;
    }

    public IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>> Execute()
    {
      var listShortestWays = new List<IEnumerable<TaggedEquatableEdge<TVertex, double>>>();
      // find the first shortest way
      var shortestWay = GetShortestPathInGraph(_graph);
      listShortestWays.Add(shortestWay);

      /*
       * in case of Dijkstra’s algorithm couldn't find any ways
       */
      if (shortestWay == null)
      {
        throw new NoPathFoundException();
      }

      for (var i = 0; i < _k - 1; i++)
      {
        var minDistance = double.MaxValue;
        IEnumerable<TaggedEquatableEdge<TVertex, double>> pathSlot = null;
        TaggedEquatableEdge<TVertex, double> removedEdge = null;
        foreach (var edge in shortestWay)
        {
          _graph.RemoveEdge(edge);

          //find shortest way in the graph without this edge
          var newPath = GetShortestPathInGraph(_graph);
          _graph.AddEdge(edge);

          if (newPath == null)
          {
            continue;
          }
          var pathWeight = GetPathDistance(newPath);
          if (pathWeight >= minDistance)
          {
            continue;
          }
          minDistance = pathWeight;
          pathSlot = newPath;
          removedEdge = edge;
        }
        if (pathSlot == null)
        {
          break;
        }
        listShortestWays.Add(pathSlot);
        _removedEdges.Add(removedEdge);
        shortestWay = pathSlot;
        _graph.RemoveEdge(removedEdge);
      }
      return _filter(listShortestWays);
    }

    private double GetPathDistance(IEnumerable<TaggedEquatableEdge<TVertex, double>> edges)
    {
      var pathSum = 0.0;
      foreach (var edge in edges)
      {
          pathSum += _weights(edge);
      }
      return pathSum;
    }

    private IEnumerable<TaggedEquatableEdge<TVertex, double>> GetShortestPathInGraph(
      AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph)
    {
      // calc distances beetween the start vertex and other
      var dij = new DijkstraShortestPathAlgorithm<TVertex, TaggedEquatableEdge<TVertex, double>>(graph, _weights);
      var vis = new VertexPredecessorRecorderObserver<TVertex, TaggedEquatableEdge<TVertex, double>>();
      using (vis.Attach(dij))
        dij.Compute(_sourceVertix);

      // get shortest path from start (source) vertex to target
      IEnumerable<TaggedEquatableEdge<TVertex, double>> path;


      return vis.TryGetPath(_targetVertix, out path) ? path : null;

    }

  }
}
