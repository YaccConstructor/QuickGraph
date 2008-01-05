using System;
using System.Collections.Generic;
using System.Text;

#if !PEX
namespace Microsoft.Pex.Framework {
    public class PexClassAttribute : Attribute 
    {
        public int MaxBranchHits;
        public int MaxRuns;
    }
    public class PexMethodAttribute : Attribute { }
    public class PexFactoryAttribute : Attribute 
    {
        public PexFactoryAttribute() { }
        public PexFactoryAttribute(Type target) { }
    }
    public class PexTargetAttribute : Attribute { }
    public class PexAssumeIsNotNullAttribute : Attribute { }
}

#endif