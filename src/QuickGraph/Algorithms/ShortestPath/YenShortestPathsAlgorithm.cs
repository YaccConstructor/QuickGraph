using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.ShortestPath
{
  //public class YenShortestPathsAlgorithm<TVertex, TEdge> where TEdge : IEdge<TVertex>
  public class YenShortestPathsAlgorithm
  {
    // нужен алгоритм, который ищет кратчайший путь
    //private DijkstraShortestPathAlgorithm<TVertex, TEdge>  algorithm;

    public YenShortestPathsAlgorithm()
    {

    }

    public DijkstraShortestPathAlgorithm<char, Edge<char>> getD()
    {
      var g = new AdjacencyGraph<char, Edge<char>>();
      var distances = new Dictionary<Edge<char>, double>();

      // пример графа в википедии
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

      var dijkstra = new DijkstraShortestPathAlgorithm<char, Edge<char>>(g, AlgorithmExtensions.GetIndexer(distances));
      var predecessors = new VertexPredecessorRecorderObserver<char, Edge<char>>();

      using (predecessors.Attach(dijkstra))
      return dijkstra;
    }

    private static void AddEdge(
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
