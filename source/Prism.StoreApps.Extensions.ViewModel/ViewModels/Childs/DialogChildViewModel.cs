using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using ReactiveUI.Legacy;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
    public enum DialogResult
    {
        Ok,
        Cancel,
		Yes,
		No,
		Abort,
		Retry,
		Ignore
    }

    public abstract class DialogChildViewModel : ModalChildViewModel<DialogResult>
	{
		private ReactiveCommand _okCommand;
		private DelegateCommand _cancelCommand;
        

	    public ICommand OkCommand
		{
			get
			{
				if (_okCommand == null)
				{
					_okCommand = new ReactiveCommand(GetOkCanExecute(), false);
					_okCommand.Subscribe(_ => OnOk());
				}
				return _okCommand;
			}
		}

		public ICommand CancelCommand
		{
			get { return _cancelCommand ?? (_cancelCommand = new DelegateCommand(OnCancel)); }
		}

		protected virtual IObservable<bool> GetOkCanExecute()
		{
			return Observable.Return(true);
		}

		protected virtual void OnOk()
		{
			ModalResult = DialogResult.Ok;
		}

		protected virtual void OnCancel()
		{
			ModalResult = DialogResult.Cancel;
		}
	}
}
