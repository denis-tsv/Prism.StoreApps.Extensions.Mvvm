using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
	public class EnumValueToVisibilityConverter<T> : IValueConverter
		where T : struct
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			if (parameter == null)
				throw new ArgumentNullException("parameter");

			var parameterEnumValue = Enum.Parse(typeof(T), parameter.ToString());
			return value.Equals(parameterEnumValue) ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}

}
