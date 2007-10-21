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
    public sealed class EdgeRecorderObserver<TVertex, TEdge> :
        IObserver<ITreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IList<TEdge> edges;
        public EdgeRecorderObserver()
            :this(new List<TEdge>())
        {}

        public EdgeRecorderObserver(IList<TEdge> edges)
        {
            if (edges == null)
                throw new ArgumentNullException("edges");
            this.edges = edges;
        }

        public IList<TEdge> Edges
        {
            get
            {
                return this.edges;
            }
        }

        public void Attach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.TreeEdge +=new EdgeEventHandler<TVertex,TEdge>(RecordEdge);
        }

        public void Detach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.TreeEdge -=new EdgeEventHandler<TVertex,TEdge>(RecordEdge);
        }

        public void RecordEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            this.Edges.Add(args.Edge);
        }
        public void RecordSource(Object sender, EdgeEdgeEventArgs<TVertex,TEdge> args)
        {
            this.Edges.Add(args.Edge);
        }
        public void RecordTarget(Object sender, EdgeEdgeEventArgs<TVertex,TEdge> args)
        {
            this.Edges.Add(args.TargetEdge);
        }

    }
}
