namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces
{
	public interface IFlyoutViewModel : IChildViewModel
	{
		bool IsDismissed { get; }

        double MaxHeight { get; }

        double MaxWidth { get; }
	}
}
