using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using QuickGraph;

namespace QuickGraph
{
    public class BidirectionalGraphFactory
    {
        private static BidirectionalGraph<String, Edge<String>> CreateGraph()
        {
            return new BidirectionalGraph<String, Edge<String>>(false);
        }

        public static Type CreateType()
        {
            return typeof(BidirectionalGraph<String, Edge<String>>);
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> Empty()
        {
            return CreateGraph();
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> NoEdges()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.NoEdges(g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> Loop()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.Loop(g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> LoopDouble()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.LoopDouble(g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> FileDependency()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.FileDependency(g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> RegularLattice10x10()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.RegularLattice(10, 10, g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> Simple()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.Simple(g);
            return g;
        }

        [Factory]
        public BidirectionalGraph<string, Edge<string>> UnBalancedFlow()
        {
            BidirectionalGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.UnBalancedFlow(g);
            return g;
        }
    }
}
