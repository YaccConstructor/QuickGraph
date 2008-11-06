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
            CodeContract.Requires(graph != null);
            CodeContract.Requires(stream != null);
            CodeContract.Requires(stream.CanWrite);

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, graph);
        }

        public static TGraph DeserializeFromBinary<TVertex, TEdge, TGraph>(this Stream stream)
            where TGraph : IGraph<TVertex, TEdge>
            where TEdge : IEdge<TVertex>
        {
            CodeContract.Requires(stream != null);
            CodeContract.Requires(stream.CanRead);

            var formatter = new BinaryFormatter();
            var result = formatter.Deserialize(stream);
            return (TGraph)result;
        }

    }
}
