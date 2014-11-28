using System.Threading.Tasks;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
	public class SettingsFlyoutViewModel : ChildViewModel, ISettingsFlyoutViewModel
	{
		public async Task ShowAsync(SettingsFlyoutShowParam showParam)
		{
	        await OnShow();
			await ChildViewModelManager.ShowViewModelAsync(this, showParam);
		}

		public virtual bool OnBack()
		{
			return false;
		}
		
	}
}
