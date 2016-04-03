using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace QuickGraph.Algorithms.KernighanLinAlgoritm
{
    public class Partition<TVertex>
    {
        public SortedSet<TVertex> A { get; set; }
        public SortedSet<TVertex> B { get; set; }
        public double cutCost { get; set; }
        public Partition(SortedSet<TVertex> A, SortedSet<TVertex> B, double cutCost = 0)
        {
            this.A = A;
            this.B = B;
            this.cutCost = cutCost;

        }
    }

}
