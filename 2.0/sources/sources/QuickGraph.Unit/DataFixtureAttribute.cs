using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public sealed class DataFixtureAttribute : TestFixtureAttributeBase
    {
        public override IEnumerable<IFixture> CreateFixtures(Type fixtureType)
        {
            foreach(DataProviderAttributeBase dataProviderAttribute 
                in fixtureType.GetCustomAttributes(typeof(DataProviderAttributeBase),true))
            {
                yield return new DataFixture(
                    this,
                    fixtureType,
                    dataProviderAttribute);
            }
        }

        private sealed class DataFixture : TypeFixtureBase<DataFixtureAttribute>
        {
            private IDataProvider dataProvider;

            public DataFixture(
                DataFixtureAttribute attribute, 
                Type fixtureType,
                IDataProvider dataProvider
                )
                : base(attribute, fixtureType)
            {
                this.dataProvider = dataProvider;
            }

            public IDataProvider DataProvider
            {
                get { return this.dataProvider; }
            }

            public override IEnumerable<ITestCase> CreateTestCases()
            {
                // load data
                XPathNavigator navigator = this.DataProvider.GetData().CreateNavigator();

                foreach(MethodInfo method in ReflectionHelper.GetMethods(
                    this.FixtureType, typeof(DataTestAttribute)))
                {
                    foreach (DataTestAttribute dataTestAttribute in
                        method.GetCustomAttributes(typeof(DataTestAttribute), true))
                    {
                        int i = 0;
                        foreach(XPathNavigator node in navigator.Select(dataTestAttribute.Select))
                        {
                            DataTestCase test = new DataTestCase(
                                this.Name, 
                                method, 
                                node,
                                i.ToString()
                                );
                            i++;
                            yield return test;
                        }
                    }
                }
            }

            public override String Name
            {
                get
                {
                    return String.Format("{0}.{1}",base.Name, this.DataProvider.Name);
                }
            }
        }
    }
}
