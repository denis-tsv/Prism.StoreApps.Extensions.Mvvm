using System;
using Windows.UI.Xaml.Data;
using Prism.StoreApps.Extensions.Common;

namespace Prism.StoreApps.Extensions.UI.Converters
{
    public class EnumToWrapperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            var wrapperType = typeof (EnumWrapper<>).MakeGenericType(value.GetType());
            var res = Activator.CreateInstance(wrapperType, value);
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
