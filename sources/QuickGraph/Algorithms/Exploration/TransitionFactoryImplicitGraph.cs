using System;
using System.Collections.Generic;
using System.Web;

using QuickGraph.Predicates;

namespace QuickGraph.Algorithms.Exploration
{
    public sealed class TransitionFactoryImplicitGraph<TVertex,TEdge> :
        IImplicitGraph<TVertex,TEdge>
        where TVertex : ICloneable
        where TEdge : IEdge<TVertex>
    {
        private IList<ITransitionFactory<TVertex, TEdge>> transitionFactories
            = new List<ITransitionFactory<TVertex, TEdge>>();
        private VertexPredicate<TVertex> successorVertexPredicate
            = new AnyVertexPredicate<TVertex>().Test;
        private EdgePredicate<TVertex, TEdge> successorEdgePredicate
            = new AnyEdgePredicate<TVertex, TEdge>().Test;

        public TransitionFactoryImplicitGraph()
        {}

        public IList<ITransitionFactory<TVertex, TEdge>> TransitionFactories
        {
            get { return this.transitionFactories; }
        }

        public VertexPredicate<TVertex> SuccessorVertexPredicate
        {
            get { return this.successorVertexPredicate; }
            set { this.successorVertexPredicate = value; }
        }

        public EdgePredicate<TVertex, TEdge> SuccessorEdgePredicate
        {
            get { return this.successorEdgePredicate; }
            set { this.successorEdgePredicate = value; }
        }

        public bool IsOutEdgesEmpty(TVertex v)
        {
            return this.OutDegree(v) == 0;
        }

        public int OutDegree(TVertex v)
        {
            int i = 0;
            foreach(TEdge edge in this.OutEdges(v))
                i++;
            return i;
        }

        public IEnumerable<TEdge> OutEdges(TVertex v)
        {
            foreach (ITransitionFactory<TVertex, TEdge> transitionFactory
                in this.TransitionFactories)
            {
                if (!transitionFactory.IsValid(v))
                    continue;

                foreach (var edge in transitionFactory.Apply(v))
                {
                    if (this.SuccessorVertexPredicate(edge.Target) &&
                        this.SuccessorEdgePredicate(edge))
                        yield return edge;
                }
            }
        }

        public TEdge OutEdge(TVertex v, int index)
        {
            int i = 0;
            foreach (var e in this.OutEdges(v))
                if (i++ == index)
                    return e;
            throw new ArgumentOutOfRangeException("index");
        }

        public bool IsDirected
        {
            get { return true; }
        }

        public bool AllowParallelEdges
        {
            get { return true; }
        }
    }
}
