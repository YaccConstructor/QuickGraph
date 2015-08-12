using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz.Dot
{
    public struct GraphvizColor
        : IEquatable<GraphvizColor>
    {
        readonly byte a;
        readonly byte r;
        readonly byte g;
        readonly byte b;

        public GraphvizColor(
            byte a, byte r, byte g, byte b)
        {
            Contract.Requires(a >= 0);
            Contract.Requires(r >= 0);
            Contract.Requires(g >= 0);
            Contract.Requires(b >= 0);

            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public byte A { get { return this.a; } }
        public byte R { get { return this.r; } }
        public byte G { get { return this.g; } }
        public byte B { get { return this.b; } }

        public static GraphvizColor Black
        {
            get { return new GraphvizColor(0xFF, 0, 0, 0); }
        }

        public static GraphvizColor White
        {
            get { return new GraphvizColor(0xFF, 0xFF, 0xFF, 0xFF); }
        }

        public static GraphvizColor LightYellow
        {
            get { return new GraphvizColor(0xFF, 0xFF, 0xFF, 0xE0); }
        }

        public bool Equals(GraphvizColor other)
        {
            return this.a == other.a && this.r == other.r && this.g == other.g && this.b == other.b;
        }

        public override int GetHashCode()
        {
            return (int)(this.a << 24 | this.r << 16 | this.g << 8 | this.b);
        }
    }
}
