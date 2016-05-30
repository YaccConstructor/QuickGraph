using System;
using System.Linq;
using QuickGraph.Algorithms.ConnectedComponents;

namespace QuickGraph.Algorithms
{
    public enum ComponentWithEdges { NoComponent, OneComponentWithOneVertex, OneComponentWithManyVertices, ManyComponents };

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

        public ComponentWithEdges checkComponentsWithEdges()
        {
            var componentsAlgo = new ConnectedComponentsAlgorithm<TVertex, UndirectedEdge<TVertex>>(this.graph);
            componentsAlgo.Compute();

            bool[] hasEdgesInComponent = new bool[componentsAlgo.ComponentCount];
            foreach (var verticeAndComponent in componentsAlgo.Components)
            {
                hasEdgesInComponent[verticeAndComponent.Value] = graph.AdjacentEdges(verticeAndComponent.Key).Count() > 0;
            }
            var t = firstAndSecondIndexOfTrue(hasEdgesInComponent);
            int? firstIndex = t.Item1, secondIndex = t.Item2;

            if (!firstIndex.HasValue)
            {
                return ComponentWithEdges.NoComponent;
            }
            if (secondIndex.HasValue)
            {
                return ComponentWithEdges.ManyComponents;
            }
            // If component contain one vertex with edge (cycle), it is an eulerian component
            if (componentsAlgo.Components.First(x => x.Value == firstIndex.Value).Key
                    .Equals(componentsAlgo.Components.Last(x => x.Value == firstIndex.Value).Key))
            {
                return ComponentWithEdges.OneComponentWithOneVertex;
            }
            return ComponentWithEdges.OneComponentWithManyVertices;
        }

        public bool satisfiesEulerianCondition(TVertex vertex)
        {
            return graph.AdjacentEdges(vertex).Count() % 2 == 0;
        }

        public bool isEulerian()
        {
            switch (checkComponentsWithEdges())
            {
                case ComponentWithEdges.OneComponentWithOneVertex:
                    return true;
                case ComponentWithEdges.OneComponentWithManyVertices:
                    {
                        foreach (var vertex in graph.Vertices)
                        {
                            if (!satisfiesEulerianCondition(vertex))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                case ComponentWithEdges.NoComponent:
                case ComponentWithEdges.ManyComponents:
                default:
                    return false;
            }
        }
    }
}