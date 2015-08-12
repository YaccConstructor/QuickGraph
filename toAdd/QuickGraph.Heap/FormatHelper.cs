using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Heap
{
    internal static class FormatHelper
    {
        public static string ToSize(int size)
        {
            if (size < 1000)
                return String.Format("{0}b", size);
            else if (size < 1000000)
                return String.Format("{0}Kb", size / 1000);
            else
                return String.Format("{0}Mb", size / 1000000);
        }
    }
}
