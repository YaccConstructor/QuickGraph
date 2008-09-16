using System;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph;
using Microsoft.Pex.Framework;

namespace QuickGraph
{
    public class BidirectionalMatrixGraphFactory
    {
        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> Empty()
        {
            return new BidirectionalMatrixGraph<Edge<int>>(10);
        }

        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> OneEdge()
        {
            BidirectionalMatrixGraph < Edge<int> > g= new BidirectionalMatrixGraph<Edge<int>>(10);
            g.AddEdge(new Edge<int>(0, 1));
            return g;
        }

        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> TwoDisjointEdges()
        {
            BidirectionalMatrixGraph<Edge<int>> g = new BidirectionalMatrixGraph<Edge<int>>(10);
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(5, 6));
            return g;
        }

        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> TwoConnectedEdges()
        {
            BidirectionalMatrixGraph<Edge<int>> g = new BidirectionalMatrixGraph<Edge<int>>(10);
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(1, 2));
            return g;
        }

        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> SelfEdge()
        {
            BidirectionalMatrixGraph<Edge<int>> g = new BidirectionalMatrixGraph<Edge<int>>(10);
            g.AddEdge(new Edge<int>(1, 1));
            return g;
        }

        [Factory]
        public BidirectionalMatrixGraph<Edge<int>> Loop()
        {
            BidirectionalMatrixGraph<Edge<int>> g = new BidirectionalMatrixGraph<Edge<int>>(10);
            g.AddEdge(new Edge<int>(0, 1));
            g.AddEdge(new Edge<int>(1, 0));
            return g;
        }
    }
}
