using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using System.Net;

namespace QuickGraph.Graphviz
{
    /// <summary>
    /// Helper extensions to render graphs to graphviz
    /// </summary>
    public static class GraphvizExtensions
    {
        /// <summary>
        /// Renders a graph to the Graphviz DOT format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string ToGraphviz<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph
            )
            where TEdge : IEdge<TVertex>
        {
            var algorithm = new GraphvizAlgorithm<TVertex, TEdge>(graph);
            return algorithm.Generate();
        }

        /// <summary>
        /// Renders a graph to the Graphviz DOT format.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <typeparam name="TEdge"></typeparam>
        /// <param name="graph"></param>
        /// <param name="initialization">delegate that initializes the algorithm</param>
        /// <returns></returns>
        public static string ToGraphviz<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph,
            Action<GraphvizAlgorithm<TVertex, TEdge>> initialization
            )
            where TEdge : IEdge<TVertex>
        {
            var algorithm = new GraphvizAlgorithm<TVertex, TEdge>(graph);
            initialization(algorithm);
            return algorithm.Generate();
        }

        /// <summary>
        /// Performs a layout .dot in an SVG (Scalable Vector Graphics) file
        /// by calling Agl through the http://rise4fun.com/ REST services.
        /// </summary>
        /// <param name="dot">the dot graph</param>
        /// <returns>the svg graph</returns>
        public static string ToSvg<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);
            Contract.Ensures(Contract.Result<string>() != null);

            return ToSvg(ToGraphviz<TVertex, TEdge>(graph));
        }


        /// <summary>
        /// Performs a layout .dot in an SVG (Scalable Vector Graphics) file
        /// by calling Agl through the http://rise4fun.com/ REST services.
        /// </summary>
        /// <param name="dot">the dot graph</param>
        /// <returns>the svg graph</returns>
        public static string ToSvg<TVertex, TEdge>(
#if !NET20
this 
#endif
            IEdgeListGraph<TVertex, TEdge> graph,
            Action<GraphvizAlgorithm<TVertex, TEdge>> initialization
            )
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(graph != null);
            Contract.Requires(initialization != null);
            Contract.Ensures(Contract.Result<string>() != null);

            return ToSvg(ToGraphviz<TVertex, TEdge>(graph, initialization));
        }


        /// <summary>
        /// Performs a layout .dot in an SVG (Scalable Vector Graphics) file
        /// by calling Agl through the http://rise4fun.com/ REST services.
        /// </summary>
        /// <param name="dot">the dot graph</param>
        /// <returns>the svg graph</returns>
        public static string ToSvg(string dot)
        {
            Contract.Requires(dot != null);
            Contract.Ensures(Contract.Result<string>() != null);

            var request = WebRequest.Create("http://rise4fun.com/services.svc/ask/agl");
            request.Method = "POST";
            // write dot
            using (var writer = new StreamWriter(request.GetRequestStream()))
                writer.Write(dot);
            // read svg
            var response = request.GetResponse();
            string svg;
            using (var reader = new StreamReader(response.GetResponseStream()))
                svg = reader.ReadToEnd();
            return svg;
        }
    }
}
