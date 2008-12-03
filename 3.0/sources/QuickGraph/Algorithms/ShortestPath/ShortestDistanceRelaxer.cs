using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms.ShortestPath
{
    public sealed class ShortestDistanceRelaxer 
        : IDistanceRelaxer
    {
        private ShortestDistanceRelaxer() { }

        public static readonly ShortestDistanceRelaxer Instance = new ShortestDistanceRelaxer();

        public double InitialDistance
        {
            get { return double.MaxValue; }
        }

        public bool Compare(double a, double b)
        {
#if DEBUG
            checked 
            {
                return a < b;
            }
#endif
        }

        public double Combine(double distance, double weight)
        {
#if DEBUG
            checked 
            {
                return distance + weight;
            }
#endif
        }
    }
}
