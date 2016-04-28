using System;
using System.Collections.Generic;
using static QuickGraph.DotParserAdapter;

namespace QuickGraph.GraphXAdapter
{
    using Attributes = IDictionary<string, string>;

    public class VertexFactory
    {
        public static Func<string, Attributes, GraphXVertex>
                Name = (v, attrs) => new GraphXVertex(v);


        public static Func<string, Attributes, GraphXTaggedVertex<Attributes>>
            NameAndAttributes = (v, attrs) =>
                new GraphXTaggedVertex<Attributes>(v, attrs);


        public static Func<string, Attributes, GraphXTaggedVertex<int?>>
            WeightedNullable = (v, attrs) =>
                new GraphXTaggedVertex<int?>(v, Common.GetWeightNullable(attrs));


        public static Func<string, Attributes, GraphXTaggedVertex<int>> Weighted(int defaultWeight)
        {
            return (v, attrs) => new GraphXTaggedVertex<int>(v, Common.GetWeight(attrs, defaultWeight));
        }
    }
}
