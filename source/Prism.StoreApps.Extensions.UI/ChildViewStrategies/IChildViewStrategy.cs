using System.Threading.Tasks;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI.ChildViewStrategies
{
	internal interface IChildViewStrategy
	{
		Task<object> ShowAsync(IChildViewModel viewModel, object optParam = null);
		void CloseViewModel(IChildViewModel childViewModel);
		void CloseAll();
	}
}