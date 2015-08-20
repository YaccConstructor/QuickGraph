using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph
{
    public static class GraphFactory
    {
        public static void NoEdges(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            string x = "x"; g.AddVertex(x);
            string y = "y"; g.AddVertex(y);
            string z = "z"; g.AddVertex(z);
        }

        public static void Loop(IMutableVertexAndEdgeListGraph<string,Edge<string>> g)
        {
            string x = "x"; g.AddVertex(x);
            string y = "y"; g.AddVertex(y);
            string z = "z"; g.AddVertex(z);

            g.AddEdge(new Edge<string>(x, y));
            g.AddEdge(new Edge<string>(y, z));
            g.AddEdge(new Edge<string>(z, x));
        }

        public static void LoopDouble(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            string x = "x"; g.AddVertex(x);
            string y = "y"; g.AddVertex(y);
            string z = "z"; g.AddVertex(z);

            g.AddEdge(new Edge<string>(x, y));
            g.AddEdge(new Edge<string>(y, z));
            g.AddEdge(new Edge<string>(z, x));
            g.AddEdge(new Edge<string>(x, z));
        }


        public static void UnBalancedFlow(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            string x = "x"; g.AddVertex(x);
            string y = "y"; g.AddVertex(y);
            string z = "z"; g.AddVertex(z);
            string w = "w"; g.AddVertex(w);

            g.AddEdge(new Edge<string>(x, y));
            g.AddEdge(new Edge<string>(x, z));
            g.AddEdge(new Edge<string>(y, z));
            g.AddEdge(new Edge<string>(x, w));
            g.AddEdge(new Edge<string>(w, y));
            g.AddEdge(new Edge<string>(w, z));
        }

        public static void Simple(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            string x = "x"; g.AddVertex(x);
            string y = "y"; g.AddVertex(y);
            string z = "z"; g.AddVertex(z);
            string w = "w"; g.AddVertex(w);
            string u = "u"; g.AddVertex(u);
            string v = "v"; g.AddVertex(v);

            g.AddEdge(new Edge<string>(u, x));
            g.AddEdge(new Edge<string>(u, v));
            g.AddEdge(new Edge<string>(w, z));
            g.AddEdge(new Edge<string>(w, y));
            g.AddEdge(new Edge<string>(w, u));
            g.AddEdge(new Edge<string>(x, v));
            g.AddEdge(new Edge<string>(v, y));
            g.AddEdge(new Edge<string>(y, x));
            g.AddEdge(new Edge<string>(z, y));
        }

        public static void FileDependency(IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            // adding files and storing names
            string zig_cpp = "zip.cpp"; g.AddVertex(zig_cpp);
            string boz_h = "boz.h"; g.AddVertex(boz_h);
            string zag_cpp = "zag.cpp"; g.AddVertex(zag_cpp);
            string yow_h = "yow.h"; g.AddVertex(yow_h);
            string dax_h = "dax.h"; g.AddVertex(dax_h);
            string bar_cpp = "bar.cpp"; g.AddVertex(bar_cpp);
            string zow_h = "zow.h"; g.AddVertex(zow_h);
            string foo_cpp = "foo.cpp"; g.AddVertex(foo_cpp);

            string zig_o = "zig.o"; g.AddVertex(zig_o);
            string zag_o = "zago"; g.AddVertex(zag_o);
            string bar_o = "bar.o"; g.AddVertex(bar_o);
            string foo_o = "foo.o"; g.AddVertex(foo_o);
            string libzigzag_a = "libzigzig.a"; g.AddVertex(libzigzag_a);
            string libfoobar_a = "libfoobar.a"; g.AddVertex(libfoobar_a);

            string killerapp = "killerapp.exe"; g.AddVertex(killerapp);

            // adding dependencies
            g.AddEdge(new Edge<string>(dax_h, foo_cpp));
            g.AddEdge(new Edge<string>(dax_h, bar_cpp));
            g.AddEdge(new Edge<string>(dax_h, yow_h));
            g.AddEdge(new Edge<string>(yow_h, bar_cpp));
            g.AddEdge(new Edge<string>(yow_h, zag_cpp));
            g.AddEdge(new Edge<string>(boz_h, bar_cpp));
            g.AddEdge(new Edge<string>(boz_h, zig_cpp));
            g.AddEdge(new Edge<string>(boz_h, zag_cpp));
            g.AddEdge(new Edge<string>(zow_h, foo_cpp));
            g.AddEdge(new Edge<string>(foo_cpp, foo_o));
            g.AddEdge(new Edge<string>(foo_o, libfoobar_a));
            g.AddEdge(new Edge<string>(bar_cpp, bar_o));
            g.AddEdge(new Edge<string>(bar_o, libfoobar_a));
            g.AddEdge(new Edge<string>(libfoobar_a, libzigzag_a));
            g.AddEdge(new Edge<string>(zig_cpp, zig_o));
            g.AddEdge(new Edge<string>(zig_o, libzigzag_a));
            g.AddEdge(new Edge<string>(zag_cpp, zag_o));
            g.AddEdge(new Edge<string>(zag_o, libzigzag_a));
            g.AddEdge(new Edge<string>(libzigzag_a, killerapp));
        }

        public static void RegularLattice(int rows, int columns, IMutableVertexAndEdgeListGraph<string, Edge<string>> g)
        {
            string[,] latice = new string[rows, columns];
            // adding vertices
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    latice[i, j] = String.Format("{0},{1}", i.ToString(), j.ToString());
                    g.AddVertex(latice[i, j]);
                }
            }

            // adding edges
            for (int i = 0; i < rows - 1; ++i)
            {
                for (int j = 0; j < columns - 1; ++j)
                {
                    g.AddEdge(new Edge<string>(latice[i, j], latice[i, j + 1]));
                    g.AddEdge(new Edge<string>(latice[i, j + 1], latice[i, j]));

                    g.AddEdge(new Edge<string>(latice[i, j], latice[i + 1, j]));
                    g.AddEdge(new Edge<string>(latice[i + 1, j], latice[i, j]));
                }
            }

            for (int j = 0; j < columns - 1; ++j)
            {
                g.AddEdge(new Edge<string>(latice[rows - 1, j], latice[rows - 1, j + 1]));
                g.AddEdge(new Edge<string>(latice[rows - 1, j + 1], latice[rows - 1, j]));
            }

            for (int i = 0; i < rows - 1; ++i)
            {
                g.AddEdge(new Edge<string>(latice[i, columns - 1], latice[i + 1, columns - 1]));
                g.AddEdge(new Edge<string>(latice[i + 1, columns - 1], latice[i, columns - 1]));
            }
        }

        public static void Fsm(IMutableVertexAndEdgeListGraph<string,NamedEdge<string>> g)
        {
            string s0 = "S0"; g.AddVertex(s0);
            string s1 = "S1"; g.AddVertex(s1);
            string s2 = "S2"; g.AddVertex(s2);
            string s3 = "S3"; g.AddVertex(s3);
            string s4 = "S4"; g.AddVertex(s4);
            string s5 = "S5"; g.AddVertex(s5);

            g.AddEdge(new NamedEdge<string>(s0, s1,"StartCalc"));

            g.AddEdge(new NamedEdge<string>(s1, s0,"StopCalc"));
            g.AddEdge(new NamedEdge<string>(s1, s1,"SelectStandard"));
            g.AddEdge(new NamedEdge<string>(s1, s1,"ClearDisplay"));
            g.AddEdge(new NamedEdge<string>(s1, s2,"SelectScientific"));
            g.AddEdge(new NamedEdge<string>(s1, s3,"EnterDecNumber"));

            g.AddEdge(new NamedEdge<string>(s2, s1,"SelectStandard"));
            g.AddEdge(new NamedEdge<string>(s2, s2,"SelectScientific"));
            g.AddEdge(new NamedEdge<string>(s2, s2,"ClearDisplay"));
            g.AddEdge(new NamedEdge<string>(s2, s4,"EnterDecNumber"));
            g.AddEdge(new NamedEdge<string>(s2, s5,"StopCalc"));

            g.AddEdge(new NamedEdge<string>(s3, s0,"StopCalc"));
            g.AddEdge(new NamedEdge<string>(s3, s1,"ClearDisplay"));
            g.AddEdge(new NamedEdge<string>(s3, s3,"SelectStandard"));
            g.AddEdge(new NamedEdge<string>(s3, s3,"EnterDecNumber"));
            g.AddEdge(new NamedEdge<string>(s3, s4,"SelectScientific"));

            g.AddEdge(new NamedEdge<string>(s4, s2,"ClearDisplay"));
            g.AddEdge(new NamedEdge<string>(s4, s3,"SelectStandard"));
            g.AddEdge(new NamedEdge<string>(s4, s4,"SelectScientific"));
            g.AddEdge(new NamedEdge<string>(s4, s4,"EnterDecNumber"));
            g.AddEdge(new NamedEdge<string>(s4, s5,"StopCalc"));

            g.AddEdge(new NamedEdge<string>(s5, s2, "StartCalc"));
        }
    }
}
