using System;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;
using ReactiveUI;
using Sample.ViewModel.Regions;

namespace Sample.ViewModel.Childs
{
    public class AddProductDialogViewModel : DialogChildViewModel
    {
        [Dependency]
        public DetailsRegionViewModel DetailsRegionViewModel { get; set; }

        protected override IObservable<bool> GetOkCanExecute()
        {
            return DetailsRegionViewModel.Product.WhenAnyValue(product => product.Name, (name) => !string.IsNullOrEmpty(name));
        }
    }
}
