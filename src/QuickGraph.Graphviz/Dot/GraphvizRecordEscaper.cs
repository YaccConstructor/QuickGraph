namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Text.RegularExpressions;
    using System.Diagnostics.Contracts;

    public sealed class GraphvizRecordEscaper
    {
        private Regex escapeRegExp = new Regex("(?<Eol>\\n)|(?<Common>\\[|\\]|\\||<|>|\"| )", RegexOptions.ExplicitCapture | RegexOptions.Multiline);

        public string Escape(string text)
        {
            Contract.Requires(text != null);

            return this.escapeRegExp.Replace(text, new System.Text.RegularExpressions.MatchEvaluator(this.MatchEvaluator));
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

