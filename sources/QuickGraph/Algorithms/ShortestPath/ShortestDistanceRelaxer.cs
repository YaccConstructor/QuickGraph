using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms.ShortestPath
{
    public sealed class ShortestDistanceRelaxer : IDistanceRelaxer
    {
        public double InitialDistance
        {
            get { return 0.0; }
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
