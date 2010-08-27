using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.Framework.TestFrameworks;
using Microsoft.Pex.Engine.TestFrameworks;
using Microsoft.Pex.Engine.ComponentModel;
using Microsoft.ExtendedReflection.Metadata.Names;
using Microsoft.ExtendedReflection.Metadata;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Pex
{
    class QuickGraphTestFrameworkAttribute : 
        PexTestFrameworkAttributeBase
    {
        protected override IPexTestFramework CreateTestFramework(IPexComponent host)
        {
            return new TestFramework(host);
        }

        class TestFramework :
            AttributeBasedTestFrameworkBase
        {
            public TestFramework(IPexComponent host)
                :base(host)
            {}

            public override ShortAssemblyName AssemblyName
            {
                get { return ShortAssemblyName.FromAssembly(typeof(QuickGraph.IEdge<>).Assembly); }
            }

            public override TypeName AssertionExceptionType
            {
                get { return Metadata<QuickGraph.Unit.Exceptions.AssertionException>.SerializableName; }
            }

            public override TypeName FixtureAttribute
            {
                get { return Metadata<QuickGraph.Unit.TestFixtureAttribute>.SerializableName; }
            }

            public override TypeName FixtureSetUpAttribute
            {
                get { return Metadata<QuickGraph.Unit.TestFixtureSetUpAttribute>.SerializableName; }
            }

            protected override string GetHintPath(string assemblyName)
            {
                return null;
            }

            public override string RootNamespace
            {
                get { return "QuickGraph.Unit"; }
            }

            public override bool FixtureSetUpTearDownInstance
            {
                get { return false; }
            }

            public override bool SupportsPartialClasses
            {
                get { return true; }
            }

            public override TypeName FixtureTearDownAttribute
            {
                get { return Metadata<QuickGraph.Unit.TestFixtureTearDownAttribute>.SerializableName; }
            }

            public override TypeName IgnoreAttribute
            {
                get { return Metadata<QuickGraph.Unit.IgnoreAttribute>.SerializableName; }
            }

            public override TypeName AssumptionExceptionType
            {
                get { return Metadata<QuickGraph.Unit.Exceptions.AssertionException>.SerializableName; }
            }

            public override TypeName ExpectedExceptionAttribute
            {
                get { return Metadata<QuickGraph.Unit.ExpectedExceptionAttribute>.SerializableName; }
            }

            protected override string IgnoreMessageProperty
            {
                get { return "Message"; }
            }

            public override string Name
            {
                get { return "QuickGraph"; }
            }

            public override TypeName SetUpAttribute
            {
                get { return Metadata<QuickGraph.Unit.SetUpAttribute>.SerializableName; }
            }

            public override bool IsFixture(TypeDefinition target)
            {
                return AttributeHelper.IsDefined(
                    target,
                    Metadata<TestFixtureAttributeBase>.Type);
            }

            public override bool IsTest(MethodDefinition method)
            {
                return AttributeHelper.IsDefined(
                    method,
                    Metadata<TestAttributeBase>.Type);
            }

            public override TypeName TearDownAttribute
            {
                get { return Metadata<QuickGraph.Unit.TearDownAttribute>.SerializableName; }
            }

            public override TypeName TestAttribute
            {
                get { return Metadata<QuickGraph.Unit.TestAttribute>.SerializableName; }
            }

            protected override bool TryGetCategories(
                ICustomAttributeProviderEx element,
                out IEnumerable<string> names)
            {
                names = new string[] { };
                return false;
            }

            public override bool TryGetAssemblySetupTeardownMethods(AssemblyEx assembly, out Method setup, out Method teardown)
            {
                setup = teardown = null;
                return false;
            }

            protected override string ExpectedExceptionProperty
            {
                get { return "ExpectedExceptionType"; }
            }

            protected override bool HasIgnoreAttributeMessage
            {
                get { return true; }
            }
        }
    }
}
