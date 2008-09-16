using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true, Inherited=true)]
    public sealed class RowAttribute : Attribute
    {
        private object[] arguments;

        public RowAttribute(object a1)
        {
            this.arguments = new object[] { a1 };
        }
        public RowAttribute(object a1, object a2)
        {
            this.arguments = new object[] { a1 , a2};
        }
        public RowAttribute(object a1, object a2, object a3)
        {
            this.arguments = new object[] { a1, a2, a3 };
        }
        public RowAttribute(object a1, object a2, object a3, object a4)
        {
            this.arguments = new object[] { a1, a2, a3,a4 };
        }
        public RowAttribute(object a1, object a2, object a3, object a4, object a5)
        {
            this.arguments = new object[] { a1, a2, a3, a4,a5 };
        }
        public RowAttribute(object a1, object a2, object a3, object a4, object a5, object a6)
        {
            this.arguments = new object[] { a1, a2, a3, a4, a5,a6 };
        }
        public RowAttribute(object a1, object a2, object a3, object a4, object a5, object a6, object a7)
        {
            this.arguments = new object[] { a1, a2, a3, a4, a5, a6,a7 };
        }
        public RowAttribute(object a1, object a2, object a3, object a4, object a5, object a6, object a7, object a8)
        {
            this.arguments = new object[] { a1, a2, a3, a4, a5, a6, a7, a8 };
        }

        public object[] GetArguments()
        {
            return arguments;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class RowTestAttribute : TestAttributeBase
    {
        public override IEnumerable<ITestCase> CreateTests(IFixture fixture, System.Reflection.MethodInfo method)
        {
            foreach (RowAttribute rowAttribute in method.GetCustomAttributes(typeof(RowAttribute), true))
            {
                yield return new RowTestCase(fixture.Name, method, rowAttribute);
            }
        }

        public sealed class RowTestCase : MethodTestCase
        {
            public RowTestCase(string fixtureName, MethodInfo method, RowAttribute row)
                :base(fixtureName, method)
            {
                foreach (object parameter in row.GetArguments())
                {
                    this.Parameters.Add(new TestCaseParameter(parameter));
                }
            }
        }
    }
}
