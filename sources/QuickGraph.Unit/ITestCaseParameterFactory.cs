using System;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public interface ITestCaseParameterFactory
    {
        IEnumerable<TestCaseParameter> CreateInstances(Type targetType);
    }
}
