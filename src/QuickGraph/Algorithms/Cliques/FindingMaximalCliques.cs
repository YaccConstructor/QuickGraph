using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.Cliques
{
    public static class FindingMaximalCliques<TEdge>
    {
        private static Dictionary<TEdge, List<TEdge>> _neighbors;
        private static List<List<TEdge>> _сliques;

        public static List<List<TEdge>> FindCliques(UndirectedGraph<TEdge, EquatableEdge<TEdge>> g)
        {
            _neighbors = new Dictionary<TEdge, List<TEdge>>();
            _сliques = new List<List<TEdge>>();
            var P = new List<TEdge>(g.Vertices);

            foreach (var v in g.Vertices)
            {
                    _neighbors.Add(v, new List<TEdge>());
            }

            foreach (var edge in g.Edges)
            {
                if (!object.Equals(edge.Source, edge.Target))
                {
                    _neighbors[edge.Source].Add(edge.Target);
                    _neighbors[edge.Target].Add(edge.Source);
                }
            }

            Compute(P);
            return _сliques;
        }

        private static void Compute(List<TEdge> P)
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
                    _сliques.Add(new List<TEdge>(R));
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
