using System.Collections.Generic;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class InputModel
  {
    public AdjacencyGraph<char, Edge<char>> Graph { get; set; }
    public Dictionary<Edge<char>, double> Distances { get; set; }
  }
}
