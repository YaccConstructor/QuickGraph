using System;
using QuickGraph.Unit;
using Microsoft.Pex.Framework;
using QuickGraph;

namespace QuickGraph {
    public class NamedVertex : IdentifiableVertex {
        private string name;
        public NamedVertex(string id)
            : base(id) { }

        [System.Xml.Serialization.XmlAttribute]
        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public sealed class Factory : IIdentifiableVertexFactory<NamedVertex> {
            public NamedVertex CreateVertex(string id) {
                return new NamedVertex(id);
            }
        }
    }

    public class NamedEdge : IdentifiableEdge<NamedVertex> {
        private string name;
        public NamedEdge(string id, NamedVertex source, NamedVertex target)
            : base(id, source, target) { }

        [System.Xml.Serialization.XmlAttribute]
        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }

        public sealed class Factory : IIdentifiableEdgeFactory<NamedVertex, NamedEdge> {
            public NamedEdge CreateEdge(string id, NamedVertex source, NamedVertex target) {
                return new NamedEdge(id, source, target);
            }
        }
    }

    public class AdjacencyGraphFactory
    {
        private static AdjacencyGraph<String, Edge<String>> CreateGraph()
        {
            return new AdjacencyGraph<String, Edge<String>>(false);
        }

        public static Type CreateType()
        {
            return typeof(AdjacencyGraph<int, Edge<int>>);
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> Empty()
        {
            return CreateGraph();
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> NoEdges()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.NoEdges(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> Loop()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.Loop(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> LoopDouble()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.LoopDouble(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> FileDependency()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.FileDependency(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> RegularLattice10x10()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.RegularLattice(10, 10, g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> Simple()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.Simple(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<string, Edge<string>> UnBalancedFlow()
        {
            AdjacencyGraph<string, Edge<string>> g = CreateGraph();
            GraphFactory.UnBalancedFlow(g);
            return g;
        }

        [Factory]
        public AdjacencyGraph<NamedVertex, NamedEdge> SimpleIdentifiable()
        {
            return Convert(Simple());
        }

        private static AdjacencyGraph<NamedVertex, NamedEdge> Convert(AdjacencyGraph<string, Edge<string>> g)
        {
            AdjacencyGraph<NamedVertex, NamedEdge> cg = 
                new AdjacencyGraph<NamedVertex, NamedEdge>();
            System.Collections.Generic.Dictionary<string,NamedVertex> vertices = new System.Collections.Generic.Dictionary<string,NamedVertex>(g.VertexCount);
            foreach (string v in g.Vertices)
            {
                NamedVertex iv = new NamedVertex(v);
                iv.Name = v;
                vertices.Add(v, iv);
                cg.AddVertex(iv);
            }
            int count = 0;
            foreach(Edge<string> e in g.Edges)
            {
                NamedEdge edge = new NamedEdge(
                    count.ToString(),
                    vertices[e.Source],
                    vertices[e.Target]
                    );
                edge.Name = edge.Source.Name + "->" + edge.Target.Name;
                cg.AddEdge(edge);
                count++;
            }
            return cg;
        }
    }
}
