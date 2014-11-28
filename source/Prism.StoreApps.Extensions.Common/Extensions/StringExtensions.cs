using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Windows.UI;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class StringExtensions
	{
		private static readonly Regex HexColorRegex = new Regex("^#[0-9a-fA-F]{6,8}$");

		public static Color ToColor(this string str)
		{
			if (str == null)
				throw new ArgumentNullException("str");

			if (HexColorRegex.IsMatch(str))
			{
				return FromHex(str.Substring(1));
			}

			return FromColors(str);
		}

		private static Color FromHex(string str)
		{
			Debug.Assert(str.Length == 6 || str.Length == 8);

			byte a = 255;
			int diff = 0;

			if (str.Length == 8)
			{
				diff = 2;
				a = GetChannelValue(str, 0);
			}

			var r = GetChannelValue(str, 0 + diff);
			var g = GetChannelValue(str, 2 + diff);
			var b = GetChannelValue(str, 4 + diff);

			return Color.FromArgb(a, r, g, b);
		}

		private static byte GetChannelValue(string str, int startIndex, int length = 2)
		{
			return byte.Parse(str.Substring(startIndex, length), NumberStyles.HexNumber);
		}

		private static Color FromColors(string str)
		{
			var propQuery = from prop in typeof(Colors).GetRuntimeProperties()
							 where
								prop.CanRead
								&& prop.PropertyType == typeof(Color)
								&& String.Compare(prop.Name, str, StringComparison.OrdinalIgnoreCase) == 0
							 select prop;

			var colorProperty = propQuery.FirstOrDefault();
			if (colorProperty == null)
				throw new ArgumentException(String.Format("Не удалось преобразовать '{0}' в цвет.", str), "str");

			return (Color)colorProperty.GetValue(null);
		}
	}
}
