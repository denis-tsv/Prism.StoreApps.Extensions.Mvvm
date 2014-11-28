using System;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
	public class StringFormatConverter : IValueConverter
	{
		public string FormatString { get; set; }

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var formattableValue = value as IFormattable;

			if (formattableValue == null)
				return null;

			return formattableValue.ToString(FormatString, null);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new Exception();
		}
	}
}
