using System.Threading.Tasks;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces
{
	public interface IChildViewModel : IViewModel
	{
		Task ShowAsync();
	}
}