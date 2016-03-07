using System.Collections.Generic;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;

namespace ConsoleApplication1
{
  internal static class Program
  {
    private static void Main()
    {
      // получили Дейсктру для картинки из википедии
      var dijkstra = GetSampleDijkstraGraph();
      var predecessors = new VertexPredecessorRecorderObserver<char, Edge<char>>();
      using (predecessors.Attach(dijkstra));
        dijkstra.Compute('1');

      // проблема в том, что у меня нет predecessors
      IEnumerable<Edge<char>> path;
      predecessors.TryGetPath('1', out path);
      //var col = path.ToList();

      //Console.WriteLine(dijkstra.Distances['1']);
      //Console.WriteLine(dijkstra.Distances['2']);
      //Console.WriteLine(dijkstra.Distances['3']);
      //Console.WriteLine(dijkstra.Distances['4']);
      //Console.WriteLine(dijkstra.Distances['5']);
      //Console.WriteLine(dijkstra.Distances['6']);
    }

    private static DijkstraShortestPathAlgorithm<char, Edge<char>> GetSampleDijkstraGraph()
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

      return new DijkstraShortestPathAlgorithm<char, Edge<char>>(g, AlgorithmExtensions.GetIndexer(distances));
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
