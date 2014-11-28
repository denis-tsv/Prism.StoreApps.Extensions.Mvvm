using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;
using ReactiveUI.Legacy;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
	public abstract class FlyoutViewModel : ChildViewModel, IFlyoutViewModel
	{
		private bool _isDismissed = true;
		public bool IsDismissed
		{
			get { return _isDismissed; }
			protected set { SetProperty(ref _isDismissed, value); }
		}

	    public virtual double MaxHeight
	    {
            get { return Double.NaN; }
	    }

	    public virtual double MaxWidth 
        {
	        get { return Double.NaN; }
	    }

	    private ReactiveCommand _confirmCommand;
		public ICommand ConfirmCommand
		{
			get
			{
				if (_confirmCommand == null)
				{
					_confirmCommand = new ReactiveCommand(GetConfirmCanExecute(), false);
					_confirmCommand.Subscribe(_ => OnConfirm());
				}
				return _confirmCommand;
			}
		}

		protected virtual IObservable<bool> GetConfirmCanExecute()
		{
			return Observable.Return(true);
		}

		protected virtual void OnConfirm()
		{
			IsDismissed = false;
			Close();
		}

		public new async Task<bool> ShowAsync()
		{
			await OnShow();
			var result = await ChildViewModelManager.ShowViewModelAsync(this);
			return (bool)result;
		}
	}
}
