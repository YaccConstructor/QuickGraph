using System;
using System.Collections.Generic;
using System.Reflection;
using QuickGraph.Operations;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public sealed class UsingLinearAttribute : UsingAttributeBase
    {
        private int start;
        private int step;
        private int stepCount;

        public UsingLinearAttribute(int start, int stepCount)
            :this(start,stepCount,1)
        {
        }

        public UsingLinearAttribute(int start, int stepCount, int step)
        {
            this.start = start;
            this.stepCount = stepCount;
            this.step = step;
        }

        public int Start
        {
            get { return this.start; }
        }

        public int StepCount
        {
            get { return this.stepCount; }
        }

        public int Step
        {
            get { return this.step; }
        }

        public override void CreateDomains(
            IList<IDomain> domains,
            ParameterInfo parameter,
            IFixture fixture)
        {
            LinearInt32Domain domain = new LinearInt32Domain(start, stepCount, step);
            domains.Add(domain);
        }
    }
}
