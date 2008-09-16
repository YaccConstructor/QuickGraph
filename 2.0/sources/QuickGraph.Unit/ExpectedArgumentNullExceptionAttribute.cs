using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false, Inherited =true)]
    public sealed class ExpectedArgumentNullExceptionAttribute : ExpectedExceptionAttribute
    {
        public ExpectedArgumentNullExceptionAttribute()
            :base(typeof(ArgumentNullException))
        {}
    }
}
