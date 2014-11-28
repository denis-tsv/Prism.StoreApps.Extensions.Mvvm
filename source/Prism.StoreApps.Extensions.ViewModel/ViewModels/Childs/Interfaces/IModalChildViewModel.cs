namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces
{
	public interface IModalChildViewModel : IChildViewModel
	{
		object ModalResult { get; }
	}

	public interface IModalChildViewModel<out T> : IModalChildViewModel
	{
		new T ModalResult { get; }
	}
}