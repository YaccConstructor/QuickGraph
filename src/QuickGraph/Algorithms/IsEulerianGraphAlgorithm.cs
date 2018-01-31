using System;
using System.Linq;
using QuickGraph.Algorithms.ConnectedComponents;

namespace QuickGraph.Algorithms
{
    public enum ComponentWithEdges { NoComponent, OneComponent, ManyComponents }

    public class IsEulerianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;

        public IsEulerianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            var newGraph = new UndirectedGraph<TVertex, UndirectedEdge<TVertex>>(false, graph.EdgeEqualityComparer);
            newGraph.AddVertexRange(graph.Vertices);
            newGraph.AddEdgeRange(graph.Edges);
            EdgePredicate<TVertex, UndirectedEdge<TVertex>> isLoop = e => e.Source.Equals(e.Target);
            newGraph.RemoveEdgeIf(isLoop);
            this.graph = newGraph;
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
                hasEdgesInComponent[verticeAndComponent.Value] = !graph.IsAdjacentEdgesEmpty(verticeAndComponent.Key);
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
            return ComponentWithEdges.OneComponent;
        }

        public bool satisfiesEulerianCondition(TVertex vertex)
        {
            return graph.AdjacentDegree(vertex) % 2 == 0;
        }

        public bool isEulerian()
        {
            switch (checkComponentsWithEdges())
            {
                case ComponentWithEdges.OneComponent:
                    return graph.Vertices.All<TVertex>(satisfiesEulerianCondition);
                case ComponentWithEdges.NoComponent:
                    return graph.VertexCount == 1;
                case ComponentWithEdges.ManyComponents:
                default:
                    return false;
            }
        }
    }
}