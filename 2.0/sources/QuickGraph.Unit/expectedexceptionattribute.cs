using System;
using System.IO;
using System.Reflection;
using QuickGraph.Unit.Exceptions;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExpectedExceptionAttribute : TestDecoratorAttributeBase
    {
        private Type expectedExceptionType;
        private NameMatchType messageMatchType = NameMatchType.Contains;
        private string messagePattern = null;

        public ExpectedExceptionAttribute(Type expectedExceptionType)
        {
            if (expectedExceptionType == null)
                throw new ArgumentNullException("expectedExceptionType");
            this.expectedExceptionType = expectedExceptionType;
        }

        public Type ExpectedExceptionType
        {
            get { return this.expectedExceptionType; }
        }

        public NameMatchType MessageMatchType
        {
            get { return this.messageMatchType; }
            set { this.messageMatchType = value; }
        }

        public string MessagePattern
        {
            get { return this.messagePattern; }
            set { this.messagePattern = value; }
        }

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new ExpectedExceptionTestCase(testCase, this);
        }

        private sealed class ExpectedExceptionTestCase : TypeDecoratorTestCaseBase<ExpectedExceptionAttribute>
        {
            public ExpectedExceptionTestCase(ITestCase testCase, ExpectedExceptionAttribute attribute)
                : base(testCase, attribute)
            {}

            public override string Name
            {
                get { return String.Format("{0}({1})", 
                    this.TestCase.Name, 
                    this.Attribute.ExpectedExceptionType.Name); }
            }

            public override void Run(Object fixture)
            {
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.TestCase.Run(fixture);
                }
                catch (Exception ex)
                {
                    Exception current = ex;
                    // check if current expection is expecetd or ignored
                    while (current != null)
                    {
                        if (current.GetType() == this.Attribute.ExpectedExceptionType)
                        {
                            VerifyException(current);
                            return;
                        }
                        if (current.GetType() == typeof(IgnoreException))
                        {
                            VerifyException(current);
                            return;
                        }
                        current = current.InnerException;
                    }
                    current = ex;
                    if (current is TargetInvocationException)
                        current = current.InnerException;
                    throw new ExceptionTypeMistmatchException(this.Attribute.ExpectedExceptionType, current);
                }
                throw new ExceptionNotThrowedException(this.Attribute.ExpectedExceptionType);
            }

            private void VerifyException(Exception ex)
            {
                if (String.IsNullOrEmpty(this.Attribute.MessagePattern))
                    return;

                INameMatcher matcher = NameMatcherFactory.CreateMatcher(
                    this.Attribute.MessageMatchType,
                    this.Attribute.MessagePattern);
                if (!matcher.IsMatch(ex.Message))
                    throw new ExceptionMessageDoesNotMatchException(matcher, ex);
            }
        }
    }
}
