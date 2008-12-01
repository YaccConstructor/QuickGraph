using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics.Contracts;

namespace QuickGraph.Serialization
{
    public static class SerializationExtensions
    {
        public static void SerializeToBinary<TVertex, TEdge>(
            this IGraph<TVertex, TEdge> graph,
            Stream stream)
            where TEdge : IEdge<TVertex>
        {            
            Contract.Requires(graph != null);
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanWrite);

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, graph);
        }

        public static TGraph DeserializeFromBinary<TVertex, TEdge, TGraph>(this Stream stream)
            where TGraph : IGraph<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanRead);

            var formatter = new BinaryFormatter();
            var result = formatter.Deserialize(stream);
            return (TGraph)result;
        }

    }
}
