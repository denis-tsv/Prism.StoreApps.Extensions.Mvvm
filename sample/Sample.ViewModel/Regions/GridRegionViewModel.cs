using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.Attributes;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;
using Sample.Logic;
using Sample.Model;
using Sample.ViewModel.Childs;
using Sample.ViewModel.Flyouts;
using Sample.ViewModel.Notifications;

namespace Sample.ViewModel.Regions
{
    public class GridRegionViewModel : RegionViewModel
    {
        private Product _selectedProduct;
        private ObservableCollection<Product> _products;

        public GridRegionViewModel()
        {
            AddProductFlyoutCommand = new DelegateCommand(OnAddProductFlyout);
            AddProductDialogCommand = new DelegateCommand(OnAddProductDialog);
            DeleteProductCommand = new DelegateCommand(OnDeleteProduct);
            RefreshCommand = new DelegateCommand(OnRefresh);
        }

        [Dependency]
        public IRepository Repository { protected get; set; }

        [Dependency]
        public IAlertMessageManager AlertMessageManager { protected get; set; }

        [Dependency]
        public IChildViewModelManager ChildViewModelManager { protected get; set; }

        public ICommand AddProductFlyoutCommand { get; private set; }
        public ICommand AddProductDialogCommand { get; private set; }
        public ICommand DeleteProductCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public string Header 
        {
            get
            {
                var attribute = typeof (Product).GetTypeInfo().GetCustomAttribute<ClassDisplayNameAttribute>();
                return attribute.PluralDisplayName;
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            protected set { SetProperty(ref _products, value); }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        public async override Task InitializeAsync()
        {
            Products = new ObservableCollection<Product>(await Repository.GetProductsAsync());
        }

        private async void OnAddProductFlyout()
        {
            var vm = ChildViewModelManager.Resolve<AddProductFlyoutViewModel>();
            vm.DetailsRegionViewModel.Product = Repository.CreateNewProduct();

            if (await vm.ShowAsync())
            {
                Repository.AddProduct(vm.DetailsRegionViewModel.Product);
                Products.Add(vm.DetailsRegionViewModel.Product);
            }
        }

        private async void OnAddProductDialog()
        {
            var vm = ChildViewModelManager.Resolve<AddProductDialogViewModel>();
            vm.DetailsRegionViewModel.Product = Repository.CreateNewProduct();

            if ((await vm.ShowAsync()) == DialogResult.Ok)
            {
                Repository.AddProduct(vm.DetailsRegionViewModel.Product);
                Products.Add(vm.DetailsRegionViewModel.Product);
            }
        }

        private async void OnDeleteProduct()
        {
            if (SelectedProduct == null)
            {
                await AlertMessageManager.ShowDialogAsync("Please select product", "Delete product", DialogCommand.OkCommands);
                return;
            }

            var result = await AlertMessageManager.ShowDialogAsync("Do you want to delete product", "Delete product", DialogCommand.YesNoCancelCommands, 1);
            if (result == DialogResult.Yes)
            {
                Products.Remove(SelectedProduct);
                SelectedProduct = null;
            }
        }

        private async void ShowErrorMessage(Exception ex)
        {
            var param = new PopupShowParam
            {
                Mode = PopupShowParamMode.UpperRegionNotification,
                DisplayDuration = TimeSpan.FromSeconds(3.5D)
            };

            var vm = ChildViewModelManager.Resolve<ErrorNotificationViewModel>();
            vm.ErrorMessgae = ex.Message;
            await vm.ShowAsync(param);
        }

        private void OnRefresh()
        {
            try
            {
                Repository.Refresh();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }
    }
}
