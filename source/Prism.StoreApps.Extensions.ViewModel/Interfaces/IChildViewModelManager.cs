using System.Threading.Tasks;
using Windows.UI.Xaml;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.Interfaces
{
	public interface IChildViewModelManager
	{
		T Resolve<T>() 
			where T : IChildViewModel;
		
		Task<object> ShowViewModelAsync(IChildViewModel childViewModel, object optParam = null);		
		void CloseViewModel(IChildViewModel childViewModel);

		void CloseAll();

		void RegisterFlyout(FrameworkElement obj, string token);
	}
}