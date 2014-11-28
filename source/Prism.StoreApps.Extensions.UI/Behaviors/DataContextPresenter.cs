using Windows.UI.Xaml;

namespace Prism.StoreApps.Extensions.UI.Behaviors
{
    public class DataContextPresenter : DependencyObject
    {
        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register("DataContext", typeof (object), typeof (DataContextPresenter), new PropertyMetadata(null));

        public object DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }
    }
}
  