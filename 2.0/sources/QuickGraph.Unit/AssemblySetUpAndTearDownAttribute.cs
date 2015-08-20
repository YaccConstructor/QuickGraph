using System;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=false)]
    public sealed class AssemblySetUpAndTearDownAttribute : Attribute
    {
        private Type targetType;
        public AssemblySetUpAndTearDownAttribute(
            Type targetType
            )
        {
            if (targetType == null)
                throw new ArgumentNullException("targetType");
            this.targetType = targetType;
        }

        public Type TargetType
        {
            get { return this.targetType; }
            set { this.targetType = value; }
        }
    }
}
