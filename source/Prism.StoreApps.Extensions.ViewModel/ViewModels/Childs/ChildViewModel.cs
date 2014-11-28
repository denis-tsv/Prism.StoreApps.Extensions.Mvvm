using System;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
	public abstract class ChildViewModel : ViewModel, IChildViewModel
	{
		[Dependency]
		public IChildViewModelManager ChildViewModelManager { protected get; set; }

        public event EventHandler Closed;

		protected void Close()
		{
            ChildViewModelManager.CloseViewModel(this);

		    if (Closed != null)
		    {
		        Closed(this, EventArgs.Empty);
		    }
		}

	    protected virtual Task OnShow()
	    {
			return Task.FromResult<object>(null);
	    }

	    public async Task ShowAsync()
	    {
	        await OnShow();
			await ChildViewModelManager.ShowViewModelAsync(this);
		}
	}
}
