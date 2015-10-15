using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace example

{
    public class MyVertex
    {
        public string Vert;
        public string Taggs;
        public MyVertex(string v, string c)
        { Vert = v; Taggs = c; }
        public MyVertex(string v)
        { Vert = v; Taggs = ""; }

        public static MyVertex TaggsToString(string v, Tuple<string, string>[] c)
        {
            var MV = new MyVertex(v, "  ");
            foreach (var i in c)
            {
                MV.Taggs = MV.Taggs + i.Item1 + "=" + i.Item2 + ". ";
            }
            return MV;
        }
        public string ToStr()
        { return this.Vert + this.Taggs; }

    }
        
    class Program
    {
        public static string MyTaggs(Tuple<string, string>[] t) 
        {
            var str = "";
            foreach (var i in t)
            {
                str = str + i.Item1 + "=" + i.Item2 + ". ";
            
            }
            return str;
        
        
        }
        static void Main()
        {
            Func<string, Tuple<string, string>[], MyVertex> FuncForV = (v, attrs) => MyVertex.TaggsToString(v, attrs);
            Func<string, string, Tuple<string, string>[], SEdge<MyVertex>> FuncForE = 
                (v1, v2, attrs) => new SEdge<MyVertex>(new MyVertex(v1),new MyVertex (v2));

            var str = "strict graph t { 6 [lable = \"v1\"] \n 1[lable = \"v\"] \n 1 -- 2 [weight = 10] \n 2 -- 1 \n 1 [lable = \"vv\"] \n 1 -- 1 [weight = 7] \n 3 --4 \n }";
            
            
            var graph = BidirectionalGraph<MyVertex, SEdge<MyVertex>>.LoadDotString(str, FuncForV, FuncForE);
            //var adr = "..\\test_inputs\\test111.dot";
                        
            Func<string, Tuple<string, string>[], string> f1 = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], SUndirectedEdge<string>> f2 = (v1, v2, attrs) => new SUndirectedEdge<string>(v1, v2);
            var g = BidirectionalGraph<string, SUndirectedEdge<string>>.LoadDotString(str, f1, f2);

            Func<string, Tuple<string, string>[], string> func1 = (v, attrs) => v;
            Func<string, string, Tuple<string, string>[], STaggedEdge<string, string>> func2 = (v1, v2, attrs) => new STaggedEdge<string, string>(v1, v2, MyTaggs(attrs));
            var g2 = BidirectionalGraph<string, STaggedEdge<string, string>>.LoadDotString(str, func1, func2);

            foreach (var i in g2.Edges)
            {
                Console.WriteLine(i);
            }
            foreach (var i in g2.Vertices)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\n     GraphType2\n");
            foreach (var i in g.Edges)
            {
                Console.WriteLine(i);
            }
            foreach (var i in g.Vertices)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\n     GraphType3\n");            
            foreach (var i in graph.Edges)
            {
                Console.WriteLine(i.Source.ToStr()+" -- "+i.Target.ToStr());
            }
            foreach (var i in graph.Vertices)
            {
                Console.WriteLine(i.ToStr());
            }

            
        }
    }
}
