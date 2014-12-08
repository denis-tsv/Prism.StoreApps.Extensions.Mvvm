using System.Reflection;
using Prism.StoreApps.Extensions.Common.Attributes;
using Prism.StoreApps.Extensions.ViewModel.ViewModels;
using Sample.Model;

namespace Sample.ViewModel.Regions
{
    public class DetailsRegionViewModel : RegionViewModel
    {
        private Product _product;

        public string Header
        {
            get
            {
                var attribute = typeof(Product).GetTypeInfo().GetCustomAttribute<ClassDisplayNameAttribute>();
                return attribute.SingularDisplayName;
            }
        }

        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }
    }
}
