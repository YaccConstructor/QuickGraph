using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Invariants;
using Microsoft.Pex.TestFrameworks;

[assembly: PexTestFramework(typeof(QuickGraph.Unit.Pex.QuickGraphTestFramework))]
[assembly: PexAssemblySettings(
    TestFramework = "QuickGraph")]
[assembly: PexFromAssembly("QuickGraph")]
[assembly: PexFocusOnAssembly("QuickGraph")]
[assembly: PexInstrumentAssembly("QuickGraph")]
[assembly: PexUseAssembly("QuickGraph")]
[assembly: PexAllowedException(typeof(ArgumentException), AcceptExceptionSubtypes = true, 
    UserAssemblies = "QuickGraph")]
[assembly: PexInvariantCheck]