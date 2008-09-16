using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Filters
{
    public sealed class ScopeFixtureFilter : IFixtureFilter
    {
        private List<string> scopes = new List<string>();

        public ScopeFixtureFilter(IEnumerable<String> scopes)
        {
            this.scopes.AddRange(scopes);
        }

        public IList<string> Scopes
        {
            get { return this.scopes; }
        }

        public bool Filter(IFixture fixture)
        {
            foreach (string scope in scopes)
                if (fixture.Name.ToLower().StartsWith(scope.ToLower()))
                    return true;
            return false;
        }
    }
}
