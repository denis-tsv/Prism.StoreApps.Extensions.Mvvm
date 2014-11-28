using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Modules
{
	public interface IModule
	{
		void Load(IUnityContainer container);
		Task Initialize(IUnityContainer container);
	}
}
