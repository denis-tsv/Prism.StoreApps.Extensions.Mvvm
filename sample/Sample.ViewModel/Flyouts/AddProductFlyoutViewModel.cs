using System;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.ViewModel;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;
using ReactiveUI;
using Sample.ViewModel.Regions;

namespace Sample.ViewModel.Flyouts
{
    [FlyoutToken(FlyoutTokens.AddProduct)]
	public class AddProductFlyoutViewModel : FlyoutViewModel
	{
        [Dependency]
        public DetailsRegionViewModel DetailsRegionViewModel { get; set; }

		protected override IObservable<bool> GetConfirmCanExecute()
		{
            return DetailsRegionViewModel.Product.WhenAnyValue(product => product.Name, (name) => !string.IsNullOrEmpty(name));
		}
	}
}
