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
    public sealed class EdgePredecessorRecorderObserver<TVertex, TEdge> :
        IObserver<IEdgePredecessorRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TEdge,TEdge> edgePredecessors;
        private IList<TEdge> endPathEdges;

        public EdgePredecessorRecorderObserver()
            :this(new Dictionary<TEdge,TEdge>(), new List<TEdge>())
        {}

        public EdgePredecessorRecorderObserver(
            IDictionary<TEdge,TEdge> edgePredecessors,
            IList<TEdge> endPathEdges
            )
        {
            if (edgePredecessors == null)
                throw new ArgumentNullException("edgePredecessors");
            if (endPathEdges == null)
                throw new ArgumentNullException("endPathEdges");

            this.edgePredecessors = edgePredecessors;
            this.endPathEdges = endPathEdges;
        }

        public IDictionary<TEdge,TEdge> EdgePredecessors
        {
            get
            {
                return edgePredecessors;
            }
        }

        public IList<TEdge> EndPathEdges
        {
            get
            {
                return endPathEdges;
            }
        }

        public void Attach(IEdgePredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            algorithm.DiscoverTreeEdge +=new EdgeEdgeEventHandler<TVertex,TEdge>(this.DiscoverTreeEdge);
            algorithm.FinishEdge +=new EdgeEventHandler<TVertex,TEdge>(this.FinishEdge);
        }

        public void Detach(IEdgePredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            algorithm.DiscoverTreeEdge -= new EdgeEdgeEventHandler<TVertex, TEdge>(this.DiscoverTreeEdge);
            algorithm.FinishEdge -= new EdgeEventHandler<TVertex, TEdge>(this.FinishEdge);
        }

        public ICollection<TEdge> Path(TEdge se)
        {
            List<TEdge> path = new List<TEdge>();

            TEdge ec = se;
            path.Insert(0, ec);
            while (EdgePredecessors.ContainsKey(ec))
            {
                TEdge e = EdgePredecessors[ec];
                path.Insert(0, e);
                ec = e;
            }
            return path;
        }

        public ICollection<ICollection<TEdge>> AllPaths()
        {
            IList<ICollection<TEdge>> es = new List<ICollection<TEdge>>();

            foreach (var e in EndPathEdges)
                es.Add(Path(e));

            return es;
        }

        public ICollection<TEdge> MergedPath(TEdge se, IDictionary<TEdge,GraphColor> colors)
        {
            List<TEdge> path = new List<TEdge>();

            TEdge ec = se;
            GraphColor c = colors[ec];
            if (c != GraphColor.White)
                return path;
            else
                colors[ec] = GraphColor.Black;

            path.Insert(0, ec);
            while (EdgePredecessors.ContainsKey(ec))
            {
                TEdge e = EdgePredecessors[ec];
                c = colors[e];
                if (c != GraphColor.White)
                    return path;
                else
                    colors[e] = GraphColor.Black;

                path.Insert(0, e);
                ec = e;
            }
            return path;
        }

        public ICollection<ICollection<TEdge>> AllMergedPaths()
        {
            List<ICollection<TEdge>> es = new List<ICollection<TEdge>>(EndPathEdges.Count);
            IDictionary<TEdge,GraphColor> colors = new Dictionary<TEdge,GraphColor>();

            foreach (KeyValuePair<TEdge,TEdge> de in EdgePredecessors)
            {
                colors[de.Key] = GraphColor.White;
                colors[de.Value] = GraphColor.White;
            }

            for (int i = 0; i < EndPathEdges.Count; ++i)
                es.Add(MergedPath(EndPathEdges[i], colors));

            return es;
        }

        private void DiscoverTreeEdge(Object sender, EdgeEdgeEventArgs<TVertex,TEdge> args)
        {
            if (!args.Edge.Equals(args.TargetEdge))
                EdgePredecessors[args.TargetEdge] = args.Edge;
        }

        private void FinishEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            foreach (var edge in this.EdgePredecessors.Values)
                if (edge.Equals(args.Edge))
                    return;

            this.EndPathEdges.Add(args.Edge);
        }
    }
}
