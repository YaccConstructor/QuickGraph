using System;
using System.Collections.Generic;

namespace QuickGraph.Unit.Filters
{
    public sealed class ScopeTestCaseFilter : ITestCaseFilter
    {
        private List<string> scopes = new List<string>();

        public ScopeTestCaseFilter(IEnumerable<string> scopes)
        {
            this.scopes.AddRange(scopes);
        }

        public IList<string> Scopes
        {
            get { return this.scopes; }
        }

        public bool Filter(IFixture fixture, ITestCase test)
        {
            foreach (string scope in scopes)
                if (test.Name.ToLower().StartsWith(scope.ToLower()))
                    return true;
            return false;
        }
    }
}
