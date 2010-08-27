using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit
{
    public abstract class TestCaseParameterFactoryAttributeBase : Attribute, 
        ITestCaseParameterFactory
    {
        public abstract IEnumerable<TestCaseParameter> CreateInstances(Type targetType);
    }
}
