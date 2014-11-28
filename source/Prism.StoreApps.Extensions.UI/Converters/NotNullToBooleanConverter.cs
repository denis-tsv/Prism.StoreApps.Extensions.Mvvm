using System;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
    public class NotNullToBooleanConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new Exception();
        }
    }
}
