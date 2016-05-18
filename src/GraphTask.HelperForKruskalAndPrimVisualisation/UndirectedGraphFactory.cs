using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using GraphX.PCL.Common.Models;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using QuickGraph.Algorithms;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace HelperForKruskalAndPrimVisualisation
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    public static class UndirectedGraphFactory
    {
        static public UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> GetUnderectedGraphFromDot(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            var ugraph = new UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>();
            foreach (var i in graph.Vertices)
            {
                if (!ugraph.ContainsVertex(i))
                    ugraph.AddVertex(i);
            }
            foreach (var i in graph.Edges)
            {
                var z = ugraph.Edges.FirstOrDefault(x => (x.Source == i.Target) && (x.Target == i.Source));
                if (ugraph.Edges.FirstOrDefault(x => (x.Source == i.Target) && (x.Target == i.Source)) == null)
                {
                    ugraph.AddEdge(i);
                }
            }
            return ugraph;
        }
    }
}
