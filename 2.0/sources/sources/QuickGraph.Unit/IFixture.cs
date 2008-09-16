using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace QuickGraph.Unit
{
    public interface IFixture
    {
        string Name { get;}
        ApartmentState Apartment { get;}
        int TimeOut { get;}
        string Description { get;}
        bool IsCurrent { get;}

        IEnumerable<ITestCase> CreateTestCases();
        IList<ITestCaseDecorator> TestCaseDecorators { get;}
        IList<string> Categories { get;}

        Object CreateInstance();

        MethodInfo FixtureSetUp { get;set;}
        MethodInfo SetUp { get;set;}
        MethodInfo TearDown { get;set;}
        MethodInfo FixtureTearDown { get;set;}
    }
}
