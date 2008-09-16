using System;
using System.Collections.Generic;

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
    public sealed class VertexDistanceRecorderObserver<TVertex, TEdge> :
        IObserver<IDistanceRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> distances;

        public VertexDistanceRecorderObserver()
            :this(new Dictionary<TVertex,int>())
        {}

        public VertexDistanceRecorderObserver(IDictionary<TVertex, int> distances)
        {
            if (distances == null)
                throw new ArgumentNullException("distances");
            this.distances = distances;
        }

        public IDictionary<TVertex, int> Distances
        {
            get { return this.distances; }
        }

        public void Attach(IDistanceRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            algorithm.TreeEdge += new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        public void Detach(IDistanceRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("arg");

            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            int sourceDistance;
            if(!this.distances.TryGetValue(args.Edge.Source, out sourceDistance))
                this.distances[args.Edge.Source] = sourceDistance = 0;
            this.distances[args.Edge.Target] = sourceDistance + 1;
        }
    }
}
