namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Text.RegularExpressions;

    public class GraphvizRecordEscaper
    {
        private Regex m_EscapeRegExp = new Regex("(?<Eol>\\n)|(?<Common>\\[|\\]|\\||<|>|\"| )", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);

        public string Escape(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            return this.m_EscapeRegExp.Replace(text, new System.Text.RegularExpressions.MatchEvaluator(this.MatchEvaluator));
        }

        public string MatchEvaluator(Match m)
        {
            if (m.Groups["Common"] != null)
            {
                return string.Format(@"\{0}", m.Value);
            }
            return @"\n";
        }
    }
}

