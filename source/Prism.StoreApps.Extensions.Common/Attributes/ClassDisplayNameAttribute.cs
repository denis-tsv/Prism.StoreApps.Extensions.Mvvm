using System;
using System.Globalization;
using System.Reflection;

namespace Prism.StoreApps.Extensions.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
	public class ClassDisplayNameAttribute : Attribute
	{
		public ClassDisplayNameAttribute(string displayName)
		{
			PluralDisplayName = SingularDisplayName = displayName;
		}

		public ClassDisplayNameAttribute(string singularDisplayName, string pluralDisplayName)
		{
			SingularDisplayName = singularDisplayName;
			PluralDisplayName = pluralDisplayName;
		}

		public ClassDisplayNameAttribute(string singularDisplayName, Type resourceType)
		{
			ResourceType = resourceType;

			PluralDisplayName = SingularDisplayName = SetResourceByProperty(singularDisplayName);
		}

		public ClassDisplayNameAttribute(string singularDisplayName, string pluralDisplayName, Type resourceType)
		{
			ResourceType = resourceType;

			PluralDisplayName = SetResourceByProperty(pluralDisplayName);
			SingularDisplayName = SetResourceByProperty(singularDisplayName);
		}

		public string PluralDisplayName { get; private set; }
		public string SingularDisplayName { get; private set; }
		private Type ResourceType { get; set; }

		private string SetResourceByProperty(string displayName)
		{
			if (ResourceType == null || string.IsNullOrEmpty(displayName))
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "ResourceType and SingularDisplayName are required"));

			PropertyInfo property = ResourceType.GetRuntimeProperty(displayName);
			if (property != null)
			{
				MethodInfo getMethod = property.GetMethod;
				if (getMethod == null || !getMethod.IsAssembly && !getMethod.IsPublic)
					property = null;
			}
			if (property == null)
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "ResourceType has not specified property"));
			if (property.PropertyType != typeof(string))
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Property type is not String"));

			return property.GetValue(null, null).ToString();
		}
	}
}
