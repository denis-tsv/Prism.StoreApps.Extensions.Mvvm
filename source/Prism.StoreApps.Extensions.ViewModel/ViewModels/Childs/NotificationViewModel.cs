using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
	public abstract class NotificationViewModel : ChildViewModel, IPopupViewModel
	{
		#region Fields

		private ICommand _confirmCommand;

		#endregion

		#region Commands

		public ICommand ConfirmCommand
		{
			get { return _confirmCommand ?? (_confirmCommand = new DelegateCommand(OnConfirm)); }
		}

		#endregion

		#region Methods

		public async Task ShowAsync(PopupShowParam showParam)
		{
			await OnShow();
			await ChildViewModelManager.ShowViewModelAsync(this, showParam);
		}

		protected virtual void OnConfirm()
		{
			Close();
		}

		#endregion
	}
}
