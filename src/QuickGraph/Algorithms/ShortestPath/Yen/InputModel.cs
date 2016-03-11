using System.Collections.Generic;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class InputModel <TVertex>
  {
    public AdjacencyGraph<TVertex, Edge<TVertex>> Graph { get; set; }
    public Dictionary<Edge<TVertex>, double> Distances { get; set; }
  }
}
