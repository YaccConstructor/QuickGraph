using System;
using System.Collections.Generic;
using static DotParserProject.GraphData;

namespace QuickGraph
{
    using Attributes = IDictionary<string, string>;

    public class DotParserAdapter
    {
        /// <param name="dotSource"></param>
        /// <param name="createGraph">Graph constructor function</param>
        /// <param name="vertexFunc">Packing function (see VertexFunctions class)</param>
        /// <param name="edgeFunc">Packing function (see EdgeFunctions class)</param>
        internal static IMutableVertexAndEdgeSet<TVertex, TEdge> LoadDot<TVertex, TEdge>(string dotSource,
            Func<bool, IMutableVertexAndEdgeSet<TVertex, TEdge>> createGraph,
            Func<string, Attributes, TVertex> vertexFunc,
            Func<TVertex, TVertex, Attributes, TEdge> edgeFunc) where TEdge : IEdge<TVertex>
        {
            var graphData = DotParser.parse(dotSource);
            var graph = createGraph(!graphData.IsStrict);

            foreach (var node in graphData.Nodes)
            {
                graph.AddVertex(vertexFunc(node.Key, node.Value));
            }
            foreach (var parallelEdges in graphData.Edges)
            {
                var vertices = parallelEdges.Key;
                var v1 = vertexFunc(vertices.Item1, graphData.Nodes[vertices.Item1]);
                var v2 = vertexFunc(vertices.Item2, graphData.Nodes[vertices.Item2]);

                foreach (var attr in parallelEdges.Value)
                {
                    graph.AddEdge(edgeFunc(v1, v2, attr));
                }
            }
            return graph;
        }

        public class Common
        {
            public static int? GetWeightNullable(Attributes attrs)
            {
                int weight;
                return int.TryParse(attrs["weight"], out weight) ? (int?) weight : null;
            }

            public static int GetWeightOrFallback(Attributes attrs, int fallback)
            {
                if (!attrs.ContainsKey("weight")) return fallback;

                int weight;
                return int.TryParse(attrs["weight"], out weight) ? weight : fallback;
            }
        }

        /// <summary>
        /// Example vertex packing functions
        /// </summary>
        public class VertexFunctions
        {
            public static Func<string, Attributes, string>
                Name = (v, attr) => v;


            public static Func<string, Attributes, KeyValuePair<string, Attributes>>
                NameAndAttributes = (v, attrs) =>
                    new KeyValuePair<string, Attributes>(v, new Dictionary<string, string>(attrs));


            public static Func<string, Attributes, KeyValuePair<string, int?>>
                WeightNullable = (v, attrs) =>
                    new KeyValuePair<string, int?>(v, Common.GetWeightNullable(attrs));


            public static Func<string, Attributes, KeyValuePair<string, int>>
                WeightOrFallback(int fallback)
            {
                return (v, attrs) =>
                    new KeyValuePair<string, int>(v, Common.GetWeightOrFallback(attrs, fallback));
            }
        }

        /// <summary>
        /// Example edge packing functions
        /// </summary>
        /// <typeparam name="TVertex">type of the vertices</typeparam>
        public class EdgeFunctions<TVertex>
        {
            public static Func<TVertex, TVertex, Attributes, SEdge<TVertex>>
                VerticesOnly = (v1, v2, attrs) => new SEdge<TVertex>(v1, v2);


            public static Func<TVertex, TVertex, Attributes, STaggedEdge<TVertex, Attributes>>
                VerticesAndEdgeAttributes = (v1, v2, attrs) =>
                    new STaggedEdge<TVertex, Attributes>(v1, v2, new Dictionary<string, string>(attrs));


            public static Func<TVertex, TVertex, Attributes, STaggedEdge<TVertex, int?>>
                WeightNullable = (v1, v2, attrs) =>
                    new STaggedEdge<TVertex, int?>(v1, v2, Common.GetWeightNullable(attrs));


            public static Func<TVertex, TVertex, Attributes, STaggedEdge<TVertex, int>>
                WeightOrFallback(int fallback)
            {
                return (v1, v2, attrs) =>
                    new STaggedEdge<TVertex, int>(v1, v2, Common.GetWeightOrFallback(attrs, fallback));
            }
        }
    }
}