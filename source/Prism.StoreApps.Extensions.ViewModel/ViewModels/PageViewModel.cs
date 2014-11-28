using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Interfaces;
using ReactiveUI.Legacy;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels
{
	public class PageViewModel : Microsoft.Practices.Prism.Mvvm.ViewModel, IViewModel
	{
        private ReactiveCommand _goBackCommand;

		[Dependency]
		public IChildViewModelManager ChildViewModelManager { protected get; set; }

        [Dependency]
        public INavigationService NavigationService { protected get; set; }

        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new ReactiveCommand(CanGoBackObservable(), CanGoBackInitialCondition);
                    _goBackCommand.Subscribe(_ => GoBackAction());
                }
                
                return _goBackCommand;
            }
        }

	    protected virtual Action GoBackAction
	    {
	        get { return NavigationService.GoBack; }
	    }

	    protected virtual bool CanGoBackInitialCondition
        {
            get { return NavigationService.CanGoBack(); }
        }

	    protected virtual IObservable<bool> CanGoBackObservable()
	    {
	        var list = new List<bool> { NavigationService.CanGoBack() };
	        return list.ToObservable();
	    }

	    public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
		{
			base.OnNavigatedFrom(viewModelState, suspending);

		    if (!suspending)
		    {
		        ChildViewModelManager.CloseAll();
		    }
		}
	    
	}
}
