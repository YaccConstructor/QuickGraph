using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using QuickGraph;

namespace QuickGraph
{
    public class EdgeListGraphFactory
    {
        [Factory]
        public EdgeListGraph<int, Edge<int>> Empty()
        {
            return new EdgeListGraph<int, Edge<int>>();
        }

        [Factory]
        public EdgeListGraph<int, Edge<int>> OneEdge()
        {
            EdgeListGraph<int, Edge<int>> g = new EdgeListGraph<int, Edge<int>>();
            g.AddEdge(new Edge<int>(0,1));
            return g;
        }

        [Factory]
        public EdgeListGraph<int, Edge<int>> TwoEdges()
        {
            EdgeListGraph<int, Edge<int>> g = new EdgeListGraph<int, Edge<int>>();
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(2, 3));
            return g;
        }

        [Factory]
        public EdgeListGraph<int, Edge<int>> Loop()
        {
            EdgeListGraph<int, Edge<int>> g = new EdgeListGraph<int, Edge<int>>();
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(1, 0));
            return g;
        }

        [Factory]
        public EdgeListGraph<int, Edge<int>> ParralelEdges()
        {
            EdgeListGraph<int, Edge<int>> g = new EdgeListGraph<int, Edge<int>>();
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(0, 1));
            return g;
        }
    }
}
