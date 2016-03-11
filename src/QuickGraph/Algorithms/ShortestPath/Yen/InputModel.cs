using System.Collections.Generic;

namespace QuickGraph.Algorithms.ShortestPath.Yen
{
  public class InputModel<TVertex, TEdge> where TEdge : IEdge<TVertex>
  {
    public AdjacencyGraph<TVertex, TEdge> Graph { get; set; }
    public Dictionary<TEdge, double> Distances { get; set; }
  }
}
