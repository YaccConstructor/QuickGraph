using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz.Dot
{
    [DebuggerDisplay("{Width}x{Height}")]
    public struct GraphvizSizeF
    {
        readonly float height;
        readonly float width;

        public float Height
        {
            get { return this.height; }
        }
        public float Width
        {
            get { return this.width; }
        }

        public GraphvizSizeF(float width, float height)
        {
            Contract.Requires(width >= 0);
            Contract.Requires(height >= 0);

            this.width = width;
            this.height = height;
        }

        public bool IsEmpty
        {
            get { return this.width == 0 || this.height == 0; }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}x{1}", this.width, this.height);
        }
    }

    [DebuggerDisplay("{Width}x{Height}")]
    public struct GraphvizSize
    {
        readonly int height;
        readonly int width;

        public int Height
        {
            get { return this.height; }
        }
        public int Width
        {
            get { return this.width; }
        }

        public GraphvizSize(int width, int height)
        {
            Contract.Requires(width >= 0);
            Contract.Requires(height >= 0);

            this.width = width;
            this.height = height;
        }

        public bool IsEmpty
        {
            get { return this.width == 0 || this.height == 0; }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}x{1}", this.width, this.height);
        }
    }
}
