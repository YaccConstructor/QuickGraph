using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace QuickGraph.Gunrock
{
    public abstract class GunrockAlgorithmWrapper
    {
        [DllImport("gunrockwrapper", EntryPoint = "release_memory")]
        protected static extern void release_memory(IntPtr ptr);

        protected static int[] ConvertToManagedArray(int elementsNum, IntPtr labelsPtr)
        {
            int[] labels = new int[elementsNum];
            Marshal.Copy(labelsPtr, labels, 0, elementsNum);
            release_memory(labelsPtr);
            return labels;
        }

        protected static Dictionary<int, int> CreateDictionary(int[] labels)
        {
            var nodesToResults = new Dictionary<int, int>();
            for (int i = 0; i < labels.Length; i++)
            {
                nodesToResults.Add(i, labels[i]);
            }
            return nodesToResults;
        }
    }
}