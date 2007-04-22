using System;

namespace QuickGraph.CommandLine
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class DefaultArgumentAttribute : ArgumentAttribute
	{
		public DefaultArgumentAttribute(string description)
		{
			this.Description = description;
		}

		public override bool IsDefault
		{
			get { return true; }
		}
	}
}
