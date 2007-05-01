namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Collections;

    public class GraphvizEdgeExtremity
    {
        private bool m_IsClipped;
        private bool m_IsHead;
        private string m_Label;
        private string m_Logical;
        private string m_Same;
        private string m_ToolTip;
        private string m_Url;

        public GraphvizEdgeExtremity(bool isHead)
        {
            this.m_IsHead = isHead;
            this.m_Url = null;
            this.m_IsClipped = true;
            this.m_Label = null;
            this.m_ToolTip = null;
            this.m_Logical = null;
            this.m_Same = null;
        }

        public void AddParameters(IDictionary dic)
        {
            if (dic == null)
            {
                throw new ArgumentNullException("dic");
            }
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
                return this.m_IsClipped;
            }
            set
            {
                this.m_IsClipped = value;
            }
        }

        public bool IsHead
        {
            get
            {
                return this.m_IsHead;
            }
        }

        public string Label
        {
            get
            {
                return this.m_Label;
            }
            set
            {
                this.m_Label = value;
            }
        }

        public string Logical
        {
            get
            {
                return this.m_Logical;
            }
            set
            {
                this.m_Logical = value;
            }
        }

        public string Same
        {
            get
            {
                return this.m_Same;
            }
            set
            {
                this.m_Same = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.m_ToolTip;
            }
            set
            {
                this.m_ToolTip = value;
            }
        }

        public string Url
        {
            get
            {
                return this.m_Url;
            }
            set
            {
                this.m_Url = value;
            }
        }
    }
}

