using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Focus;
using Microsoft.Pex.Framework.Validation;
using QuickGraph.Unit.Pex;

[assembly: PexAssemblyUnderTest(typeof(QuickGraph.GraphColor))]
[assembly: QuickGraphPackage]
[assembly: PexAssemblySettings(
    TestFramework = "QuickGraph")]
[assembly: PexAllowedExceptionFromAssembly(
    typeof(ArgumentException),
    "QuickGraph",
    AcceptExceptionSubtypes = true)]
