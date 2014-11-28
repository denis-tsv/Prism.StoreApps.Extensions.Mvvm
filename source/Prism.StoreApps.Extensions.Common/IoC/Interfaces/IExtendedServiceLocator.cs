using Microsoft.Practices.ServiceLocation;

namespace Prism.StoreApps.Extensions.Common.IoC.Interfaces
{
	public interface IExtendedServiceLocator : IServiceLocator
	{
		/// <summary>
		/// Perform injection of dependencies for <paramref name="obj" />
		/// </summary>
		T BuildUp<T>(T obj);
	}
}
