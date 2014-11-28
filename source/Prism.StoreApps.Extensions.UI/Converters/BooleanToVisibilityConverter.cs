using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
    /// <summary>
    /// Converter performs direct conversion without parameter and invert conversion with "back" parameter
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && parameter.ToString() == "back")
                return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;

            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
            if (parameter != null && parameter.ToString() == "back")
            {
                return value is Visibility && (Visibility) value == Visibility.Collapsed;
            }

			return value is Visibility && (Visibility)value == Visibility.Visible;
		}
    }
}
