using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Cliques
{
    public class FindingMaximalCliques<TEdge>
    {
        private Dictionary<TEdge, List<TEdge>> _neighbors = new Dictionary<TEdge, List<TEdge>>();
        private readonly int _amountVertices;
        private readonly int _amountEdges;
        private readonly UndirectedGraph<TEdge, EquatableEdge<TEdge>> _graph;
        public readonly List<List<TEdge>> Cliques = new List<List<TEdge>>();

        public FindingMaximalCliques(UndirectedGraph<TEdge, EquatableEdge<TEdge>> g)
        {
            _graph = g;
            _amountEdges = _graph.Edges.Count();
            _amountVertices = _graph.VertexCount;
            
        }
        public void FindCliques()
        {
            var P = new List<TEdge>(_graph.Vertices);

            for (int i = 0; i < _amountVertices; i++)
            {
                _neighbors.Add(_graph.Vertices.ElementAt(i), new List<TEdge>());
            }

            for (int i = 0; i < _amountEdges; i++)
            {
                var edge = _graph.Edges.ElementAt(i);
                if (!object.Equals(edge.Source, edge.Target))
                {
                    _neighbors[edge.Source].Add(edge.Target);
                    _neighbors[edge.Target].Add(edge.Source);
                }
            }

            Compute(P);
        }

        private void Compute(List<TEdge> P)
        {
            Tuple<List<TEdge>, List<TEdge>, List<TEdge>> cur;
            var R = new List<TEdge>();
            var X = new List<TEdge>();

            var S = new Stack<Tuple<List<TEdge>, List<TEdge>, List<TEdge>>>();
            S.Push(Tuple.Create(new List<TEdge>(), P, new List<TEdge>()));

            while (S.Any())
            {
                cur = S.Pop();
                R = cur.Item1;
                P = cur.Item2;
                X = cur.Item3;
                if (!P.Any() && !X.Any() && R.Any())
                {
                    Cliques.Add(new List<TEdge>(R));
                }

                if (P.Any())
                {
                    var v = P.First();

                    var pushR = new List<TEdge>(R);
                    var pushP = new List<TEdge>(P);
                    pushP.Remove(v);
                    var pushX = new List<TEdge>(X);
                    pushX.Add(v);
                    S.Push(Tuple.Create(pushR, pushP, pushX));

                    pushR = new List<TEdge>(R);
                    pushR.Add(v);
                    pushP = P.Intersect(_neighbors[v]).ToList();
                    pushX = X.Intersect(_neighbors[v]).ToList();
                    S.Push(Tuple.Create(pushR, pushP, pushX));
                }
            }
        }   
    }
}
