using System;
using System.Collections.Generic;

namespace QuickGraph.GraphXAdapter
{
    using Attributes = IDictionary<string, string>;

    public class EdgeFactory<TVertex>
    {
        public static Func<TVertex, TVertex, Attributes, GraphXEdge<TVertex>>
            VerticesOnly = (v1, v2, attrs) => new GraphXEdge<TVertex>(v1, v2);


        public static Func<TVertex, TVertex, Attributes, GraphXTaggedEdge<TVertex, Attributes>>
            VerticesAndEdgeAttributes = (v1, v2, attrs) =>
                new GraphXTaggedEdge<TVertex, Attributes>(v1, v2, new Dictionary<string, string>(attrs));


        public static Func<TVertex, TVertex, Attributes, GraphXTaggedEdge<TVertex, int?>>
            WeightedNullable = (v1, v2, attrs) =>
                new GraphXTaggedEdge<TVertex, int?>(v1, v2, DotParserAdapter.Common.GetWeightNullable(attrs));


        public static Func<TVertex, TVertex, Attributes, GraphXTaggedEdge<TVertex, int>> Weighted(int defaultWeight)
        {
            return (v1, v2, attrs) =>
                new GraphXTaggedEdge<TVertex, int>(v1, v2, DotParserAdapter.Common.GetWeight(attrs, defaultWeight));
        }
        public static Func<TVertex, TVertex, Attributes, TaggedEdge<TVertex, int>> WeightedTaggedEdge(int defaultWeight)
        {
            return (v1, v2, attrs) =>
                new TaggedEdge<TVertex, int>(v1, v2, DotParserAdapter.Common.GetWeight(attrs, defaultWeight));
        }
    }
}
