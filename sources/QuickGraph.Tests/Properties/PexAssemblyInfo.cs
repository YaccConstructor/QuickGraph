using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.Pex.TestFrameworks;

[assembly: PexTestFramework(typeof(QuickGraph.Unit.Pex.QuickGraphTestFramework))]
[assembly: PexAssemblyUnderTest(typeof(QuickGraph.GraphColor))]
[assembly: PexAssemblySettings(
    TestFramework = "QuickGraph")]
[assembly: PexFocusOnAssembly("QuickGraph")]
[assembly: PexInstrumentAssembly("QuickGraph")]
[assembly: PexUseAssembly("QuickGraph")]
[assembly: PexAllowedException(
    typeof(ArgumentException), 
    AcceptExceptionSubtypes = true, 
    UserAssemblies = "QuickGraph")]
