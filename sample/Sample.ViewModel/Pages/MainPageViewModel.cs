using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.ViewModel.ViewModels;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;
using ReactiveUI;
using Sample.Logic;
using Sample.ViewModel.Notifications;
using Sample.ViewModel.Regions;
using Sample.ViewModel.SettingsFlyouts;


namespace Sample.ViewModel.Pages
{
    public class MainPageViewModel : PageViewModel
    {
        private IDisposable _selectedProductSubscription;
        
        public ICommand ShowSettingsFlyoutCommand { get; private set; }
        
        [Dependency]
        public IRepository Repository { protected get; set; }

        [Dependency]
        public GridRegionViewModel GridRegionViewModel { get; set; }

        [Dependency]
        public DetailsRegionViewModel DetailsRegionViewModel { get; set; }
        
        public MainPageViewModel()
        {
            ShowSettingsFlyoutCommand = new DelegateCommand(OnShowSettingsFlyout);
        }

        public async override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            await GridRegionViewModel.InitializeAsync();
            await DetailsRegionViewModel.InitializeAsync();

            _selectedProductSubscription =  GridRegionViewModel.ObservableForProperty(vm => vm.SelectedProduct).Subscribe(change => DetailsRegionViewModel.Product = change.Value);
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);

            _selectedProductSubscription.Dispose();
        }

        private async void OnShowSettingsFlyout()
        {
            SettingsFlyoutShowParam param = new SettingsFlyoutShowParam();
            param.ShowIndependent = true;

            var vm = ChildViewModelManager.Resolve<AppSettingsFlyoutViewModel>();
            await vm.ShowAsync(param);
        }

        
    }
}
