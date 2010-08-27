using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true, Inherited=true)]
    public sealed class DataTestAttribute : TestAttributeBase
    {
        private string select = "//*";
        private Type dataType;

        public DataTestAttribute(string select)
        {
            if (String.IsNullOrEmpty(select))
                throw new ArgumentNullException("select");
            this.select = select;
        }

        public string Select
        {
            get { return this.select; }
            set { this.select = value; }
        }

        public Type DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

        public override IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method)
        {
            yield return null;
        }
    }
}
