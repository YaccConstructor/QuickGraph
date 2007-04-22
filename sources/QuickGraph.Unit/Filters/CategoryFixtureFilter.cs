using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Filters
{
    public sealed class CategoryFixtureFilter : IFixtureFilter
    {
        private string categories;

        public CategoryFixtureFilter(IEnumerable<string> categories)       
        {
            this.categories = "";
            foreach (string category in categories)
                this.categories += ";"+category.ToLower();
        }

        public bool Filter(IFixture fixture)
        {
            foreach (string category in fixture.Categories)
            {
                if (this.categories.Contains(category.ToLower()))
                    return true;
            }

            return false;
        }
    }
}
