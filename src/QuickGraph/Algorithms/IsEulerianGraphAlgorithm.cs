using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms.ConnectedComponents;

namespace QuickGraph.Algorithms
{
    public class IsEulerianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;

        public IsEulerianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            this.graph = graph;
        }

        private Tuple<int?, int?> firstAndSecondIndexOfTrue(bool[] data)
        {
            // if no true elements returns (null, null)
            // if only one true element, returns (indexOfTrue, null)
            int? firstIndex = null, secondIndex = null;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i])
                {
                    if (!firstIndex.HasValue)
                    {
                        firstIndex = i;
                    }
                    else
                    {
                        return new Tuple<int?, int?>(firstIndex, i);
                    } 
                }
            }
            return new Tuple<int?, int?>(firstIndex, secondIndex);
        }

        public bool isEulerian()
        {
            var componentsAlgo = new ConnectedComponentsAlgorithm<TVertex, UndirectedEdge<TVertex>>(this.graph);
            componentsAlgo.Compute();
            
            // Only one component could contain edges
            bool[] hasEdgesInComponent = new bool[componentsAlgo.ComponentCount];
            foreach (var verticeAndComponent in componentsAlgo.Components)
            {
                hasEdgesInComponent[verticeAndComponent.Value] = graph.AdjacentEdges(verticeAndComponent.Key).Count() > 0;
            }
            var t = firstAndSecondIndexOfTrue(hasEdgesInComponent);
            int? firstIndex = t.Item1, secondIndex = t.Item2;
            // No edges at all or More than one component contain edges
            if (!firstIndex.HasValue || secondIndex.HasValue) 
            {
                return false;
            }
            else
            {
                // Now only one component contains edges, check is it an eulerian component
                foreach (var verticeAndComponent in componentsAlgo.Components)
                {   
                    // Vertice in selected component and has even count of edges
                    if (verticeAndComponent.Value == firstIndex.Value && 
                        graph.AdjacentEdges(verticeAndComponent.Key).Count() % 2 == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
