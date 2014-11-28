using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
	public class EnumValueToBooleanConverter<T> : IValueConverter
		where T : struct
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			if (parameter == null)
				throw new ArgumentNullException("parameter");

			var parameterEnumValue = Enum.Parse(typeof(T), parameter.ToString());
			return value.Equals(parameterEnumValue);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			if (!(value is bool))
				throw new ArgumentException("value");

			var boolValue = (bool)value;
			if (!boolValue)
				return DependencyProperty.UnsetValue;

			if (parameter == null)
				throw new ArgumentNullException("parameter");

			var parameterEnumValue = Enum.Parse(typeof(T), parameter.ToString());
			return parameterEnumValue;
		}
	}

}
