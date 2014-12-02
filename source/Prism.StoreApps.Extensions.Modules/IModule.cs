using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Modules
{
	public interface IModule
	{
		void RegisterServices(IUnityContainer container);
		Task InitializeAsync(IUnityContainer container);
	}
}
