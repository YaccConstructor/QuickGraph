using System;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public interface ITestCase
    {
        string FixtureName { get;}
        string Name { get;}
        string FullName { get;}

        IList<TestCaseParameter> Parameters { get;}

        void Run(Object fixture);
    }
}
