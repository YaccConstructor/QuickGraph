using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace QuickGraph.Gunrock
{
    public class ConnectedComponents : GunrockAlgorithmWrapper
    {
        [DllImport("gunrockwrapper", EntryPoint="run_cc")]
        public static extern IntPtr run_cc(int nodesNum, int edgesNum, int[] rowOffsets, int[] colIndices);
        
        public static Dictionary<int, int> FindComponents<TEdge, TGraph>(TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IEdgeListGraph<int, TEdge>
        {
            var csrRepresentation = Util.CreateCsrRepresentationFromQuickGraphFast<TEdge, TGraph>(inputGraph);
            return FindComponents(csrRepresentation);
        }

        ///returns component index for each node. 
        ///Component index is the number of the first node to appear in the component
        public static Dictionary<int, int> FindComponents(Tuple<int[], int[], int[]> csrRepresentation)
        {
            var rowOffsets = csrRepresentation.Item1;
            var colIndices = csrRepresentation.Item2;
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;
            
            IntPtr labelsPtr = run_cc(nodesNum, edgesNum, rowOffsets, colIndices);
            
            int[] labels = ConvertToManagedArray(nodesNum, labelsPtr);

            var verticesToLabels = CreateDictionary(labels);
            return verticesToLabels;
        }
    }
}