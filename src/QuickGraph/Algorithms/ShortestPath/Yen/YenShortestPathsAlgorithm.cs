using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class YenShortestPathsAlgorithm<TVertex>
  {
    private TVertex sourceVertix;
    private TVertex targetVertix;
    // limit for amount of paths
    private int k;
    private AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph;

    /*
      double type of tag comes from Dijkstra’s algorithm,
      which is used to get one shortest path.
    */

    public YenShortestPathsAlgorithm(AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph, TVertex s,
      TVertex t, int k)
    {
      sourceVertix = s;
      targetVertix = t;
      this.k = k;
      this.graph = graph;
    }

    public IEnumerable<IEnumerable<TaggedEquatableEdge<TVertex, double>>> Execute()
    {
      var listShortestWays = new List<IEnumerable<TaggedEquatableEdge<TVertex, double>>>();
      // find the first shortest way
      var shortestWay = GetShortestPathInGraph(graph);
      listShortestWays.Add(shortestWay);

      /*
       * in case of Dijkstra’s algorithm couldn't find any ways
       */
      if (shortestWay == null)
      {
        throw new NoPathFoundException();
      }

      for (var i = 0; i < k - 1; i++)
      {
        var minDistance = double.MaxValue;
        IEnumerable<TaggedEquatableEdge<TVertex, double>> pathSlot = null;
        // slote for graph state without some edge
        AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graphSlot = null;
        foreach (var edge in shortestWay)
        {
          // get new state without the edge
          var newGraph = RemoveEdge(graph, edge);

          //find shortest way in the new graph
          var newPath = GetShortestPathInGraph(newGraph);
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
          graphSlot = newGraph;
        }
        if (pathSlot == null)
        {
          break;
        }
        listShortestWays.Add(pathSlot);
        shortestWay = pathSlot;
        graph = graphSlot;
      }
      return listShortestWays;
    }

    private double GetPathDistance(IEnumerable<TaggedEquatableEdge<TVertex, double>> edges)
    {
      var pathSum = 0.0;
      foreach (var edge in edges)
      {
        pathSum += edge.Tag;
      }
      return pathSum;
    }

    private IEnumerable<TaggedEquatableEdge<TVertex, double>> GetShortestPathInGraph(
      AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> graph)
    {
      // calc distances beetween the start vertex and other
      var dij = new DijkstraShortestPathAlgorithm<TVertex, TaggedEquatableEdge<TVertex, double>>(graph, e => e.Tag);
      var vis = new VertexPredecessorRecorderObserver<TVertex, TaggedEquatableEdge<TVertex, double>>();
      using (vis.Attach(dij))
        dij.Compute(sourceVertix);

      // get shortest path from start (source) vertex to target
      IEnumerable<TaggedEquatableEdge<TVertex, double>> path;

      return vis.TryGetPath(targetVertix, out path) ? path : null;
    }

    private AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> RemoveEdge(
      AdjacencyGraph<TVertex, TaggedEquatableEdge<TVertex, double>> old,
      TaggedEquatableEdge<TVertex, double> edgeRemoving)
    {
      // get copy of the grapth using Serialization and Deserialization
      var copyGraph = old.Clone();

      // remove the edge
      foreach (var edge in copyGraph.Edges)
      {
        if (edge == edgeRemoving)
        {
          copyGraph.RemoveEdge(edge);
          break;
        }
      }

      // get all edges but the removing one
      var oldEdges = new List<TaggedEquatableEdge<TVertex, double>>();
      foreach (var edge in old.Edges)
      {
        if (edge != edgeRemoving)
        {
          oldEdges.Add(edge);
        }
      }

      return copyGraph;
    }
  }
}