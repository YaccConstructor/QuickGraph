using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework.Packages;
using QuickGraph.Unit.Pex;

[assembly: QuickGraphTestFramework]

namespace QuickGraph.Unit.Pex
{
    public sealed class QuickGraphPackageAttribute :
        PexPackageAssemblyAttribute
    {
        public QuickGraphPackageAttribute()
            :base(typeof(QuickGraphPackageAttribute))
        { }
    }
}
