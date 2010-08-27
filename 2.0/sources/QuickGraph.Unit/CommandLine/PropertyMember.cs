using System;
using System.Reflection;

namespace QuickGraph.CommandLine
{
	internal sealed class PropertyMember : IMember
	{
		private PropertyInfo property;

		public PropertyMember(PropertyInfo property)
		{
			this.property = property;
		}

		public string Name
		{
			get { return this.property.Name; }
		}

		public Type DeclaringType
		{
			get { return this.property.DeclaringType; }
		}

		public Type MemberType
		{
			get { return this.property.PropertyType; }
		}

		public object GetValue(object instance)
		{
			return this.property.GetValue(instance, null);
		}

		public void SetValue(object instance, object value)
		{
			this.property.SetValue(instance, value, null);
		}
	}
}
