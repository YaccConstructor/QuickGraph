using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Pex.TestFrameworks;
using Microsoft.ExtendedReflection.Metadata;
using System.CodeDom;
using Microsoft.ExtendedReflection.Utilities;
using QuickGraph.Unit.Core;
using Microsoft.ExtendedReflection.Metadata.Names;

namespace QuickGraph.Unit.Pex
{
    public sealed class QuickGraphTestFramework : AttributeBasedTestFramework
    {
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

        public override bool FixtureSetUpTearDownInstance
        {
            get { return false; }
        }

        public override TypeName FixtureTearDownAttribute
        {
            get { return Metadata<QuickGraph.Unit.TestFixtureTearDownAttribute>.SerializableName; }
        }

        public override CodeAttributeDeclarationCollection GetIgnoreAttributes(string message)
        {
            return new CodeAttributeDeclarationCollection(
                new CodeAttributeDeclaration[]{
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.IgnoreAttribute)),
                        new CodeAttributeArgument(
                            CodeDomHelper.Expr.Prim(message)
                            )
                    )
                });
        }

        public override CodeNamespaceImport[] GetImports()
        {
            return new CodeNamespaceImport[]{
                new CodeNamespaceImport(typeof(QuickGraph.Unit.TestFixtureAttribute).Namespace)
            };
        }

        public override CodeAttributeDeclarationCollection GetTestClassAttributes()
        {
            return new CodeAttributeDeclarationCollection(
                new CodeAttributeDeclaration[]{
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.TestFixtureAttribute))
                        )
                    }
                );
        }

        public override CodeAttributeDeclarationCollection GetTestMethodAttributes(Microsoft.Pex.Execution.PexGeneratedTest test)
        {
            return new CodeAttributeDeclarationCollection(
                new CodeAttributeDeclaration[]{
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.TestAttribute))
                        )
                    }
                );
        }

        public override TypeName IgnoreAttribute
        {
            get { return Metadata<QuickGraph.Unit.IgnoreAttribute>.SerializableName; }
        }

        protected override string IgnoreMessageProperty
        {
            get { return "Message"; }
        }

        public override void MarkExpectedException(System.CodeDom.CodeMemberMethod method, Type exceptionType)
        {
            if (exceptionType == typeof(ArgumentNullException))
                method.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.ExpectedArgumentNullExceptionAttribute))
                        )
                    );
            else if (exceptionType == typeof(ArgumentException))
                method.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.ExpectedArgumentExceptionAttribute))
                        )
                    );
            else
                method.CustomAttributes.Add(
                    new CodeAttributeDeclaration(
                        CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.ExpectedExceptionAttribute)),
                        new CodeAttributeArgument(
                            CodeDomHelper.Expr.TypeOf(exceptionType)
                            )
                        )
                    );
        }

        public override void MarkRollback(System.CodeDom.CodeMemberMethod method)
        {
            method.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    CodeDomHelper.ToTypeReference(typeof(QuickGraph.Unit.RollbackAttribute))
                )
            );
        }

        public override string Name
        {
            get { return "QuickGraph"; }
        }

        public override TypeName SetUpAttribute
        {
            get { return Metadata<QuickGraph.Unit.SetUpAttribute>.SerializableName; }
        }

        public override bool SupportPartialClasses
        {
            get { return true; }
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
            names = new string[]{};
            return false;
        }

        public override bool TryReadExpectedException(
            ICustomAttributeProviderEx target, 
            out TypeEx exceptionType)
        {
            object[] attributes = target.GetAttributeValues(
                Metadata<ExpectedExceptionAttribute>.Type, true);
            if (attributes != null && attributes.Length > 0)
            {
                ExpectedExceptionAttribute attribute = attributes[0] as ExpectedExceptionAttribute;
                if (attribute != null)
                {
                    exceptionType = MetadataFromReflection.GetType(attribute.ExpectedExceptionType);
                    return true;
                }
            }

            exceptionType = null;
            return false;
        }

        public override bool TryReadRollback(ICustomAttributeProviderEx target)
        {
            object[] attributes = target.GetAttributeValues(
                Metadata<ExpectedExceptionAttribute>.Type, true);
            return attributes != null && attributes.Length > 0;
        }

        public override bool TryGetAssemblySetupTeardownMethods(AssemblyEx assembly, out Method setup, out Method teardown)
        {
            setup = teardown = null;
            return false;
        }
    }
}
