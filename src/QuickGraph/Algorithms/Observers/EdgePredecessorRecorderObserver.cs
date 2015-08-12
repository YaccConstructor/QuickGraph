using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
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
            Contract.Requires(edgePredecessors != null);
            Contract.Requires(endPathEdges != null);

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

        public IDisposable Attach(IEdgePredecessorRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.DiscoverTreeEdge += new EdgeEdgeAction<TVertex, TEdge>(this.DiscoverTreeEdge);
            algorithm.FinishEdge += new EdgeAction<TVertex, TEdge>(this.FinishEdge);

            return new DisposableAction(
                () =>
                {
                    algorithm.DiscoverTreeEdge -= new EdgeEdgeAction<TVertex, TEdge>(this.DiscoverTreeEdge);
                    algorithm.FinishEdge -= new EdgeAction<TVertex, TEdge>(this.FinishEdge);
                });
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

        private void DiscoverTreeEdge(TEdge edge, TEdge targetEdge)
        {
            if (!edge.Equals(targetEdge))
                this.EdgePredecessors[targetEdge] = edge;
        }

        private void FinishEdge(TEdge args)
        {
            foreach (var edge in this.EdgePredecessors.Values)
                if (edge.Equals(args))
                    return;

            this.EndPathEdges.Add(args);
        }
    }
}
