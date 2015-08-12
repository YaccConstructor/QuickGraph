using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QuickGraph.Heap
{
    /// <summary>
    /// A generic string filtering interface
    /// </summary>
    internal interface IFilter
    {
        /// <summary>
        /// Gets a value indicating wheter the value is matched by
        /// the filter
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool Match(string value);
    }

    /// <summary>
    /// Static helper methods to create simple string filters
    /// </summary>
    internal static class FilterHelper
    {
        /// <summary>
        /// Creates a regex from a string, supporting
        /// * and ? wildcards.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFilter ToFilter(string name)
        {
            if (String.IsNullOrEmpty(name))
                return new AnyFilter();

            foreach (char c in name)
                switch (c)
                {
                    case ',':
                    case ';':
                    case '?':
                    case '*':
                        // this code will trigger a lot of jitting/computation
                        string rx = Regex.Escape(name);
                        // ,; -> |
                        rx = rx.Replace(',', '|').Replace(';', '|');
                        // ? -> .
                        rx = Regex.Replace(rx, @"\?", ".");
                        // * -> .*?
                        rx = Regex.Replace(rx, @"\*", ".*?");
                        return new RegexFilter(new Regex(rx));
                }
            return new ContainsFilter(name);
        }

        private sealed class AnyFilter : IFilter
        {
            public bool Match(string value)
            {
                return true;
            }

            public override string ToString()
            {
                return "any";
            }
        }

        private sealed class RegexFilter : IFilter
        {
            private readonly Regex rx;
            public RegexFilter(Regex rx)
            {
                this.rx = rx;
            }

            public bool Match(string value)
            {
                if (String.IsNullOrEmpty(value))
                    return false;

                Match m = rx.Match(value);
                return m.Success;
            }

            public override string ToString()
            {
                return String.Format("regex {0}", rx);
            }
        }

        private sealed class ContainsFilter : IFilter
        {
            private readonly string substring;
            public ContainsFilter(string substring)
            {
                this.substring = substring;
            }

            public bool Match(string value)
            {
                if (String.IsNullOrEmpty(value))
                    return false;

                return value.Contains(substring);
            }

            public override string ToString()
            {
                return String.Format("substring '{0}'", this.substring);
            }
        }
    }
}
