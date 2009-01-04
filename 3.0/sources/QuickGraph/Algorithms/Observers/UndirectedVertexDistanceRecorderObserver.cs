using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
    [Serializable]
    public sealed class UndirectedVertexDistanceRecorderObserver<TVertex, TEdge> 
        : IObserver<IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, int> distances;

        public UndirectedVertexDistanceRecorderObserver()
            :this(new Dictionary<TVertex,int>())
        {}

        public UndirectedVertexDistanceRecorderObserver(IDictionary<TVertex, int> distances)
        {
            Contract.Requires(distances != null);

            this.distances = distances;
        }

        public IDictionary<TVertex, int> Distances
        {
            get { return this.distances; }
        }

        public void Attach(IUndirectedTreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge += new UndirectedEdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        public void Detach(IUndirectedTreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge -= new UndirectedEdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        private void TreeEdge(Object sender, UndirectedEdgeEventArgs<TVertex,TEdge> args)
        {
            int sourceDistance;
            if(!this.distances.TryGetValue(args.Source, out sourceDistance))
                this.distances[args.Source] = sourceDistance = 0;
            this.distances[args.Target] = sourceDistance + 1;
        }
    }
}
