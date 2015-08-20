using System;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class FactoryAttribute : Attribute
    {
        private Type factoredType = null;

        public FactoryAttribute() { }
        public FactoryAttribute(Type factoredType)
        {
            if (factoredType == null)
                throw new ArgumentNullException("factoredType");
            this.factoredType = factoredType;
        }

        public Type FactoredType
        {
            get
            {
                return this.factoredType;
            }
            set
            {
                this.factoredType = value;
            }
        }
    }
}
