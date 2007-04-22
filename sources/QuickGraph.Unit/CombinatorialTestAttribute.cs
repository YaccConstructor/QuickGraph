using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;
using System.IO;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false, Inherited =true)]
    public sealed class CombinatorialTestAttribute : TestAttributeBase
    {
        private CombinationType combination = CombinationType.PairWize;

        public CombinatorialTestAttribute() { }
        public CombinatorialTestAttribute(CombinationType combination)
        {
            this.Combination = combination;
        }

        public CombinationType Combination
        {
            get { return this.combination; }
            set { this.combination = value; }
        }

        public override IEnumerable<ITestCase> CreateTests(
            IFixture fixture,
            MethodInfo method
            )
        {
            // check parameters
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length < 1)
                throw new InvalidOperationException("Method "+method.Name+" has not enough parameters");

            // create the domains for each parameter
            List<IDomain> domains = new List<IDomain>();
            Type[] parameterTypes = new Type[parameters.Length];
            for (int index = 0; index < parameters.Length; ++index)
            {
                ParameterInfo parameter = parameters[index];
                parameterTypes[index] = parameter.ParameterType;

                // get domains for parameter
                List<IDomain> pdomains = new List<IDomain>();
                foreach (IParameterDomainFactory parameterDomainFactory 
                    in parameter.GetCustomAttributes(typeof(UsingAttributeBase), true))
                {
                    try
                    {
                        parameterDomainFactory.CreateDomains(pdomains, parameter, fixture);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Failed while loading domains from parameter " + parameter.Name,ex);
                    }
                }
                if (pdomains.Count == 0)
                    throw new ApplicationException("Could not find domain for argument " + parameter.Name);

                domains.Add(Domains.ToDomain(pdomains));
            }

            // we make a cartesian product of all those
            foreach (ITuple tuple in Products.Cartesian(domains))
            {
                // create data domains
                List<IDomain> tdomains = new List<IDomain>();
                for (int i = 0; i < tuple.Count; ++i)
                {
                    IDomain dm = (IDomain)tuple[i];
                    tdomains.Add(dm);
                }

                // computing the pairwize product
                IEnumerable<ITuple> ptproducts = Products.ComputeTupleProducts(tdomains, this.Combination);

                foreach (ITuple ptuple in ptproducts)
                {
                    // tuple is valid, adding test
                    CombinatorialMethodTestCase test =
                        new CombinatorialMethodTestCase(
                            fixture.Name,
                            method, 
                            tuple, 
                            ptuple);

                    yield return test;
                }
            }
        }

        private sealed class CombinatorialMethodTestCase : MethodTestCase
        {
            private ITuple tupleDomains;
            private ITuple tuple;

            public CombinatorialMethodTestCase(
                string fixtureName,
                MethodInfo method,
                ITuple tupleDomains,
                ITuple tuple
                )
                : base(fixtureName, method)
            {
                if (tupleDomains == null)
                    throw new ArgumentNullException("tupleDomains");
                if (tuple == null)
                    throw new ArgumentNullException("tuple");

                this.tupleDomains = tupleDomains;
                this.tuple = tuple;

                foreach (Object parameter in tuple)
                    this.Parameters.Add(new TestCaseParameter(parameter));
            }

            public override string Name
            {
                get
                {
                    StringWriter sw = new StringWriter();
                    for (int i = 0; i < this.tupleDomains.Count; ++i)
                    {
                        IDomain dm = (IDomain)this.tupleDomains[i];
                        if (dm.Name != null)
                            sw.Write("{0}({1}),", dm.Name, this.tuple[i]);
                        else
                            sw.Write("{0},", this.tuple[i]);
                    }
                    return String.Format("{0}({1})", this.Method.Name, sw.ToString().TrimEnd(','));
                }
            }
        }
    }
}
