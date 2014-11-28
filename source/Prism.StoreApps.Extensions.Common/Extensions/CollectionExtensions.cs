using System.Collections.Generic;
using Windows.Foundation.Collections;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class CollectionExtensions
	{
		public static T Get<T>(this IPropertySet propertySet, string key, T defaultValue)
		{
			if (!propertySet.ContainsKey(key)) return defaultValue;

			return (T)propertySet[key];
		}

		public static void AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] = value;
			}
			else
			{
				dictionary.Add(key, value);
			}
		}
	}
}
