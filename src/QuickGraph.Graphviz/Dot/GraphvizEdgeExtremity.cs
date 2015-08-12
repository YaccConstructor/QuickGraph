namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    public class GraphvizEdgeExtremity
    {
        private bool isClipped;
        private bool isHead;
        private string label;
        private string logical;
        private string same;
        private string tooltip;
        private string url;

        public GraphvizEdgeExtremity(bool isHead)
        {
            this.isHead = isHead;
            this.url = null;
            this.isClipped = true;
            this.label = null;
            this.tooltip = null;
            this.logical = null;
            this.same = null;
        }

        public void AddParameters(IDictionary<string, object> dic)
        {
            Contract.Requires(dic != null);
            
            string text = null;
            if (this.IsHead)
            {
                text = "head";
            }
            else
            {
                text = "tail";
            }
            if (this.Url != null)
            {
                dic.Add(text + "URL", this.Url);
            }
            if (!this.IsClipped)
            {
                dic.Add(text + "clip", this.IsClipped);
            }
            if (this.Label != null)
            {
                dic.Add(text + "label", this.Label);
            }
            if (this.ToolTip != null)
            {
                dic.Add(text + "tooltip", this.ToolTip);
            }
            if (this.Logical != null)
            {
                dic.Add("l" + text, this.Logical);
            }
            if (this.Same != null)
            {
                dic.Add("same" + text, this.Same);
            }
        }

        public bool IsClipped
        {
            get
            {
                return this.isClipped;
            }
            set
            {
                this.isClipped = value;
            }
        }

        public bool IsHead
        {
            get
            {
                return this.isHead;
            }
        }

        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
            }
        }

        public string Logical
        {
            get
            {
                return this.logical;
            }
            set
            {
                this.logical = value;
            }
        }

        public string Same
        {
            get
            {
                return this.same;
            }
            set
            {
                this.same = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.tooltip;
            }
            set
            {
                this.tooltip = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }
    }
}

