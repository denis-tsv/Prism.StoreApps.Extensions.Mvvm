using System.Threading.Tasks;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces
{
	public interface ISettingsFlyoutViewModel : IChildViewModel
	{
		Task ShowAsync(SettingsFlyoutShowParam showParam);
		/// <returns><value>true</value> - allow close flyout, <value>false</value> - forbid close flyout</returns>
		bool OnBack();
		
	}

	public class SettingsFlyoutShowParam
	{
		public bool ShowIndependent { get; set; }
	}
}