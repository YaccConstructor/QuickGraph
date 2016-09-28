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
        private readonly UndirectedGraph<TEdge, EquatableEdge<TEdge>> _graph;
        public readonly List<List<TEdge>> Cliques = new List<List<TEdge>>();

        public FindingMaximalCliques(UndirectedGraph<TEdge, EquatableEdge<TEdge>> g)
        {
            _graph = g;
            
        }
        public List<List<TEdge>> FindCliques()
        {
            var P = new List<TEdge>(_graph.Vertices);
            _neighbors.Clear();
            Cliques.Clear();
            foreach (var v in _graph.Vertices)
            {
                _neighbors.Add(v, new List<TEdge>());
            }

            foreach (var edge in _graph.Edges)
            {
                if (!object.Equals(edge.Source, edge.Target))
                {
                    _neighbors[edge.Source].Add(edge.Target);
                    _neighbors[edge.Target].Add(edge.Source);
                }
            }

            Compute(P);
            return Cliques;
        }

        private void Compute(List<TEdge> P)
        {
            Tuple<List<TEdge>, List<TEdge>, List<TEdge>> cur;
            var R = new List<TEdge>();
            var X = new List<TEdge>();

            var S = new Stack<Tuple<List<TEdge>, List<TEdge>, List<TEdge>>>();
            S.Push(Tuple.Create(new List<TEdge>(), P, new List<TEdge>()));

            while (S.Count > 0)
            {
                cur = S.Pop();
                R = cur.Item1;
                P = cur.Item2;
                X = cur.Item3;
                if (P.Count == 0 && X.Count == 0 && R.Count > 0)
                {
                    Cliques.Add(new List<TEdge>(R));
                }

                if (P.Count > 0)
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
