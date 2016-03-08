using System.Collections.Generic;
using QuickGraph;

namespace ConsoleApplication1
{
  public class InputModel
  {
    public AdjacencyGraph<char, Edge<char>> Graph { get; set; }
    public Dictionary<Edge<char>, double> Distances { get; set; }
  }
}
