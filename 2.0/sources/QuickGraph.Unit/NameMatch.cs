using System;
using System.Text.RegularExpressions;

namespace QuickGraph.Unit
{
    public enum NameMatchType
    {
        Exact,
        Contains,
        Regex
    }

    public interface INameMatcher
    {
        bool IsMatch(string value);
    }

    public static class NameMatcherFactory
    {
        public static  INameMatcher CreateMatcher(NameMatchType matchType, string name)
        {
            switch (matchType)
            {
                case NameMatchType.Exact: return new ExactNameMatcher(name);
                case NameMatchType.Contains: return new ContainsNameMatcher(name);
                case NameMatchType.Regex: return new RegexNameMatcher(name);
                default: throw new NotSupportedException(matchType.ToString());
            }
        }

        private sealed class ExactNameMatcher : INameMatcher
        {
            string name;
            public ExactNameMatcher(string name)
            {
                this.name = name;
            }

            public bool IsMatch(string value)
            {
                return this.name.Equals(value);
            }
        }


        private sealed class ContainsNameMatcher : INameMatcher
        {
            string name;
            public ContainsNameMatcher(string name)
            {
                this.name = name;
            }

            public bool IsMatch(string value)
            {
                return this.name.Contains(value);
            }
        }

        private sealed class RegexNameMatcher : INameMatcher
        {
            private Regex rx;
            public RegexNameMatcher(string name)
            {
                rx = new Regex(name, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }

            public bool IsMatch(string name)
            {
                return rx.IsMatch(name);
            }
        }
    }
}
