using Windows.Storage;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class ApplicationDataContainerExtensions
	{
		public static void SetValue<T>(this ApplicationDataContainer container, string key, T value)
		{
			container.Values[key] = value;
		}

		public static T GetValue<T>(this ApplicationDataContainer container, string key, T defaultValue)
		{
			if (container.Values[key] == null)
				return defaultValue;

			return (T)container.Values[key];
		}

		public static void SetValue<T>(this ApplicationDataContainer container, string containerName, string key, T value)
		{
			var childContainer = container.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
			childContainer.SetValue(key, value);
		}

		public static T GetValue<T>(this ApplicationDataContainer container, string containerName, string key, T defaultValue)
		{
			ApplicationDataContainer childContainer;

			if (container.Containers.TryGetValue(containerName, out childContainer))
			{
				return childContainer.GetValue(key, defaultValue);
			}

			return defaultValue;
		}
	}
}
