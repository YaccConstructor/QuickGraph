using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.GraphColoring.VertexColoring
{
    public sealed class InputModel<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public UndirectedGraph<TVertex, TEdge> Graph { get; set; }
        public Dictionary<TVertex, int> Colors { get; set; }
    }

    public class VertexColoringAlgorithm<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        private InputModel<TVertex, TEdge> input;

        public VertexColoringAlgorithm(InputModel<TVertex, TEdge> input)
        {
            this.input = input;
        }

        public InputModel<TVertex, TEdge> Compute()
        {
            int V = input.Graph.VertexCount;
            var listOfVertex = new List<IEnumerable<TVertex>>();
            var vertexColor = new Dictionary<TVertex, int>();
            var firstVertex = input.Graph.Vertices.First();


            // Initialize remaining vertices as unassigned
            foreach (var vertex in input.Graph.Vertices)
            {
                vertexColor[vertex] = -1; // no color is assigned to vertex
            }

            // Assign the first color to first vertex
            vertexColor[firstVertex] = 0;

            /*
            A temporary array to store the available colors. True
            value of available[cr] would mean that the color cr is
            assigned to one of its adjacent vertices
            */
            bool[] available = new bool[V];
            for (int usedColor = 0; usedColor < V; usedColor++)
            {
                available[usedColor] = false;
            }

            // Assign colors to remaining V-1 vertices            
            foreach (var vertexOfGraph in input.Graph.Vertices)
            {
                if (!(vertexOfGraph.Equals(firstVertex)))
                {
                    // Process all adjacent vertices and flag their colors as unavailable
                    foreach (var edgesOfProcessVertex in input.Graph.AdjacentEdges(vertexOfGraph))
                    {
                        if (vertexColor[edgesOfProcessVertex.GetOtherVertex(vertexOfGraph)] != -1)
                        {
                            available[vertexColor[edgesOfProcessVertex.GetOtherVertex(vertexOfGraph)]] = true;
                        }
                    }

                    // Find the first available color
                    int usedColor = new int();
                    for (usedColor = 0; usedColor < V; usedColor++)
                    {
                        if (available[usedColor] == false)
                            break;
                    }

                    // Assign the found color
                    vertexColor[vertexOfGraph] = usedColor;

                    // Reset the values back to false for the next iteration
                    foreach (var edgesOfProcessVertex in input.Graph.AdjacentEdges(vertexOfGraph))
                    {
                        if (vertexColor[edgesOfProcessVertex.GetOtherVertex(vertexOfGraph)] != -1)
                        {
                            available[vertexColor[edgesOfProcessVertex.GetOtherVertex(vertexOfGraph)]] = false;
                        }
                    }
                }
            }
            return new InputModel<TVertex, TEdge>
            {
                Graph = input.Graph,
                Colors = vertexColor
            };

        }
    }
}