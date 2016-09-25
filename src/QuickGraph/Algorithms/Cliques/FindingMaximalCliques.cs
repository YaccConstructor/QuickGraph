using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Cliques
{
    public class Graph
    {
        public List<Edge> Edges;
        public List<char> Vertexes;
    }
    public class Edge
    {
        public Edge(char _v1, char _v2)
        {
            v1 = _v1;
            v2 = _v2;
        }
        public char v1;
        public char v2;
    }

    //class Vertex
    //{
    //    public List<Vertex> neighbors;
    //}

    public class FindingMaximalCliques
    {
        public Dictionary<char, List<char>> neighbors = new Dictionary<char, List<char>>();
        private int amountVertices;
        private int amountEdges;
        public Graph graph;
        public List<List<char>> cliques = new List<List<char>>();

        public FindingMaximalCliques(Graph g)
        {
            graph = g;
            amountEdges = graph.Edges.Count;
            amountVertices = graph.Vertexes.Count;
            
        }
        public void FindNeighbors()
        {
            for (int i = 0; i < amountVertices; i++)
            {
                neighbors.Add(graph.Vertexes[i], new List<char>());
            }

            for (int i = 0; i < amountEdges; i++)
            {
                var edge = graph.Edges[i];
                neighbors[edge.v1].Add(edge.v2);
                neighbors[edge.v2].Add(edge.v1);
            }
            
        }
        
        public void Run()
        {
            var R = new List<char>();
            var P = new List<char>(graph.Vertexes);
            var X = new List<char>();
            Compute(R, P, X);
        }

        public void Compute(List<char> R, List<char> P, List<char> X)
        {
            if (!P.Any() && !X.Any()) // Тут или?
            {
                cliques.Add(new List<char>(R));
            }

            for (int i = 0; i < P.Count; i++)
            {
                var v = P[i];
                R.Add(v);
                Compute(R, P.Intersect(neighbors[v]).ToList(), X.Intersect(neighbors[v]).ToList() );
                R.Remove(v);
                P.Remove(v);
                X.Add(v);
            }
        }
    }
}
