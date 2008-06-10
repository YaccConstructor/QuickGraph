using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Focus;
using Microsoft.Pex.Framework.Validation;

[assembly: PexAssemblyUnderTest(typeof(QuickGraph.GraphColor))]
[assembly: PexAssemblySettings(
    TestFramework = "QuickGraph")]
[assembly: PexAllowedExceptionFromAssembly(
    typeof(ArgumentException),
    "QuickGraph",
    AcceptExceptionSubtypes = true)]
