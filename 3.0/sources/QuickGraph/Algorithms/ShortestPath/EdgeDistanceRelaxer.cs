using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph.Algorithms.ShortestPath
{
    public sealed class EdgeDistanceRelaxer
        : IDistanceRelaxer
    {
        public readonly static IDistanceRelaxer Instance = new EdgeDistanceRelaxer();

        public double InitialDistance
        {
            get { return 0; }
        }

        public bool Compare(double a, double b)
        {
            return a < b;
        }

        public double Combine(double distance, double weight)
        {
            return distance + weight;
        }
    }
}
