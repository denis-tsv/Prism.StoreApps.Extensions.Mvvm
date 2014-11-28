using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.IoC.Interfaces;

namespace Prism.StoreApps.Extensions.Common.IoC
{
	public class ExtendedUnityServiceLocator : UnityServiceLocator, IExtendedServiceLocator
	{
		public ExtendedUnityServiceLocator(IUnityContainer container)
			: base(container)
		{
		}

		public T BuildUp<T>(T obj)
		{
			return (T)_container.BuildUp(obj.GetType(), obj);
		}
	}
}
