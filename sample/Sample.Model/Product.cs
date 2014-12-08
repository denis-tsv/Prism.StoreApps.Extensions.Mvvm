using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.StoreApps.Extensions.Common.Attributes;
using Sample.Model.Strings;

namespace Sample.Model
{
    [ClassDisplayName("Product_SingularName", "Product_PluralName", typeof(Resources))]
    public class Product : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
