using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Focus;
using Microsoft.Pex.Framework.Validation;
using QuickGraph;

[assembly: PexAssemblyUnderTest(typeof(QuickGraph.GraphColor))]
[assembly: PexAllowedExceptionFromAssembly(
    typeof(ArgumentException),
    "QuickGraph",
    AcceptExceptionSubtypes = true)]

[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
//[assembly: PexGenericArguments(typeof(int), typeof(Edge<int>))]
//[assembly: PexGenericArguments(typeof(int), typeof(SEdge<int>))]
