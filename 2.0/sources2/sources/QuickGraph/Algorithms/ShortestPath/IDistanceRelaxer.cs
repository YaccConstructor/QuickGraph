using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms.ShortestPath
{
    public interface IDistanceRelaxer
    {
        double InitialDistance { get;}
        bool Compare(double a, double b);
        double Combine(double distance, double weight);
    }
}
