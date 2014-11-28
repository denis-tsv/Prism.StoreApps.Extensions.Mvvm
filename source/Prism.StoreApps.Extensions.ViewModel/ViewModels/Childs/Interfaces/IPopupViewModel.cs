using System;
using System.Threading.Tasks;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces
{
	public enum PopupShowParamMode
	{
		FullScreen,
		UpperRegionNotification
	}

	public class PopupShowParam
	{
		public PopupShowParamMode Mode { get; set; }
		public TimeSpan? DisplayDuration { get; set; }
	}

	public interface IPopupViewModel : IChildViewModel
	{
		Task ShowAsync(PopupShowParam showParam);
	}
}